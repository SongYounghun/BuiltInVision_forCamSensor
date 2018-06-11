using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalvoScanner.LaserVision.OpenCV
{
    public class ProcessThreshold
    {
        OpenCVData m_parent = null;        

        public ProcessThreshold(OpenCVData parent)
        {
            m_parent = parent;
        }

        private int m_nProcessNum = -1;
        public int ProcessNum
        {
            get { return m_nProcessNum; }
            set { m_nProcessNum = value; }
        }

        private double m_nThreshold = 100;
        public double Threshold
        {
            get { return m_nThreshold; }
            set { m_nThreshold = value; }
        }

        private ThresholdType m_thresholdType = ThresholdType.Binary;
        public ThresholdType ThresholdType
        {
            get { return m_thresholdType; }
            set { m_thresholdType = value; }
        }

        public void ExcuteTheshold()
        {
            try
            {
                IplImage processImg = m_parent.GetIPLImageProcessing();
                IplImage thresholdImg = processImg.Clone();
                Cv.Threshold(processImg, thresholdImg, m_nThreshold, 255, m_thresholdType);
                m_parent.SetIPLImageProcessing(thresholdImg);
            }
            catch (Exception E)
            {
                throw E;
            }
        }
    }
}
