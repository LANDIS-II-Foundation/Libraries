using Landis.Species;
using Landis.Landscape;

namespace Landis.Succession
{
	/// <summary>
	/// Seeding algorithm where every species can seed any site; a species does
	/// not even need to be present in any neighboring site.
	/// </summary>
	public class UniversalSeeding
		: ISeeding
	{
		public bool Seeds(ISpecies   species,
		           		  ActiveSite site)
		{
			return Reproduction.SufficientLight(species, site) &&
				   Reproduction.Establish(species, site);
		}
	}
}
