﻿//  Copyright: Portland State University 2009-2014
//  Authors:  Robert M. Scheller, John McNabb and Amin Almassian

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Landis.Library.Climate
{
    public class ClimateFileFormatProvider
    {
        private string format;
        private TemporalGranularity timeStep;
        private List<string> maxTempTriggerWord;
        private List<string> minTempTriggerWord;
        private List<string> precipTriggerWord;
        private List<string> windDirectionTriggerWord;
        private List<string> windSpeedTriggerWord;
        private List<string> nDepositionTriggerWord;
        private List<string> co2TriggerWord;

        private const double ABS_ZERO = -273.15;
            
        //------
        public TemporalGranularity InputTimeStep { get { return this.timeStep; } }
        public List<string> MaxTempTriggerWord { get { return this.maxTempTriggerWord; } }
        public List<string> MinTempTriggerWord { get { return this.minTempTriggerWord; } }
        public List<string> PrecipTriggerWord { get { return this.precipTriggerWord; } }
        public List<string> WindDirectionTriggerWord { get { return this.windDirectionTriggerWord; } }
        public List<string> WindSpeedTriggerWord { get { return this.windSpeedTriggerWord; } }
        public List<string> NDepositionTriggerWord { get { return this.nDepositionTriggerWord; } }
        public List<string> CO2TriggerWord { get { return this.co2TriggerWord; } }
        public string SelectedFormat { get { return format; } }
  
        // JM: properties for transformations
        public double PrecipTransformation { get; private set; }
        public double TemperatureTransformation { get; private set; }

        //------
        public ClimateFileFormatProvider(string format)
        {
            this.format = format;

            // default trigger words
            this.maxTempTriggerWord = new List<string>() { "maxTemp", "Tmax" };
            this.minTempTriggerWord = new List<string>() { "minTemp", "Tmin"};
            this.precipTriggerWord = new List<string>() { "ppt", "precip", "Prcp" };
            this.windDirectionTriggerWord = new List<string>() { "windDirect", "wd" , "winddirection"};
            this.windSpeedTriggerWord = new List<string>() { "windSpeed", "ws" };
            this.nDepositionTriggerWord = new List<string>() { "Ndeposition", "Ndep" };
            this.co2TriggerWord = new List<string>() { "CO2", "CO2conc" };

            //IMPORTANT FOR ML:  Need to add these as optional trigger words.
            //this.precipTriggerWord = "Prcp";
            //    this.maxTempTriggerWord = "Tmax";
            //    this.minTempTriggerWord = "Tmin";

            // Transformations used for all formats that have temps in C and precip in mm
            this.PrecipTransformation = 0.1;        // Assumes data is in mm and so it converts the data from mm to cm.  
            this.TemperatureTransformation = 0.0;   // Assumes data is in degrees Celsius so no transformation is needed.

            //this.timeStep = ((this.format == "PRISM") ? TemporalGranularity.Monthly : TemporalGranularity.Daily);
            switch (this.format.ToLower())
            {
                case "daily_temp-c_precip-mmday":  //was 'gfdl_a1fi' then ipcc3_daily
                    this.timeStep = TemporalGranularity.Daily;
                    break;

                case "monthly_temp-c_precip-mmmonth":  // was ipcc3_monthly

                    this.timeStep = TemporalGranularity.Monthly;
                    break;

                case "monthly_temp-k_precip-mmsec":
                    this.timeStep = TemporalGranularity.Monthly;
                    this.TemperatureTransformation = ABS_ZERO;      // ipcc5 temp. data are in Kelvin.
                    this.PrecipTransformation = 262974.6;            // ipcc5 precip. data are in kg / m2 / sec -> convert to cm / month
                    break;

                case "daily_temp-k_precip-mmsec":  //add
                    this.timeStep = TemporalGranularity.Daily;
                    this.TemperatureTransformation = ABS_ZERO;      // ipcc5 temp. data are in Kelvin.
                    this.PrecipTransformation = 8640.0;             // ipcc5 precip. data are in kg / m2 / sec -> convert to cm / day
                    break;

                //case "prism_monthly":  //was 'prism'
                //    this.timeStep = TemporalGranularity.Monthly;
                //    break;

                //case "mauer_daily":  //was griddedobserved
                //    this.timeStep = TemporalGranularity.Daily;
                //    this.precipTriggerWord = "Prcp";
                //    this.maxTempTriggerWord = "Tmax";
                //    this.minTempTriggerWord = "Tmin";
                //    // RH and wind speed for Mauer are the default trigger words
                    //break;

                default:
                    Climate.ModelCore.UI.WriteLine("Error in ClimateFileFormatProvider: the given \"{0}\" file format is not supported.", this.format);
                    throw new ApplicationException("Error in ClimateFileFormatProvider: the given \"" + this.format + "\" file format is not supported.");
                   
            }
        }

       


    }
}
