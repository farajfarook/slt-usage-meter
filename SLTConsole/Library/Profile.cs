using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLTConsole.Library
{
    public class Profile
    {        
        private float downlink = 0f;

        public float Downlink
        {
            get { return downlink; }
            set { downlink = value; }
        }

        private float uplink = 0f;

        public float Uplink
        {
            get { return uplink; }
            set { uplink = value; }
        }

        private float totalfup = 0f;

        public float Totalfup
        {
            get { return totalfup; }
            set { totalfup = value; }
        }

        private float totalrem = 0f;

        public float Totalrem
        {
            get { return totalrem; }
            set { totalrem = value; }
        }

        private float peakfup = 0f;

        public float Peakfup
        {
            get { return peakfup; }
            set { peakfup = value; }
        }

        private float peakrem = 0f;

        public float Peakrem
        {
            get { return peakrem; }
            set { peakrem = value; }
        }

        public float PeakRemGb
        {
            get { return peakrem / 1073741824; }
        }

        public float PeakGb
        {
            get { return PeakRemGb * 100 / (100 - peakfup); }
        }

        public float TotalRemGb
        {
            get { return totalrem / 1073741824; }
        }

        public float TotalGb
        {
            get { return TotalRemGb * 100 / (100 - totalfup); }
        }

        public String PeakStatus
        {
            get { return PeakRemGb.ToString("0.0") + " / " + PeakGb.ToString("0.0") + " GBs"; }
        }

        public String TotalStatus
        {
            get { return TotalRemGb.ToString("0.0") + " / " + TotalGb.ToString("0.0") + " GBs"; }
        }
    }
}
