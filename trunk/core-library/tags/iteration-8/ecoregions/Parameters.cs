using Edu.Wisc.Forest.Flel.Util;

namespace Landis.Ecoregions
{
	/// <summary>
	/// The parameters for a single ecoregion.
	/// </summary>
	public class Parameters
		: IParameters
	{
		private string name;
		private string description;
		private byte mapCode;
		private bool active;

		//---------------------------------------------------------------------

		public string Name
		{
			get {
				return name;
			}
		}

		//---------------------------------------------------------------------

		public string Description
		{
			get {
				return description;
			}
		}

		//---------------------------------------------------------------------

		public byte MapCode
		{
			get {
				return mapCode;
			}
		}

		//---------------------------------------------------------------------

		public bool Active
		{
			get {
				return active;
			}
		}

		//---------------------------------------------------------------------

		public Parameters(string name,
		                  string description,
		                  byte mapCode,
		                  bool active)
		{
			this.name        = name;
			this.description = description;
			this.mapCode     = mapCode;
			this.active      = active;
		}

		//---------------------------------------------------------------------

		public Parameters(IParameters parameters)
		{
			name        = parameters.Name;
			description = parameters.Description;
			mapCode     = parameters.MapCode;
			active      = parameters.Active;
		}
	}
}
