using OpenCvSharp.CPlusPlus;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalvoScanner.LaserVision.OpenCV
{
    public static class ProcessUtils
    {
        public static double Distance(System.Drawing.Point p1, System.Drawing.Point p2)
        {
            return Math.Sqrt(Math.Pow(p1.X - p2.X, 2) + Math.Pow(p1.Y - p2.Y, 2));
        }

        public static double Distance(PointDouble p1, PointDouble p2)
        {
            return Math.Sqrt(Math.Pow(p1.X - p2.X, 2) + Math.Pow(p1.Y - p2.Y, 2));
        }


        public static Mat RGBTo(Bitmap src, ColorType type)
        {
            if (src == null)
                return null;

            return RGBTo(OpenCvSharp.Extensions.BitmapConverter.ToMat(src), type);
        }

        public static Mat RGBTo(Mat src, ColorType type, Rectangle rect = new Rectangle())
        {
            if (src.Type() != MatType.CV_8UC3)
            {
                if (src.Type() == MatType.CV_8U) // input image가 8-bit 이미지이면 그냥 리턴
                    return src.Clone();
                else
                    return null;
            }

            switch (type)
            {
                // RGB
                case ColorType.Red: return RGBTo(src, 2);
                case ColorType.Green: return RGBTo(src, 1);
                case ColorType.Blue: return RGBTo(src, 0);
                case ColorType.Gray: return RGBToGray(src);

                // HSV
                case ColorType.Hue: return RGBToHue(src);
                case ColorType.Saturation: return RGBToSaturation(src);
                case ColorType.Value: return RGBToValue(src);

                default: return RGBToGray(src);
            }
        }

        unsafe private static Mat RGBToGray(Mat src)
        {
            Mat result = new Mat(src.Rows, src.Cols, MatType.CV_8U);

            byte* pSrc = src.DataPointer;
            byte* pDst = result.DataPointer;

            int pixels = src.Width * src.Height;

            for (int i = 0; i < pixels; i++)
            {
                byte b = *pSrc++;
                byte g = *pSrc++;
                byte r = *pSrc++;

                double gray = 0.299 * r + 0.587 * g + 0.114 * b + 0.5;

                *pDst++ = (byte)Math.Min(gray, 255);
            }

            return result;
        }

        unsafe private static Mat RGBTo(Mat src, int type)
        {
            Mat result = new Mat(src.Rows, src.Cols, MatType.CV_8U);

            byte* pSrc = src.DataPointer;
            byte* pDst = result.DataPointer;

            int pixels = src.Width * src.Height;

            for (int i = 0; i < pixels; i++)
            {
                *pDst++ = *(pSrc + type);
                pSrc += src.Channels();
            }

            return result;
        }

        unsafe private static Mat RGBToValue(Mat src)
        {
            Mat result = new Mat(src.Rows, src.Cols, MatType.CV_8U);

            byte* pSrc = src.DataPointer;
            byte* pDst = result.DataPointer;

            int pixels = src.Width * src.Height;

            for (int i = 0; i < pixels; i++)
            {
                double b = *pSrc++ / 255.0;
                double g = *pSrc++ / 255.0;
                double r = *pSrc++ / 255.0;

                double max = Max(r, g, b);

                *pDst++ = (byte)(max * 255.0 + 0.5);
            }

            return result;
        }

        unsafe private static Mat RGBToHue(Mat src)
        {
            Mat result = new Mat(src.Rows, src.Cols, MatType.CV_8U);

            byte* pSrc = src.DataPointer;
            byte* pDst = result.DataPointer;

            int pixels = src.Width * src.Height;

            for (int i = 0; i < pixels; i++)
            {
                double b = *pSrc++ / 255.0;
                double g = *pSrc++ / 255.0;
                double r = *pSrc++ / 255.0;

                double max = Max(r, g, b);
                double min = Min(r, g, b);

                double hue = 0;
                if (max == r) hue = 60.0 * (g - b) / (max - min);
                else if (max == g) hue = 120 + 60.0 * (b - r) / (max - min);
                else if (max == b) hue = 240 + 60.0 * (r - g) / (max - min);

                if (hue < 0) hue += 360.0;

                hue /= 2;

                *pDst++ = (byte)(hue + 0.5);
            }

            return result;
        }

        unsafe private static Mat RGBToSaturation(Mat src)
        {
            Mat result = new Mat(src.Rows, src.Cols, MatType.CV_8U);

            byte* pSrc = src.DataPointer;
            byte* pDst = result.DataPointer;

            int stride = src.Width * src.Channels();

            int pixels = src.Width * src.Height;

            for (int i = 0; i < pixels; i++)
            {
                double b = *pSrc++ / 255.0;
                double g = *pSrc++ / 255.0;
                double r = *pSrc++ / 255.0;

                double max = Max(r, g, b);
                double min = Min(r, g, b);

                if (max != 0)
                    *pDst++ = (byte)(Math.Min((max - min) / max * 255 + 0.5, 255));
                else
                    *pDst++ = 0;
            }

            return result;
        }

        private static double Max(double v1, double v2, double v3)
        {
            return Math.Max(Math.Max(v1, v2), v3);
        }

        private static byte Max(byte v1, byte v2, byte v3)
        {
            return Math.Max(Math.Max(v1, v2), v3);
        }

        private static byte Min(byte v1, byte v2, byte v3)
        {
            return Math.Min(Math.Min(v1, v2), v3);
        }

        private static double Min(double v1, double v2, double v3)
        {
            return Math.Min(Math.Min(v1, v2), v3);
        }

        // <Circle Fitting 수행>
        //   - 원점부터 거리를 측정하여 크게 벗어나는 점을 제거하고 피팅함
        //   - 관련 파라미터 : threshold
        public static void FitCircle(OverlayUnitParameter param, CircleInfo circleInfo)
        {
            if (!param.CircleFittingEnabled)
                return;
            if (circleInfo.CenterPoint.X <= 0 || circleInfo.CenterPoint.Y <= 0)
                return;

            // 거리 측정하여 중간값 찾음
            // 거리 : 원점부터 엣지 포인트까지 거리
            List<double> distList = new List<double>();
            foreach (System.Drawing.Point p in circleInfo.PerimeterPoints)
            {
                distList.Add(Distance(circleInfo.CenterPoint, p));
            }
            List<double> distCopy = new List<double>(distList);
            distCopy.Sort();

            // threshold 퍼센트 만큼 벗어난 값 제거
            double mid = distCopy[Define.OverlayRays / 2];
            double upperThreshold = mid + mid * param.Threshold * 0.01;
            double lowerThreshold = mid - mid * param.Threshold * 0.01;

            List<System.Drawing.Point> targetPoints = new List<System.Drawing.Point>();
            for (int i = 0; i < Define.OverlayRays; i++)
            {
                if (lowerThreshold < distList[i] && distList[i] < upperThreshold)
                {
                    targetPoints.Add(circleInfo.PerimeterPoints[i]);
                }
            }

            // Circle Fitting
            FitCircle(circleInfo, targetPoints);
        }

        // <Circle Fitting>
        //  - 둘레 포인트를 원으로 바꿈
        private static void FitCircle(CircleInfo circleInfo, List<System.Drawing.Point> list)
        {
            int pointNum = list.Count;

            double sx = 0.0;
            double sy = 0.0;
            double sx2 = 0.0;
            double sy2 = 0.0;
            double sxy = 0.0;
            double sx3 = 0.0;
            double sy3 = 0.0;
            double sx2y = 0.0;
            double sxy2 = 0.0;

            for (int i = 0; i < pointNum; i++)
            {
                double x = list[i].X;
                double y = list[i].Y;

                double xx = x * x;
                double yy = y * y;

                sx += x;
                sy += y;
                sx2 += xx;
                sy2 += yy;
                sxy += (x * y);
                sx3 += (x * xx);
                sy3 += (y * yy);
                sx2y += (xx * y);
                sxy2 += (yy * x);
            }

            double a1 = 2.0 * (sx * sx - sx2 * pointNum);
            double a2 = 2.0 * (sx * sy - sxy * pointNum);
            double b1 = a2;
            double b2 = 2.0 * (sy * sy - sy2 * pointNum);
            double c1 = sx2 * sx - sx3 * pointNum + sx * sy2 - sxy2 * pointNum;
            double c2 = sx2 * sy - sy3 * pointNum + sy * sy2 - sx2y * pointNum;

            double det = a1 * b2 - a2 * b1;

            double cx = (c1 * b2 - c2 * b1) / det;
            double cy = (a1 * c2 - a2 * c1) / det;

            double radsq = (sx2 - 2 * sx * cx + cx * cx * pointNum + sy2 - 2 * sy * cy + cy * cy * pointNum) / pointNum;
            double radius = Math.Sqrt(radsq);

            circleInfo.CenterPoint.X = (int)(cx + 0.5);
            circleInfo.CenterPoint.Y = (int)(cy + 0.5);
            circleInfo.CP.X = cx;
            circleInfo.CP.Y = cy;
            circleInfo.Radius = radius;

            // fitting에 맞게 둘레 포인트들 갱신
            // center point -> (cx, cy)
            // radius       -> radius
            double gap = Define.OverlayInspectionAngle * Math.PI / 180;
            double radian = 0;

            for (int i = 0; i < Define.OverlayRays; i++)
            {
                circleInfo.PerimeterPoints[i].X = (int)(cx + Math.Cos(radian) * radius + 0.5);
                circleInfo.PerimeterPoints[i].Y = (int)(cy + Math.Sin(radian) * radius + 0.5);

                radian += gap;
            }
        }

        // <사용하지 않음>
        //  - circle fitting하기 전의 둘레를 가지고 circularity 계산
        //  - l^2 / 4piA (l:둘레, A:넓이)
        public static void ScoreCircularity(CircleInfo circleInfo)
        {
            if (circleInfo.CenterPoint.X <= 0 || circleInfo.CenterPoint.Y <= 0)
                return;

            System.Drawing.Point leftTop = new System.Drawing.Point();
            System.Drawing.Point rightBottom = new System.Drawing.Point();

            leftTop.X = int.MaxValue;
            leftTop.Y = int.MaxValue;

            rightBottom.X = int.MinValue;
            rightBottom.Y = int.MinValue;

            foreach (System.Drawing.Point p in circleInfo.PerimeterPoints)
            {
                leftTop.X = Math.Min(leftTop.X, p.X);
                leftTop.Y = Math.Min(leftTop.Y, p.Y);
                rightBottom.X = Math.Max(rightBottom.X, p.X);
                rightBottom.Y = Math.Max(rightBottom.Y, p.Y);
            }

            OpenCvSharp.CPlusPlus.Scalar zero = new OpenCvSharp.CPlusPlus.Scalar(0);
            OpenCvSharp.CPlusPlus.Size size = new OpenCvSharp.CPlusPlus.Size(rightBottom.X - leftTop.X + 1, rightBottom.Y - leftTop.Y + 1);
            OpenCvSharp.CPlusPlus.Mat mat = new OpenCvSharp.CPlusPlus.Mat(size, OpenCvSharp.CPlusPlus.MatType.CV_8U, zero);

            OpenCvSharp.CPlusPlus.Scalar color = new OpenCvSharp.CPlusPlus.Scalar(255);
            OpenCvSharp.CPlusPlus.Point p1 = new OpenCvSharp.CPlusPlus.Point();
            OpenCvSharp.CPlusPlus.Point p2 = new OpenCvSharp.CPlusPlus.Point();

            // 이웃하는 점 선으로 연결
            // 이웃하는 점 거리 구하기
            double distance = 0;
            for (int i = 0; i < Define.OverlayRays - 1; i++)
            {
                p1.X = circleInfo.PerimeterPoints[i].X - leftTop.X;
                p1.Y = circleInfo.PerimeterPoints[i].Y - leftTop.Y;
                p2.X = circleInfo.PerimeterPoints[i + 1].X - leftTop.X;
                p2.Y = circleInfo.PerimeterPoints[i + 1].Y - leftTop.Y;
                mat.Line(p1, p2, color);
                distance += ProcessUtils.Distance(circleInfo.PerimeterPoints[i], circleInfo.PerimeterPoints[i + 1]);
            }
            p1.X = circleInfo.PerimeterPoints[0].X - leftTop.X;
            p1.Y = circleInfo.PerimeterPoints[0].Y - leftTop.Y;
            p2.X = circleInfo.PerimeterPoints[Define.OverlayRays - 1].X - leftTop.X;
            p2.Y = circleInfo.PerimeterPoints[Define.OverlayRays - 1].Y - leftTop.Y;
            mat.Line(p1, p2, color);
            distance += ProcessUtils.Distance(circleInfo.PerimeterPoints[0], circleInfo.PerimeterPoints[Define.OverlayRays - 1]);

            p1.X = mat.Cols / 2;
            p1.Y = mat.Rows / 2;

            OpenCvSharp.CPlusPlus.Point center = new OpenCvSharp.CPlusPlus.Point(circleInfo.CenterPoint.X - leftTop.X, circleInfo.CenterPoint.Y - leftTop.Y);
            mat.FloodFill(center, color);

            float area = mat.CountNonZero();

            float circularity = (float)(Math.Pow(distance, 2.0f) / (4 * Math.PI * area));
            circleInfo.Score = circularity;

            mat.Dispose();
        }

        // Deep copy
        unsafe public static Bitmap BitmapCopy(Bitmap image, Rectangle section)
        {
            Bitmap copy = new Bitmap(section.Width, section.Height, image.PixelFormat);
            BitmapData imageData = image.LockBits(new Rectangle(0, 0, image.Width, image.Height), ImageLockMode.ReadOnly, image.PixelFormat);
            BitmapData copyData = copy.LockBits(new Rectangle(0, 0, section.Width, section.Height), ImageLockMode.WriteOnly, image.PixelFormat);

            byte* pSrcStart = (byte*)imageData.Scan0;
            byte* pDstStart = (byte*)copyData.Scan0;

            int channels = imageData.Stride / imageData.Width;

            int widthIterations = section.Width * channels;

            pSrcStart += (section.Y * imageData.Stride + channels * section.X);

            for (int y = 0; y < section.Height; y++)
            {
                byte* pSrc = pSrcStart;
                byte* pDst = pDstStart;

                for (int x = 0; x < widthIterations; x++)
                {
                    *pDst++ = *pSrc++;
                }

                pSrcStart += imageData.Stride;
                pDstStart += copyData.Stride;
            }

            image.UnlockBits(imageData);
            copy.UnlockBits(copyData);

            return copy;
        }


        public static void GetEdgePoint(
            Mat image,
            PointDouble startPoint,
            PointDouble vector,
            OverlayUnitParameter param,
            out System.Drawing.Point edgePoint)
        {
            edgePoint = new System.Drawing.Point();

            double prox = TargetGV(image, startPoint, vector, param);
            edgePoint = GetEdgePoint(image, startPoint, vector, param, prox);
        }

        unsafe private static System.Drawing.Point GetEdgePoint(
            Mat image,
            PointDouble point,
            PointDouble vector,
            OverlayUnitParameter param,
            double prox)
        {
            byte* pImg = image.DataPointer;

            System.Drawing.Point result = new System.Drawing.Point((int)point.X, (int)point.Y);

            double positionX;
            double positionY;
            double vectorX;
            double vectorY;

            // 찾는 방향 설정 (In to Out / Out to In)
            SetDirection(param, vector, out positionX, out positionY, out vectorX, out vectorY);

            byte prev = 0;
            byte current = 0;

            int x = 0;
            int y = 0;

            x = (int)(point.X + positionX + 0.5);
            y = (int)(point.Y + positionY + 0.5);

            CheckPointRange(image, ref x, ref y);

            prev = pImg[y * image.Width + x];

            for (int i = 0; i < param.SearchLength; i++)
            {
                x = (int)(point.X + positionX + 0.5);
                y = (int)(point.Y + positionY + 0.5);

                CheckPointRange(image, ref x, ref y);

                current = pImg[y * image.Width + x];
                if ((current >= prox && prox > prev) ||
                   (current <= prox && prox < prev))
                {
                    return new System.Drawing.Point(x, y);
                }

                prev = current;

                positionX += vectorX;
                positionY += vectorY;
            }

            return result; // 못찾으면 그대로 반환
        }

        private static void SetDirection(
            OverlayUnitParameter param,
            PointDouble vector,
            out double positionX,
            out double positionY,
            out double vectorX,
            out double vectorY)
        {
            positionX = 0;
            positionY = 0;
            vectorX = vector.X;
            vectorY = vector.Y;

            if (param.SearchDirection == OverlaySearchDirection.OutToIn)
            {
                positionX = param.SearchLength * vectorX;
                positionY = param.SearchLength * vectorY;
                vectorX *= -1;
                vectorY *= -1;
            }
        }

        private static double TargetGV(
            Mat image,
            PointDouble point,
            PointDouble vector,
            OverlayUnitParameter param)
        {
            double target = 0;

            if (param.AbsoluteGVEnabled)
            {
                target = param.SearchGV;
            }
            else
            {
                byte min;
                byte max;
                GetMinMax(image, point, vector, param.SearchLength, out min, out max);
                target = (double)(min + (max - min) * param.SearchGV * 0.01);
            }

            return target;
        }

        private static double TargetGV(
            Mat image,
            PointDouble point,
            PointDouble vector,
            byte searchGV,
            int searchLength)
        {
            double target = 0;

            byte min;
            byte max;
            GetMinMax(image, point, vector, searchLength, out min, out max);
            target = (double)(min + (max - min) * searchGV * 0.01);

            return target;
        }

        private static void CheckPointRange(
            Mat image,
            ref int x,
            ref int y)
        {
            if (x < 0) x = 0;
            if (y < 0) y = 0;
            if (x >= image.Width) x = image.Width - 1;
            if (y >= image.Height) y = image.Height - 1;
        }

        unsafe private static void GetMinMax(
            Mat image,
            PointDouble point,
            PointDouble vector,
            int searchLength,
            out byte min,
            out byte max)
        {
            byte* pImg = image.DataPointer;

            min = Byte.MaxValue;
            max = Byte.MinValue;

            double vectorX = 0;
            double vectorY = 0;

            for (int i = 0; i < searchLength; i++)
            {
                int x = (int)(point.X + vectorX + 0.5);
                int y = (int)(point.Y + vectorY + 0.5);

                CheckPointRange(image, ref x, ref y);

                byte pix = pImg[y * image.Width + x];
                if (pix < min)
                    min = pix;
                if (pix > max)
                    max = pix;

                vectorX += vector.X;
                vectorY += vector.Y;
            }
        }

        // 타원의
        // minor : 단축
        // major : 장축
        // x0, y0 : 중심
        // tilt : 기울기
        // 를 구하고 나면 타원을 그릴 수 있음
        public static void FitEllipse(CircleInfo circleInfo, double minor, double major, double x0, double y0, double tilt)
        {
            double gap = 2 * Math.PI / Define.OverlayRays;
            double radian = 0;

            for (int i = 0; i < Define.OverlayRays; i++)
            {
                double cosTau = Math.Cos(tilt);
                double sinTau = Math.Sin(tilt);

                double sin_t = Math.Sin(radian);
                double cos_t = Math.Cos(radian);

                circleInfo.PerimeterPoints[i].X = (int)(x0 + cosTau * (minor * cos_t) - sinTau * (major * sin_t) + 0.5);
                circleInfo.PerimeterPoints[i].Y = (int)(y0 + sinTau * (minor * cos_t) + cosTau * (major * sin_t) + 0.5);

                radian += gap;
            }
        }

        // 타원 구한 후 score 저장
        unsafe public static void FitEllipse(CircleInfo circleInfo)
        {
            if (circleInfo.CenterPoint.X < 0 || circleInfo.CenterPoint.Y < 0)
                return;

            int rows = circleInfo.PerimeterPoints.Length;
            int cols = 5; // 변수 개수
            Mat A = new Mat(rows, cols, MatType.CV_64F);
            Mat B = new Mat(rows, 1, MatType.CV_64F);

            // x^2 + ay^2 + bxy + cx + dy + e = 0
            for (int row = 0; row < rows; row++)
            {
                System.Drawing.Point p = circleInfo.PerimeterPoints[row];
                A.Set<double>(row, 0, p.Y * p.Y);
                A.Set<double>(row, 1, p.X * p.Y);
                A.Set<double>(row, 2, p.X);
                A.Set<double>(row, 3, p.Y);
                A.Set<double>(row, 4, 1);

                B.Set<double>(row, 0, -1 * p.X * p.X);
            }

            Mat invA = A.Inv(MatrixDecomposition.SVD);

            Mat X = invA * B;

            double a = 1;
            double b = X.Get<double>(1, 0);
            double c = X.Get<double>(0, 0);
            double d = X.Get<double>(2, 0);
            double f = X.Get<double>(3, 0);
            double g = X.Get<double>(4, 0);

            b /= 2.0;
            d /= 2.0;
            f /= 2.0;

            double up = 2 * (a * f * f + c * d * d + g * b * b - 2 * b * d * f - a * c * g);
            double dw = (b * b - a * c) * (Math.Sqrt(Math.Pow(a - c, 2) + 4 * b * b) - (a + c));

            double a_ = Math.Sqrt(up / dw); // 장축

            dw = (b * b - a * c) * (-Math.Sqrt(Math.Pow(a - c, 2) + 4 * b * b) - (a + c));
            double b_ = Math.Sqrt(up / dw); // 단축

            double minor = Math.Min(a_, b_); // 혹시 몰라서 작은 값으로
            double major = Math.Max(a_, b_);

            circleInfo.Score = minor / major;

            A.Dispose();
            B.Dispose();
            invA.Dispose();
            X.Dispose();
        }

        // 선 따라가는 것을 편차가 아닌 반경으로 바꾸자
        // 8방향 모두 보기
        // 하나 획득 하고 삭제하면 상관 없을듯
        //private static void ArrangeLineList(out List<LineInfo> lineList, List<PointDouble> pointList, LASParameter param)
        //{
        //    lineList = new List<LineInfo>();

        //    int eliminateSize = 0;
        //    if (param.SearchDirection == LASSearchDirection.Width)
        //        eliminateSize = (int)(param.InspRectList[0].Height * param.MinLineRatio * 0.01);
        //    else
        //        eliminateSize = (int)(param.InspRectList[0].Width * param.MinLineRatio * 0.01);

        //    while (pointList.Count > 0)
        //    {
        //        LineInfo list = new LineInfo();
        //        PointDouble cur = new PointDouble();
        //        PointDouble prev = new PointDouble(pointList[0].X, pointList[0].Y);

        //        pointList.Remove(prev);

        //        list.Points.Add(prev);

        //        for (int i = 0; i < pointList.Count; i++)
        //        {
        //            cur = pointList[i];
        //            double diff = Distance(cur, prev);

        //            //if(param.SearchDirection == LASSearchDirection.Width)
        //            //    diff = Math.Abs(cur.X - prev.X);
        //            //else if (param.SearchDirection == LASSearchDirection.Height)
        //            //    diff = Math.Abs(cur.Y - prev.Y); 

        //            if (diff <= param.AcceptableDist)
        //            {
        //                list.Points.Add(cur);
        //                pointList.Remove(cur);
        //                i--;
        //                prev.X = cur.X;
        //                prev.Y = cur.Y;
        //            }
        //        }

        //        if (list.Points.Count >= eliminateSize)
        //            lineList.Add(list);
        //    }

        //    // Sorting
        //    foreach (LineInfo line in lineList)
        //    {
        //        if (param.SearchDirection == LASSearchDirection.Width)
        //            line.Points.Sort(PointDouble.CompareY);
        //        else if (param.SearchDirection == LASSearchDirection.Height)
        //            line.Points.Sort(PointDouble.CompareX);
        //    }
        //    if (param.SearchDirection == LASSearchDirection.Width)
        //        lineList.Sort(LineInfo.CompareListX);
        //    else if (param.SearchDirection == LASSearchDirection.Height)
        //        lineList.Sort(LineInfo.CompareListY);
        //}

        //public static double Distance(LineInfo line, PointDouble pt, InspectionLineAndSpace.FittingMode mode)
        //{
        //    double numerator;
        //    double denominator;

        //    double gradient = line.Gradient;
        //    double intercept = line.Intercept;

        //    switch (mode)
        //    {
        //        case InspectionLineAndSpace.FittingMode.ForY: // (y = ax + b) -> (-ax + y - b = 0)
        //            numerator = Math.Abs(-gradient * pt.X + pt.Y - intercept);
        //            break;
        //        case InspectionLineAndSpace.FittingMode.ForX: // (x = ay + b) -> (x - ay - b = 0)
        //            numerator = Math.Abs(pt.X - gradient * pt.Y - intercept);
        //            break;
        //        default:
        //            numerator = 0;
        //            break;
        //    }

        //    denominator = Math.Sqrt(1 + gradient * gradient);

        //    return numerator / denominator;
        //}

        //unsafe public static void FindLines(out List<LineInfo> lineList, Mat srcImg, Rectangle roi, LASParameter param)
        //{
        //    Mat gradImg = MakeGradientImage(srcImg, roi, param.SearchDirection);

        //    List<PointDouble> pointList = new List<PointDouble>();
        //    GetLineEdge(gradImg, pointList, param.SearchDirection, param.LocalMaxRatio);

        //    Mat edge = new Mat(roi.Height, roi.Width, MatType.CV_8U, 0);
        //    foreach (PointDouble pt in pointList)
        //    {
        //        edge.Set<byte>((int)pt.Y, (int)pt.X, 255);
        //    }

        //    ArrangeLineList(out lineList, pointList, param);

        //    if (lineList.Count > 0)
        //    {
        //        InspectionLineAndSpace.FittingMode mode;

        //        if (param.SearchDirection == LASSearchDirection.Width)
        //            mode = InspectionLineAndSpace.FittingMode.ForX;
        //        else
        //            mode = InspectionLineAndSpace.FittingMode.ForY;

        //        FitLine(lineList[0], mode);

        //        double gradient = lineList[0].Gradient;

        //        for (int i = 1; i < lineList.Count; i++)
        //        {
        //            lineList[i].Gradient = gradient;
        //            FitIntercept(lineList[i], mode);
        //        }
        //    }

        //    using (new OpenCvSharp.CPlusPlus.Window("Gradient Image", gradImg))
        //    using (new OpenCvSharp.CPlusPlus.Window("Edge Image", edge))
        //    {
        //        OpenCvSharp.CPlusPlus.Cv2.WaitKey();
        //    }

        //    edge.Dispose();
        //    gradImg.Dispose();
        //}


        private static void GetLineEdge(Mat srcImg, List<PointDouble> pointList, LASSearchDirection dir, double threshold)
        {
            int width = srcImg.Width;
            int height = srcImg.Height;

            switch (dir)
            {
                case LASSearchDirection.Width:
                    for (int h = 0; h < height; h++)
                    {
                        GetVerticalEdge(srcImg, h, pointList, threshold);
                    }
                    break;
                case LASSearchDirection.Height:
                    for (int w = 0; w < width; w++)
                    {
                        GetHorizontalEdge(srcImg, w, pointList, threshold);
                    }
                    break;
            }
        }

        unsafe private static void GetHorizontalEdge(Mat gradImg, int col, List<PointDouble> pointList, double threshold)
        {
            byte* grad = (byte*)gradImg.DataPointer;
            grad += col;

            byte globalMax = byte.MinValue;

            // Global Maximum
            for (int i = 0; i < gradImg.Height; i++)
                globalMax = Math.Max(grad[i * gradImg.Width], globalMax);

            // Local Threshold
            double localThreshold = globalMax * threshold * 0.01;

            // Fitted Peak Points (Local Maxima)
            for (int i = 0; i < gradImg.Height; i++)
            {
                int idx = i * gradImg.Width;

                if (grad[idx] >= localThreshold)
                {
                    byte maxValue = byte.MinValue;
                    int maxPosition = 0;
                    while (grad[idx] >= localThreshold && i < gradImg.Height)
                    {
                        if (grad[idx] > maxValue)
                        {
                            maxValue = grad[idx];
                            maxPosition = i;
                        }
                        i++;
                        idx = i * gradImg.Width;
                    }
                    if (maxValue > byte.MinValue)
                    {
                        double edgePosition = FitQuadraticAndGetPoint(grad, LASSearchDirection.Height, gradImg.Width, maxPosition - 1, maxPosition + 1);

                        if (edgePosition < gradImg.Height && edgePosition >= 0)
                            pointList.Add(new PointDouble(col, edgePosition));
                    }
                }
            }
        }

        unsafe private static void GetVerticalEdge(Mat gradImg, int row, List<PointDouble> pointList, double threshold)
        {
            byte* grad = (byte*)gradImg.DataPointer;
            grad += (gradImg.Width * row);

            byte globalMax = byte.MinValue;

            // Global Maximum
            for (int i = 0; i < gradImg.Width; i++)
                globalMax = Math.Max(grad[i], globalMax);

            // Local Threshold
            double localThreshold = globalMax * threshold * 0.01;

            // Fitted Peak Points (Local Maxima)
            for (int i = 0; i < gradImg.Width; i++)
            {
                if (grad[i] >= localThreshold)
                {
                    byte maxValue = byte.MinValue;
                    int maxPosition = 0;
                    while (grad[i] >= localThreshold && i < gradImg.Width)
                    {
                        if (grad[i] > maxValue)
                        {
                            maxValue = grad[i];
                            maxPosition = i;
                        }
                        i++;
                    }
                    if (maxValue > byte.MinValue)
                    {
                        double edgePosition = FitQuadraticAndGetPoint(grad, LASSearchDirection.Width, gradImg.Width, maxPosition - 1, maxPosition + 1);

                        if (edgePosition < gradImg.Width && edgePosition >= 0)
                            pointList.Add(new PointDouble(edgePosition, row));
                    }
                }
            }
        }

        unsafe private static double FitQuadraticAndGetPoint(byte* grad, LASSearchDirection dir, int width, int start, int end)
        {
            int length = end - start + 1;

            Mat A = new Mat(length, 3, MatType.CV_64F);
            Mat B = new Mat(length, 1, MatType.CV_64F);

            for (int i = start; i <= end; i++)
            {
                if (i < 0)
                    continue;

                A.Set<double>(i - start, 0, i * i);
                A.Set<double>(i - start, 1, i);
                A.Set<double>(i - start, 2, 1);

                switch (dir)
                {
                    case LASSearchDirection.Width:
                        B.Set<double>(i - start, grad[i]);
                        break;
                    case LASSearchDirection.Height:
                        B.Set<double>(i - start, grad[i * width]);
                        break;
                }
            }

            Mat inv = A.Inv(MatrixDecomposition.SVD);
            Mat X = inv * B;

            double a = X.Get<double>(0);
            double b = X.Get<double>(1);
            double c = X.Get<double>(2);

            return -b / (2 * a);
        }

        unsafe private static void FitQuadratic(out double pos, Mat grad, int start, int end)
        {
            int length = end - start + 1;

            Mat A = new Mat(length, 3, MatType.CV_64F);
            Mat B = new Mat(length, 1, MatType.CV_64F);

            double* pavg = (double*)grad.DataPointer;

            for (int i = start; i <= end; i++)
            {
                if (i < 0)
                    continue;

                int row = i - start;
                A.Set<double>(row, 0, i * i);
                A.Set<double>(row, 1, i);
                A.Set<double>(row, 2, 1);

                B.Set<double>(row, pavg[i]);
            }

            Mat inv = A.Inv(MatrixDecomposition.SVD);
            Mat X = inv * B;

            double a = X.Get<double>(0);
            double b = X.Get<double>(1);
            double c = X.Get<double>(2);

            pos = -b / (2 * a);
        }

        unsafe private static Mat MakeGradientImage(Mat srcImg, Rectangle roi, LASSearchDirection dir)
        {
            int[,] mask = new int[3, 3];
            int[,] mask2 = new int[3, 3];
            if (dir == LASSearchDirection.Width)
            {
                mask[0, 0] = -1; mask[0, 1] = 0; mask[0, 2] = 1;
                mask[1, 0] = -1; mask[1, 1] = 0; mask[1, 2] = 1;
                mask[2, 0] = -1; mask[2, 1] = 0; mask[2, 2] = 1;
            }
            else if (dir == LASSearchDirection.Height)
            {
                mask[0, 0] = -1; mask[0, 1] = -1; mask[0, 2] = -1;
                mask[1, 0] = 0; mask[1, 1] = 0; mask[1, 2] = 0;
                mask[2, 0] = 1; mask[2, 1] = 1; mask[2, 2] = 1;
            }

            int width = roi.Width;
            int height = roi.Height;

            Mat gradImg = new Mat(height, width, MatType.CV_8U);

            byte* src = (byte*)srcImg.DataPointer;
            byte* dst = (byte*)gradImg.DataPointer;

            // Gradient Image
            for (int h = roi.Top; h < roi.Bottom; h++)
            {
                for (int w = roi.Left; w < roi.Right; w++)
                {
                    int idx = (h - roi.Top) * width + (w - roi.Left);

                    int acc = 0;

                    for (int i = h - 1; i <= h + 1; i++)
                    {
                        if (i < 0 || i >= srcImg.Height)
                            continue;
                        for (int j = w - 1; j <= w + 1; j++)
                        {
                            if (j < 0 || j >= srcImg.Width)
                                continue;
                            acc += (src[i * srcImg.Width + j] * mask[i - (h - 1), j - (w - 1)]);

                        }
                    }

                    acc = Math.Abs(acc);
                    acc = Math.Min(acc, 255);
                    dst[idx] = (byte)acc;
                }
            }

            return gradImg;
        }

        //public static void FitIntercept(LineInfo line, InspectionLineAndSpace.FittingMode mode)
        //{
        //    int pointNum = line.Points.Count;
        //    if (pointNum <= 0)
        //        return;

        //    double sumX = 0;
        //    double sumY = 0;

        //    for (int i = 0; i < pointNum; i++)
        //    {
        //        sumX += line.Points[i].X;
        //        sumY += line.Points[i].Y;
        //    }

        //    double gradient = line.Gradient;
        //    switch (mode)
        //    {
        //        case InspectionLineAndSpace.FittingMode.ForY: // y = ax + b
        //            line.Intercept = (sumY - gradient * sumX) / pointNum;
        //            break;
        //        case InspectionLineAndSpace.FittingMode.ForX: // x = ay + b
        //            line.Intercept = (sumX - gradient * sumY) / pointNum;
        //            break;
        //    }
        //}

        //public static void FitLine(LineInfo line, InspectionLineAndSpace.FittingMode mode)
        //{
        //    int pointNum = line.Points.Count;
        //    if (pointNum <= 0)
        //    {
        //        line.Gradient = 0;
        //        line.Intercept = 0;
        //        return;
        //    }

        //    double s = pointNum;
        //    double sx = 0;
        //    double sy = 0;
        //    double sxx = 0;
        //    double sxy = 0;

        //    switch (mode)
        //    {
        //        case InspectionLineAndSpace.FittingMode.ForY: // y = ax + b
        //            foreach (PointDouble pt in line.Points)
        //            {
        //                sx += pt.X;
        //                sy += pt.Y;
        //                sxx += (pt.X * pt.X);
        //                sxy += (pt.X * pt.Y);
        //            }
        //            break;
        //        case InspectionLineAndSpace.FittingMode.ForX: // x = ay + b
        //            foreach (PointDouble pt in line.Points)
        //            {
        //                sx += pt.Y;
        //                sy += pt.X;
        //                sxx += (pt.Y * pt.Y);
        //                sxy += (pt.X * pt.Y);
        //            }
        //            break;
        //    }

        //    double delta = s * sxx - (sx * sx);
        //    line.Gradient = (s * sxy - sx * sy) / delta;
        //    line.Intercept = (sxx * sy - sx * sxy) / delta;
        //}

        unsafe public static void Sum(Bitmap img, Rectangle roi)
        {
            uint[] sum = new uint[roi.Height];

            BitmapData bd = img.LockBits(roi, ImageLockMode.ReadOnly, img.PixelFormat);

            byte* src = (byte*)bd.Scan0;
            int channels = bd.Stride / bd.Width;
            int widthStep = roi.Width * channels;

            int leftEnd = roi.Width / 3;
            int startRight = roi.Width * 2 / 3;

            byte* p = src;
            for (int y = roi.Top; y < roi.Bottom; y++)
            {
                p = src + y * widthStep;

                for (int x = roi.Left; x < roi.Right; x++)
                {
                    sum[y - roi.Top] += (*p++);
                }
            }

            img.UnlockBits(bd);
            System.IO.FileStream fs = null;
            System.IO.StreamWriter sw = null;
            try
            {
                fs = new System.IO.FileStream("data.txt", System.IO.FileMode.Create);
                sw = new System.IO.StreamWriter(fs, Encoding.ASCII);

                for (int i = 0; i < roi.Height; i++)
                {
                    sw.WriteLine(sum[i]);
                }

                sw.WriteLine("----------------------------");

                for (int i = 0; i < roi.Height; i++)
                {
                    sw.WriteLine(sum[i] / (double)roi.Width);
                }

                sw.Flush();
                sw.Close();
                fs.Close();
            }
            catch (Exception)
            {

            }
            finally
            {
                if (fs != null) fs.Dispose();
                if (sw != null) sw.Dispose();
            }

            System.Diagnostics.Debug.WriteLine("-----------------start-----------------");
            for (int i = 0; i < roi.Height; i++)
            {
                System.Diagnostics.Debug.WriteLine(sum[i]);
            }
        }

        unsafe private static void Average(out Mat avg, Mat srcImg, Rectangle roi, LASSearchDirection dir)
        {
            Mat roiImg = new Mat(srcImg, new Rect(roi.Left, roi.Top, roi.Width, roi.Height));
            byte* src = roiImg.DataPointer;

            if (dir == LASSearchDirection.Width)
            {
                avg = new Mat(1, roiImg.Cols, MatType.CV_64F);
                double* dst = (double*)avg.DataPointer;
                for (int x = 0; x < roiImg.Cols; x++)
                {
                    double sum = 0;
                    for (int y = 0; y < roiImg.Rows; y++)
                    {
                        sum += src[y * srcImg.Width + x];
                    }
                    dst[x] = sum / roiImg.Rows;
                }
            }
            else
            {
                avg = new Mat(1, roiImg.Rows, MatType.CV_64F);
                double* dst = (double*)avg.DataPointer;
                for (int y = 0; y < roiImg.Rows; y++)
                {
                    double sum = 0;
                    for (int x = 0; x < roiImg.Cols; x++)
                    {
                        sum += src[y * srcImg.Width + x];
                    }
                    dst[y] = sum / roiImg.Cols;
                }
            }
            roiImg.Dispose();
        }

        //unsafe private static void Range(out List<Rectangle> rtList, Mat avg, LASParameter param)
        //{
        //    rtList = new List<Rectangle>();
        //    List<int> ptList = new List<int>();
        //    ptList.Add(0);

        //    double diffRatio = param.LocalMaxRatio * 0.01;
        //    double dist = param.AcceptableDist;

        //    int ptListIdx = 0;
        //    double* pavg = (double*)avg.DataPointer;
        //    for (int i = 0; i < avg.Cols - 1; i++)
        //    {
        //        double cur = pavg[i];
        //        double next = pavg[i + 1];

        //        if (cur != next && diffRatio <= Math.Abs(cur - next) / Math.Max(cur, next))
        //        {
        //            if (i - ptList[ptListIdx] > dist)
        //            {
        //                ptList.Add(i);
        //                ptListIdx++;
        //            }
        //        }
        //    }

        //    ptList.Add(avg.Cols - 1);
        //    for (int i = 0; i < ptList.Count - 2; i++)
        //    {
        //        int left = (ptList[i + 1] + ptList[i]) / 2;
        //        int right = (ptList[i + 2] + ptList[i + 1]) / 2;

        //        rtList.Add(new Rectangle(left, 0, right - left, 0));
        //    }
        //}

        unsafe private static void Edge(out List<int> posList, Mat avg, List<Rectangle> rtList, double ratio)
        {
            posList = new List<int>();

            double* pavg = (double*)avg.DataPointer;
            double min = double.MaxValue;
            double max = double.MinValue;

            for (int i = 0; i < avg.Cols; i++)
            {
                min = Math.Min(pavg[i], min);
                max = Math.Max(pavg[i], max);
            }

            double prox = min + (max - min) * ratio * 0.01;

            foreach (Rectangle range in rtList)
            {
                for (int i = range.Left; i < range.Right; i++)
                {
                    if ((pavg[i] <= prox && prox < pavg[i + 1]) ||
                        (pavg[i] > prox && prox >= pavg[i + 1]))
                    {
                        posList.Add(i);
                        break;
                    }
                }
            }
        }
        unsafe private static void FitQuadratic(out List<double> fittedPosList, Mat avg, List<int> posList, int searchLength)
        {
            fittedPosList = new List<double>();

            Mat grad = new Mat(avg.Rows, avg.Cols, MatType.CV_64F);

            double* g = (double*)grad.DataPointer;
            double* pavg = (double*)avg.DataPointer;
            g[0] = 0;
            for (int i = 1; i < avg.Cols - 1; i++)
            {
                g[i] = pavg[i + 1] - pavg[i - 1];
            }
            g[grad.Cols - 1] = 0;

            foreach (int pos in posList)
            {
                double fittedPos;
                FitQuadratic(out fittedPos, grad, pos - searchLength, pos + searchLength);
                fittedPosList.Add(fittedPos);
            }

            grad.Dispose();
        }

        //unsafe public static void FindLinesEdgeBox(out List<LineInfo> lineList, Mat srcImg, Rectangle roi, LASParameter param)
        //{
        //    lineList = new List<LineInfo>(); // <== 뭔가 처리해야 됨

        //    // 1. ROI 내에서 avg
        //    Mat avg;
        //    Average(out avg, srcImg, roi, param.SearchDirection);

        //    double* pavg = (double*)avg.DataPointer;
        //    for (int i = 0; i < avg.Cols; i++)
        //    {
        //        System.Diagnostics.Debug.WriteLine("{0} : {1}", i, pavg[i]);
        //    }

        //    // 2. avg img에서 line 개수 파악
        //    List<Rectangle> rtList;
        //    Range(out rtList, avg, param);

        //    // 3. avg img에서 edge box 범위 설정
        //    // 4. 각 박스에 edge box 적용해서 point 받아옴
        //    List<int> posList;
        //    Edge(out posList, avg, rtList, param.MinLineRatio);

        //    // 중간 점검

        //    int add = 10;
        //    foreach (Rectangle rt in rtList)
        //    {
        //        if (param.SearchDirection == LASSearchDirection.Width)
        //        {
        //            srcImg.Line(new OpenCvSharp.CPlusPlus.Point(roi.Left + rt.Left, roi.Top + add), new OpenCvSharp.CPlusPlus.Point(roi.Left + rt.Right, roi.Top + add), new Scalar(255));
        //        }
        //        else
        //        {
        //            srcImg.Line(new OpenCvSharp.CPlusPlus.Point(roi.Left + add, roi.Top + rt.Left), new OpenCvSharp.CPlusPlus.Point(roi.Left + add, roi.Top + rt.Right), new Scalar(255));
        //        }
        //        add += 10;
        //    }

        //    foreach (int pos in posList)
        //    {
        //        if (param.SearchDirection == LASSearchDirection.Width)
        //        {
        //            srcImg.Line(new OpenCvSharp.CPlusPlus.Point(roi.Left + pos, roi.Top), new OpenCvSharp.CPlusPlus.Point(roi.Left + pos, roi.Bottom), new Scalar(255), 1);
        //        }
        //        else
        //        {
        //            srcImg.Line(new OpenCvSharp.CPlusPlus.Point(roi.Left, roi.Top + pos), new OpenCvSharp.CPlusPlus.Point(roi.Right, roi.Top + pos), new Scalar(255), 1);
        //        }
        //    }

        //    Mat test = new Mat(srcImg, new Rect(roi.Left, roi.Top, roi.Width, roi.Height));
        //    using (new OpenCvSharp.CPlusPlus.Window("test", test))
        //    {
        //        OpenCvSharp.CPlusPlus.Cv2.WaitKey();
        //    }

        //    // 5. 앞뒤로 pixel 가져와서 미분한 후 2차 곡선 피팅  ==> FitQuadratic(Mat avg, Point pt, out double position)
        //    List<double> fittedPosList;
        //    FitQuadratic(out fittedPosList, avg, posList, 4);

        //    foreach (double fittedPos in fittedPosList)
        //    {
        //        LineInfo li = new LineInfo();
        //        li.Gradient = 0;
        //        li.Intercept = fittedPos;

        //        if (param.SearchDirection == LASSearchDirection.Width)
        //        {
        //            // x = ay + b;
        //            li.Points.Add(new PointDouble(fittedPos, 0));
        //            li.Points.Add(new PointDouble(fittedPos, roi.Height));
        //        }
        //        else // Height
        //        {
        //            // y = ax + b;
        //            li.Points.Add(new PointDouble(0, fittedPos));
        //            li.Points.Add(new PointDouble(roi.Width, fittedPos));
        //        }
        //        lineList.Add(li);
        //    }
        //}
    }
}
