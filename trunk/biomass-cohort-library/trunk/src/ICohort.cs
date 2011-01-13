//  Copyright 2005-2010 Portland State University, University of Wisconsin
//  Authors:  Srinivas S., Robert M. Scheller, James B. Domingo

using Landis.Cohorts;
using Landis.SpatialModeling;
//using Landis.SpatialModeling.CoreServices;

namespace Landis.Library.BiomassCohorts
{
    /// <summary>
    /// A species cohort with biomass information.
    /// </summary>
    public interface ICohort
        : Landis.Library.AgeOnlyCohorts.ICohort
    {
        /// <summary>
        /// The cohort's biomass (units?).
        /// </summary>
        int Biomass
        {
            get;
        }

        //ushort Age
        //{
        //    get;
        //}

        //---------------------------------------------------------------------

        /// <summary>
        /// Computes how much of the cohort's biomass is non-woody.
        /// </summary>
        /// <param name="site">
        /// The site where the cohort is located.
        /// </param>
        int ComputeNonWoodyBiomass(ActiveSite site);
        
    }
}
