using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication
{
    public class ShipDetails
    {
        public string id
        {
            get;
            set;
        }

        public string name
        {
            get;
            set;
        }
        public string nearestportinKm
        {
            get;
            set;
        }

        public int velocity
        {
            get;
            set;
        }

        public double latitude
        {
            get;
            set;
        }
        public double longitude
        {
            get;
            set;
        }
        public string timeToReachNearestPortinHrs
        {
            get;
            set;
        }
    }

   

    public class PortDetail
    {
        public string portname
        {
            get;
            set;
        }

        public double latitude
        {
            get;
            set;
        }
        public double longitude
        {
            get;
            set;
        }
    }

    public class AddShip
    {
        public string name
        {
            get;
            set;
        }

        public int velocity
        {
            get;
            set;
        }

        public double latitude
        {
            get;
            set;
        }
        public double longitude
        {
            get;
            set;
        }
    }
}
