using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLTConsole.Library
{
    class Profile
    {        
        private float downlink;

        public float Downlink
        {
            get { return downlink; }
            set { downlink = value; }
        }

        private float uplink;

        public float Uplink
        {
            get { return uplink; }
            set { uplink = value; }
        }

        private float totalfup;

        public float Totalfup
        {
            get { return totalfup; }
            set { totalfup = value; }
        }

        private float totalrem;

        public float Totalrem
        {
            get { return totalrem; }
            set { totalrem = value; }
        }

        private float peakfup;

        public float Peakfup
        {
            get { return peakfup; }
            set { peakfup = value; }
        }

        private float peakrem;

        public float PeakRem
        {
            get { return peakrem; }
            set { peakrem = value; }
        }
    }
}
