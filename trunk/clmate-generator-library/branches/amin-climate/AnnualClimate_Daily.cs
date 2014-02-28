﻿//  Copyright: Portland State University 2009-2014
//  Authors:  Robert M. Scheller, Amin Almassian

using Landis.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Landis.Library.Climate
{
    public class AnnualClimate_Daily: AnnualClimate
    {
        private bool isLeapYear;

        public int MaxDayInYear { get{return isLeapYear? 366 : 365; } } // = 366;

        public double[] DailyTemp = new double[366];
        public double[] DailyMinTemp = new double[366];
        public double[] DailyMaxTemp = new double[366];
        public double[] DailyPrecip = new double[366];
        public double[] DailyPAR = new double[366];
        public double[] DailyVarTemp = new double[366];
        public double[] DailyPptVarTemp = new double[366];
        //public int tempEcoIndex = -1;

        public double[] DailyPET = new double[366];  // Potential Evapotranspiration
        public double[] DailyVPD = new double[366];  // Vapor Pressure Deficit
        //public double[] DailyNdeposition = new double[366];
        public double[] DailyDayLength = new double[366];
        public double[] DailyNightLength = new double[366];
        public int[] DailyGDD = new int[366];

        //For Sequenced and Random timeStep arg should be passed
        public AnnualClimate_Daily(IEcoregion ecoregion, int actualYear, double latitude, Climate.Phase spinupOrfuture = Climate.Phase.Future_Climate, int timeStep = Int32.MinValue) 
        {

            this.climatePhase = spinupOrfuture;
            IClimateRecord[,] timestepData = new IClimateRecord[Climate.ModelCore.Ecoregions.Count, 366];
            //Climate.ModelCore.UI.WriteLine("  Calculating daily data ...  Ecoregion = {0}, Year = {1}, timestep = {2}.", ecoregion.Name, actualYear, timeStep);

            // ------------------------------------------------------------------------------------------------------
            // Case:  Monthly average data used for either spinup or future data
            if (timeStep == Int32.MinValue && Climate.ConfigParameters.ClimateTimeSeries.ToLower().Contains("average"))
            {
                //if (this.climatePhase == Climate.Phase.Future_Climate)
                //{
                    timestepData = AnnualClimate_Avg(ecoregion, actualYear, latitude); 
                //}
            }
            if (timeStep == Int32.MinValue && Climate.ConfigParameters.SpinUpClimateTimeSeries.ToLower().Contains("average"))
            {
                //if (this.climatePhase == Climate.Phase.SpinUp_Climate)
                //{
                    timestepData = AnnualClimate_Avg(ecoregion, actualYear, latitude); 
                //}
            }
            // ------------------------------------------------------------------------------------------------------
            // Case:  Monthly RANDOM or SEQUENCED data used for spinup or future data
            // (if average data, timestep = int.minvalue)
            if (timeStep != Int32.MinValue)
            {
                TimeStep = timeStep;
                try
                {
                    if (this.climatePhase == Climate.Phase.Future_Climate)
                    {
                        // ------------------------------------------------------------------------------------------------------
                        // Case:  Monthly RANDOM future data
                        // Presumption: The RandSelectedTimeSteps_future has been filled out in Climate.Initialize()
                        if (Climate.ConfigParameters.ClimateFileFormat.ToLower().Contains("random")) // a specific timeStep is provided but it points to an item in the preprocessed-randomly-selected-timesteps for returning the climate
                        {
                            if (Climate.RandSelectedTimeSteps_future == null)
                            {
                                Climate.ModelCore.UI.WriteLine("Error in creating new AnnualClimate: Climate library has not been initialized.");
                                throw new ApplicationException("Error in creating new AnnualClimate: Climate library has not been initialized.");
                            }
                            //timestepData = Climate.Future_AllData.ElementAt(Climate.RandSelectedTimeSteps_future[TimeStep]).Value;
                            timestepData = Climate.Future_AllData[Climate.RandSelectedTimeSteps_future[TimeStep]];
                        }
                        // ------------------------------------------------------------------------------------------------------
                        // Case:  Monthly SEQUENCED future data
                        // Presumption: The RandSelectedTimeSteps_future has been filled out in Climate.Initialize()
                        else //Sequenced
                        {
                            //timestepData = Climate.Future_AllData.ElementAt(TimeStep).Value;
                            timestepData = Climate.Future_AllData[TimeStep];
                        }

                    }
                    else if (this.climatePhase == Climate.Phase.SpinUp_Climate)
                    {
                        // ------------------------------------------------------------------------------------------------------
                        // Case:  Monthly RANDOM spinup data
                        // Presumption: The RandSelectedTimeSteps_future has been filled out in Climate.Initialize()
                        if (Climate.ConfigParameters.SpinUpClimateTimeSeries.ToLower().Contains("random"))
                        {
                            if (Climate.RandSelectedTimeSteps_spinup == null)
                            {
                                Climate.ModelCore.UI.WriteLine("Error in creating new AnnualClimate: Climate library has not been initialized.");
                                throw new ApplicationException("Error in creating new AnnualClimate: Climate library has not been initialized.");
                            }
                            //timestepData = Climate.Spinup_AllData.ElementAt(Climate.RandSelectedTimeSteps_spinup[TimeStep]).Value;
                            timestepData = Climate.Spinup_AllData[Climate.RandSelectedTimeSteps_spinup[TimeStep]];
                        }
                        // ------------------------------------------------------------------------------------------------------
                        // Case:  Monthly SEQUENCED spinup data
                        // Presumption: The RandSelectedTimeSteps_future has been filled out in Climate.Initialize()
                        else
                        {
                            //timestepData = Climate.Spinup_AllData.ElementAt(TimeStep).Value;
                            timestepData = Climate.Spinup_AllData[TimeStep];
                        }

                    }
                }
                catch (System.Collections.Generic.KeyNotFoundException ex)
                {
                    throw new ClimateDataOutOfRangeException("Exception: The requested Time-step or ecoregion is out of range of the provided " + this.climatePhase.ToString() + " input file. This might happened because the number of provided climate data is not devisable to the number of specified time-steps or there is not enoght historic climate data to run the model for the specified duration in scenario file.", ex);
                }
            }
                //if (this.climatePhase == Climate.Phase.Future_Climate)
                //{
                //    if (Climate.ConfigParameters.ClimateTimeSeries.ToLower().Contains("average"))
                //    {
                //        //Climate.ModelCore.UI.WriteLine("  Using FUTURE average daily data ...  Ecoregion = {0}, Year = {1}, Timestep = {2}, InputConfig = {3}.", ecoregion.Name, actualYear, timeStep, Climate.ConfigParameters.ClimateTimeSeries);
                //        timestepData = AnnualClimate_Avg(ecoregion, actualYear, latitude); //avgEcoClimate_future;
                //    }
                //    else if (Climate.ConfigParameters.ClimateFileFormat.ToLower().Contains("random")) // a specific timeStep is provided but it points to an item in the preprocessed-randomly-selected-timesteps for returning the climate
                //    {
                //        if (Climate.RandSelectedTimeSteps_future == null)
                //        {
                //            Climate.ModelCore.UI.WriteLine("Error in creating new AnnualClimate: Climate library has not been initialized.");
                //            throw new ApplicationException("Error in creating new AnnualClimate: Climate library has not been initialized.");
                //        }

                //        //Climate.ModelCore.UI.WriteLine("  Using random daily data ...  Ecoregion = {0}, Year = {1}, Timestep = {2}.", ecoregion.Name, actualYear, timeStep);
                //        //timestepData = Climate.Future_AllData.ElementAt(Climate.RandSelectedTimeSteps_future[TimeStep]).Value;
                //        timestepData = Climate.Future_AllData[Climate.RandSelectedTimeSteps_future[actualYear]];
                //    }
                //    else //Sequenced
                //    {
                //        //Climate.ModelCore.UI.WriteLine("  Using SEQUENCED daily data ...  Ecoregion = {0}, Year = {1}, Timestep = {2}.", ecoregion.Name, actualYear, timeStep);
                //        //timestepData = Climate.Future_AllData.ElementAt(TimeStep).Value;
                //        timestepData = Climate.Future_AllData[actualYear];

                //    }
                //}
                //else if (this.climatePhase == Climate.Phase.SpinUp_Climate)// && !Climate.ConfigParameters.SpinUpClimateTimeSeries.ToLower().Contains("monthly")) 
                //{
                //    if (Climate.ConfigParameters.SpinUpClimateTimeSeries.ToLower().Contains("average"))
                //    {

                //        //Climate.TimestepData = avgEcoClimate_spinUp;
                //        Climate.ModelCore.UI.WriteLine("  Using PAST average daily data ...  Ecoregion = {0}, Year = {1}, Timestep??? = {2}.", ecoregion.Name, actualYear, timeStep);
                //        timestepData = AnnualClimate_Avg(ecoregion, actualYear, latitude); //avgEcoClimate_spinUp;
                //    }
                //    else if (Climate.ConfigParameters.SpinUpClimateTimeSeries.ToLower().Contains("random"))
                //    {
                //        if (Climate.RandSelectedTimeSteps_spinup == null)
                //        {
                //            Climate.ModelCore.UI.WriteLine("Error in creating new AnnualClimate: Climate library has not been initialized.");
                //            throw new ApplicationException("Error in creating new AnnualClimate: Climate library has not been initialized.");
                //        }
                //        //Climate.TimestepData = Climate.Spinup_AllData.ElementAt(Climate.RandSelectedTimeSteps_spinup[TimeStep]).Value;
                //        timestepData = Climate.Spinup_AllData.ElementAt(Climate.RandSelectedTimeSteps_spinup[TimeStep]).Value;
                //    }
                //    else //Historic
                //    {
                //        //Climate.TimestepData = Climate.Spinup_AllData.ElementAt(TimeStep).Value;
                //        timestepData = Climate.Spinup_AllData.ElementAt(TimeStep).Value;
                //    }

                //}
                //    catch (System.Collections.Generic.KeyNotFoundException ex)
                //    {
                //        throw new ClimateDataOutOfRangeException("Exception: The requested Time-step or ecoregion is out of range of the provided " + this.climatePhase.ToString() + " input file. This might have happened because the number of provided climate data is not devisable to the number of specified time-steps or there is not enoght historic climate data to run the model for the specified duration in scenario file.", ex);
                //    }


                //}

                //else
                //{
                //    Climate.ModelCore.UI.WriteLine("Error in creating a new AnnualClimate: the There is an inconsistancy between the passed arguments and the parameters set up in the climate-input-file.");
                //    throw new ApplicationException("Error in creating a new AnnualClimate: the There is an inconsistancy between the passed arguments and the parameters set up in the climate-input-file.");
                //}

                IClimateRecord[] ecoClimate = new IClimateRecord[MaxDayInYear];

                this.Year = actualYear;
                this.AnnualPrecip = 0.0;
                //this.AnnualN = 0.0;

                for (int day = 0; day < MaxDayInYear; day++)
                {

                    ecoClimate[day] = timestepData[ecoregion.Index, day]; // Climate.TimestepData[ecoregion.Index, day];


                    //ecoClimate[day] = Climate.TimestepData[TimeStep, day];
                    if (ecoClimate[day] != null)
                    {
                        double DailyAvgTemp = (ecoClimate[day].AvgMinTemp + ecoClimate[day].AvgMaxTemp) / 2.0;

                        //Climate.ModelCore.UI.WriteLine("Timestep Data.  PPt={0}, T={1}.", ecoClimate[day].AvgPpt, DailyAvgTemp);
                        //double standardDeviation = ecoClimate[day].StdDevTemp * (Climate.ModelCore.GenerateUniform() * 2.0 - 1.0);

                        this.DailyTemp[day] = DailyAvgTemp; // +standardDeviation;
                        this.DailyMinTemp[day] = ecoClimate[day].AvgMinTemp; // +standardDeviation;
                        this.DailyMaxTemp[day] = ecoClimate[day].AvgMaxTemp; // +standardDeviation;
                        this.DailyPrecip[day] = Math.Max(0.0, ecoClimate[day].AvgPpt); // + (ecoClimate[day].StdDevPpt * (Climate.ModelCore.GenerateUniform() * 2.0 - 1.0)));
                        this.DailyPAR[day] = ecoClimate[day].PAR;

                        this.AnnualPrecip += this.DailyPrecip[day];

                        if (this.DailyPrecip[day] < 0)
                            this.DailyPrecip[day] = 0;

                        double hr = CalculateDayNightLength(day, latitude);
                        this.DailyDayLength[day] = (60.0 * 60.0 * hr);                  // seconds of daylight/day
                        this.DailyNightLength[day] = (60.0 * 60.0 * (24 - hr));         // seconds of nighttime/day

                        //this.DOY[day] = DayOfYear(day);
                    }
                    else
                    {
                        Climate.ModelCore.UI.WriteLine("Daily data = null.");
                    }
                }

                this.beginGrowing = CalculateBeginGrowingDay_Daily(); //ecoClimate);
                this.endGrowing = CalculateEndGrowingDay_Daily(ecoClimate);
                this.growingDegreeDays = GrowSeasonDegreeDays(actualYear);

                //if (Climate.TimestepData.GetLength(1) > 365)
                if (timestepData.GetLength(1) > 365)
                    this.isLeapYear = true;

            

        }


        private IClimateRecord[,] AnnualClimate_Avg(IEcoregion ecoregion, int year, double latitude)
        {
            IClimateRecord[,] timestepData = new IClimateRecord[Climate.ModelCore.Ecoregions.Count, 366];

            // check average or random
            //if (ecoregion.Index != tempEcoIndex)
            //{
            //    tempEcoIndex = ecoregion.Index;
            IClimateRecord[,] avgEcoClimate = new IClimateRecord[Climate.ModelCore.Ecoregions.Count, MaxDayInYear];
            IClimateRecord[,] ecoClimateT = new IClimateRecord[Climate.ModelCore.Ecoregions.Count, MaxDayInYear];

            for (int i = 0; i < MaxDayInYear; i++)
            {
                this.DailyMinTemp[i] = 0.0;
                this.DailyMaxTemp[i] = 0.0;
                this.DailyVarTemp[i] = 0.0;
                this.DailyPptVarTemp[i] = 0.0;
                this.DailyPrecip[i] = 0.0;
                this.DailyPAR[i] = 0.0;

            }

            int allDataCount = 0;
            if (this.climatePhase == Climate.Phase.Future_Climate)
                allDataCount = Climate.Future_AllData.Count;
            else if (this.climatePhase == Climate.Phase.SpinUp_Climate)
                allDataCount = Climate.Spinup_AllData.Count;

            for (int day = 0; day < MaxDayInYear; day++)
            {

                for (int stp = 0; stp < allDataCount; stp++)
                {
                    if (this.climatePhase == Climate.Phase.Future_Climate)
                        //Climate.TimestepData = Climate.Future_AllData.ElementAt(stp).Value;
                        timestepData = Climate.Future_AllData.ElementAt(stp).Value;
                    else if (this.climatePhase == Climate.Phase.SpinUp_Climate)
                        //Climate.TimestepData = Climate.Spinup_AllData.ElementAt(stp).Value;
                        timestepData = Climate.Spinup_AllData.ElementAt(stp).Value;

                    ecoClimateT[ecoregion.Index, day] = timestepData[ecoregion.Index, day]; //Climate.TimestepData[ecoregion.Index, day];

                    if (ecoClimateT[ecoregion.Index, day] != null)
                    {
                        this.DailyMinTemp[day] += ecoClimateT[ecoregion.Index, day].AvgMinTemp;
                        this.DailyMaxTemp[day] += ecoClimateT[ecoregion.Index, day].AvgMaxTemp;
                        this.DailyVarTemp[day] += ecoClimateT[ecoregion.Index, day].AvgVarTemp;
                        this.DailyPptVarTemp[day] += ecoClimateT[ecoregion.Index, day].AvgPptVarTemp;
                        this.DailyPrecip[day] += ecoClimateT[ecoregion.Index, day].AvgPpt;
                        this.DailyPAR[day] += ecoClimateT[ecoregion.Index, day].PAR;
                    }
                }
                this.DailyMinTemp[day] = this.DailyMinTemp[day] / allDataCount;
                this.DailyMaxTemp[day] = this.DailyMaxTemp[day] / allDataCount;
                this.DailyVarTemp[day] = this.DailyVarTemp[day] / allDataCount;
                this.DailyPptVarTemp[day] = this.DailyPptVarTemp[day] / allDataCount;
                this.DailyPrecip[day] = this.DailyPrecip[day] / allDataCount; //This DailyPrecip avg is the historic average so the average should be taken as opposed to summing that up.
                this.DailyPAR[day] = this.DailyPAR[day] / allDataCount;
                avgEcoClimate[ecoregion.Index, day] = new ClimateRecord();
                avgEcoClimate[ecoregion.Index, day].AvgMinTemp = this.DailyMinTemp[day];
                avgEcoClimate[ecoregion.Index, day].AvgMaxTemp = this.DailyMaxTemp[day];
                avgEcoClimate[ecoregion.Index, day].AvgVarTemp = this.DailyVarTemp[day];

                avgEcoClimate[ecoregion.Index, day].StdDevTemp = Math.Sqrt(DailyVarTemp[day]);

                avgEcoClimate[ecoregion.Index, day].AvgPptVarTemp = this.DailyPptVarTemp[day];
                avgEcoClimate[ecoregion.Index, day].AvgPpt = this.DailyPrecip[day];
                avgEcoClimate[ecoregion.Index, day].StdDevPpt = Math.Sqrt(this.DailyPrecip[day]);
                avgEcoClimate[ecoregion.Index, day].PAR = this.DailyPAR[day];
                //}

                //if (this.climatePhase == Climate.Phase.Future_Climate)
                //    avgEcoClimate_future = avgEcoClimate;
                //else if (this.climatePhase == Climate.Phase.SpinUp_Climate)
                //    avgEcoClimate_spinUp = avgEcoClimate;


                //Climate.TimestepData = avgEcoClimate;

            }
            return avgEcoClimate;
        }


        
        private int GetJulianMonthFromJulianDay(int yr, int mo, int d)
        {
            //System.Globalization.GregorianCalendar gc = new System.Globalization.GregorianCalendar();
            System.Globalization.JulianCalendar jc = new System.Globalization.JulianCalendar();
            return jc.GetMonth(new DateTime(yr,mo,d,jc));
        }



       

        //---------------------------------------------------------------------------
        private int CalculateBeginGrowingDay_Daily()
        //Calculate Begin Growing Degree Day (Last Frost; Minimum = 0 degrees C): 
        {
            double nightTemp = 0.0;
            int beginGrow = 162;
            for (int i = 1 ; i < 162; i++)  //Loop through all the days of the year from day 1 to day 162
            {
                nightTemp = this.DailyMinTemp[i];
                if (nightTemp > 0.0)
                {
                   // this.beginGrowing = i;
                    beginGrow = i;
                    break;
                }
            }
            //Climate.ModelCore.UI.WriteLine("  Calculating daily begin growing season day...{0}", beginGrow);

            return beginGrow;
           // return MaxDayInYear; // For the time being if no night could be find with the Temp. > 0 then 0 is returned. A result of this could be that no growth would occure.
        }

        //---------------------------------------------------------------------------
        private int CalculateEndGrowingDay_Daily(IClimateRecord[] annualClimate)//, Random autoRand)
        //Calculate End Growing Degree Day (First frost; Minimum = 0 degrees C):
        {
            double nightTemp = 0.0;
            //int beginGrowingDay = CalculateBeginGrowingDay_Daily(annualClimate);
            int endGrowingDay = MaxDayInYear;
            //int i = beginGrowingDay;
            for (int day = MaxDayInYear; day > this.BeginGrowing; day--)  //Loop through all the days of the year from day 1 to day 162
            {
                nightTemp = this.DailyMinTemp[day];
                if (nightTemp > 0)
                {
                    //this.endGrowing = i;
                    //endGrowingDay = i;
                    return day;
                }
            }
             
            return 0;
        }

       

         //---------------------------------------------------------------------------
        public int GrowSeasonDegreeDays(int currentYear)
        //Calc growing season degree days (Degree_Day) based on monthly temperatures
        //normally distributed around a specified mean with a specified standard
        //deviation.
        {
           
            
            //degDayBase is temperature (C) above which degree days (Degree_Day)
            //are counted
            double degDayBase = 5.56;      // 42F.

            double Deg_Days = 0.0;

            //Calc monthly temperatures (mean +/- normally distributed
            //random number times standard deviation) and
            //sum degree days for consecutve months.
            for (int i = 0; i < 12 ; i++) //12 months in year
            {
                //I talked to Melissa and Allec  and we decided to use the difference between Begin and End growing days for the GrowSeasonDegreeDays. 
                //if (DailyTemp[i] > degDayBase)
                    Deg_Days += (DailyTemp[i] - degDayBase);
            }
            this.growingDegreeDays = (int)Deg_Days;
            return (int)Deg_Days;
            
        }

        //---------------------------------------------------------------------------
        //public void WriteToLogFile()
        //{ 
        //    //(IEcoregion ecoregion, int actualYear, double latitude, ClimatePhase spinupOrfuture = Climate.Phase.Future_Climate, int timeStep = Int32.MinValue)
        //}

    }
}
