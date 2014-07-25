//  Copyright 2005-2010 Portland State University, University of Wisconsin
//  Authors:  Robert M. Scheller, James B. Domingo

using Landis.SpatialModeling;
using Landis.Core;
using Edu.Wisc.Forest.Flel.Util;

namespace Landis.Library.BiomassCohortsPnET
{
    /// <summary>
    /// A species cohort with biomass information.
    /// </summary>
    public interface ICohort : Landis.Library.BiomassCohorts.ICohort
    {
        /// <summary>
        /// The cohort's biomass (g m-2).
        /// </summary>

       
        float FolShed
        {
            get;
            set;
        }
        float Wood
        {
            get;
            set;
        }
        float Fol
        {
            get;
            set;
        }
        float Root
        {
            get;
            set;
        }
        float NSC
        {
            get;
            set;
        }
         
       
        //void IncrementAge();
         
         
        //---------------------------------------------------------------------

        
        
    }
}
