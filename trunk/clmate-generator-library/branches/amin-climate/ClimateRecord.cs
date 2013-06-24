//  Copyright 2009 Conservation Biology Institute
//  Authors:  Robert M. Scheller
//  License:  Available at  
//  http://www.landis-ii.org/developers/LANDIS-IISourceCodeLicenseAgreement.pdf

using System.Collections.Generic;

namespace Landis.Library.Climate
{
    /// <summary>
    /// Weather parameters for each month.
    /// </summary>
    public interface IClimateRecord
    {

        double AvgMinTemp{get;set;}
        double AvgMaxTemp{get;set;}
        double StdDevTemp{get;set;}
        double AvgPpt{get;set;}
        double StdDevPpt{get;set;}
        double PAR{get;set;}
        double AvgVarTemp { get; set; }
        double AvgPptVarTemp { get; set; }
        
    }

    public class ClimateRecord
    : IClimateRecord
    {

        private double avgMinTemp;
        private double avgMaxTemp;
        private double stdDevTemp;
        private double avgPpt;
        private double stdDevPpt;
        private double par;
        private double avgVarTemp;
        private double avgPptVarTemp;
        
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
        public double PAR
        {
            get {
                return par;
            }
            set {
                par = value;
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
        public double AvgPptVarTemp
        {
            get
            {
                return avgPptVarTemp;
            }
            set
            {
                avgPptVarTemp = value;
            }
        }
        
        public ClimateRecord(
                            double avgMinTemp,
                            double avgMaxTemp,
                            double stdDevTemp,
                            double avgPpt,
                            double stdDevPpt,
                            double par,
                            double avgVarTemp,
                            double avgPptVarTemp
                            )
        {
            this.avgMinTemp = avgMinTemp;
            this.avgMaxTemp = avgMaxTemp;
            this.stdDevTemp = stdDevTemp;
            this.avgPpt = avgPpt;
            this.stdDevPpt = stdDevPpt;
            this.par = par;
            this.avgVarTemp = avgVarTemp;
            this.avgPptVarTemp = avgPptVarTemp;

        }
        
        public ClimateRecord()
        {
            this.avgMinTemp = -99.0;
            this.avgMaxTemp = -99.0;
            this.stdDevTemp = -99.0;
            this.avgPpt = -99.0;
            this.stdDevPpt = -99.0;
            this.par = -99.0;
            this.avgVarTemp = -99.0;
            this.avgPptVarTemp = -99.0;

        }
        
    }
}
