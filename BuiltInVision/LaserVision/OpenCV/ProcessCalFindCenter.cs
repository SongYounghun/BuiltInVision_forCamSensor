using OpenCvSharp;
using OpenCvSharp.Blob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalvoScanner.LaserVision.OpenCV
{
    public class ProcessCalFindCenter
    {
        OpenCVData m_parent = null;
        public ProcessCalFindCenter(OpenCVData parent)
        {
            m_parent = parent;
        }

        private IplImage m_pImageFindCenter = null;
        public IplImage GetIPLImageFindCenter()
        {
            return m_pImageFindCenter;
        }

        public void CopyImageFindCenterFromOriginal()
        {
            try
            {
                if (m_parent != null)
                {
                    if (m_parent.GetIPLImage() != null)
                    {
                        Cv.Copy(m_parent.GetIPLImage(), m_pImageFindCenter);
                    }
                }
            }
            catch (Exception E)
            {
                throw E;
            }
        }

        public void ConvertGrayInageFindCenter()
        {
            try
            {
                if (m_pImageFindCenter != null)
                {
                    IplImage grayImg = Cv.CreateImage(Cv.GetSize(m_pImageFindCenter), BitDepth.U8, 1);
                    Cv.Copy(grayImg, m_pImageFindCenter);
                    Cv.CvtColor(m_pImageFindCenter, grayImg, ColorConversion.RgbToGray);
                    grayImg.Dispose();

                    CvBlobs blobs = new CvBlobs();
                }
            }
            catch (Exception E)
            {
                throw E;
            }
        }
    }
}
