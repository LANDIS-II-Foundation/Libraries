//  Copyright: Portland State University 2009-2014
//  Authors:  Robert M. Scheller, Amin Almassian

using System.Collections.Generic;

namespace Landis.Library.Climate
{
    /// <summary>
    /// Weather parameters for each month/day
    /// </summary>
    public class ClimateRecord
    {

        private double avgMinTemp;
        private double avgMaxTemp;
        private double stdDevTemp;
        private double avgVarTemp;
        private double avgPpt;
        private double avgVarPpt;
        private double stdDevPpt;
        private double avgPAR;
        private double avgRH;
        private double stdDevRH;
        private double avgVarRH;
        private double avgWindSpeed;
        private double stdDevWindSpeed;
        private double avgVarWindSpeed;
        private double avgNDeposition;
        private double stdDevNDeposition;
        private double avgVarNDeposition;
        

        
        public double AvgMinTemp
        {
            get {
                return avgMinTemp;
            }
            set {
                avgMinTemp = value;
            }
        }

        public double AvgMaxTemp
        {
            get {
                return avgMaxTemp;
            }
            set {
                avgMaxTemp = value;
            }
        }
        public double StdDevTemp
        {
            get {
                return stdDevTemp;
            }
            set {
                stdDevTemp = value;
            }
        }
        public double AvgPpt
        {
            get {
                return avgPpt;
            }
            set {
                avgPpt = value;
            }
        }
        public double StdDevPpt
        {
            get {
                return stdDevPpt;
            }
            set {
                stdDevPpt = value;
            }
        }
        public double AvgPAR
        {
            get {
                return avgPAR;
            }
            set {
                avgPAR = value;
            }
        }

        public double AvgVarTemp
        {
            get
            {
                return avgVarTemp;
            }
            set
            {
                avgVarTemp = value;
            }
        }
        public double AvgVarPpt
        {
            get
            {
                return avgVarPpt;
            }
            set
            {
                avgVarPpt = value;
            }
        }

        public double AvgRH
        {
            get
            {
                return avgRH;
            }
            set
            {
                avgRH = value;
            }
        }
        public double StdDevRH
        {
            get
            {
                return stdDevRH;
            }
            set
            {
                stdDevRH = value;
            }
        }
        public double AvgVarRH
        {
            get
            {
                return avgVarRH;
            }
            set
            {
                avgVarRH = value;
            }
        }

        public double AvgWindSpeed
        {
            get
            {
                return avgWindSpeed;
            }
            set
            {
                avgWindSpeed = value;
            }
        }
        public double StdDevWindSpeed
        {
            get
            {
                return stdDevWindSpeed;
            }
            set
            {
                stdDevWindSpeed = value;
            }
        }
        public double AvgVarWindSpeed
        {
            get
            {
                return avgVarWindSpeed;
            }
            set
            {
                avgVarWindSpeed = value;
            }
        }


        public double AvgNDeposition
        {
            get
            {
                return avgNDeposition;
            }
            set
            {
                avgNDeposition = value;
            }
        }

        public double AvgVarNdeposition
        {
            get
            {
                return avgVarNDeposition;
            }
            set
            {
                avgVarNDeposition = value;
            }
        }

        public double StdDevNDeposition
        {
            get
            {
                return stdDevNDeposition;
            }
            set
            {
                stdDevNDeposition = value;
            }
        }
        
      

        public ClimateRecord(double avgMinTemp, double avgMaxTemp, double stdDevTemp, double avgPpt, double stdDevPpt, double avgPAR, double avgVarTemp, double avgPptVarTemp, double avgRH,
                            double avgVarRH, double stdDevRH, double avgWindSpeed, double avgVarWindSpeed, double stdDevWindSpeed, double avgNDeposition, double avgVarNDeposition, double stdDevNDeposition)
        {
            this.avgMinTemp = avgMinTemp;
            this.avgMaxTemp = avgMaxTemp;
            this.stdDevTemp = stdDevTemp;
            this.avgPpt = avgPpt;
            this.stdDevPpt = stdDevPpt;
            this.avgPAR = avgPAR;
            this.avgVarTemp = avgVarTemp;
            this.avgVarPpt = avgVarPpt;
            this.avgRH = avgRH;
            this.avgVarRH = avgVarRH;
            this.stdDevRH = stdDevRH;
            this.avgWindSpeed = avgWindSpeed;
            this.avgVarWindSpeed = avgVarWindSpeed;
            this.stdDevWindSpeed = stdDevWindSpeed;
            this.avgNDeposition = avgNDeposition;
            this.avgVarNDeposition = avgVarNDeposition;
            this.stdDevNDeposition = stdDevNDeposition;
        }
        
        public ClimateRecord()
        {
            this.avgMinTemp = -99.0;
            this.avgMaxTemp = -99.0;
            this.stdDevTemp = -99.0;
            this.avgPpt = -99.0;
            this.stdDevPpt = -99.0;
            this.avgPAR = -99.0;
            this.avgVarTemp = -99.0;
            this.avgVarPpt = -99.0;
            this.avgRH = -99.0;
            this.avgVarRH = -99.0;
            this.stdDevRH = -99.0;
            this.avgWindSpeed = -99.0;
            this.avgVarWindSpeed = -99.0;
            this.stdDevWindSpeed = -99.0;
            this.avgNDeposition = -99.0;
            this.avgVarNDeposition = -99.0;
            this.stdDevNDeposition = -99.0;
        }
    }
}
