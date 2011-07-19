using Edu.Wisc.Forest.Flel.Util;
using System.Collections.Generic;

namespace Landis.Species
{
	/// <summary>
	/// A parser that reads a dataset of species parameter from text input.
	/// </summary>
	public class DatasetParser
		: Landis.TextParser<IDataset>
	{
		public override string LandisDataValue
		{
			get {
				return "Species";
			}
		}

		//---------------------------------------------------------------------

		public DatasetParser()
		{
		}

		//---------------------------------------------------------------------

		protected override IDataset Parse()
		{
			ReadLandisDataVar();

			IEditableDataset dataset = new EditableDataset();
			Dictionary <string, int> lineNumbers = new Dictionary<string, int>();

			InputVar<string> name = new InputVar<string>("Name");
			InputVar<int> longevity = new InputVar<int>("Longevity");
			InputVar<int> maturity = new InputVar<int>("Sexual Maturity");
			InputVar<byte> shadeTolerance = new InputVar<byte>("Shade Tolerance");
			InputVar<byte> fireTolerance = new InputVar<byte>("Fire Tolerance");
			InputVar<int> effectiveSeedDist = new InputVar<int>("Effective Seed Dist");
			InputVar<int> maxSeedDist = new InputVar<int>("Max Seed Dist");
			InputVar<float> vegReprodProb = new InputVar<float>("Vegetative Reprod Prob");
			InputVar<int> minSproutAge = new InputVar<int>("Min Sprout Age");
			InputVar<int> maxSproutAge = new InputVar<int>("Max Sprout Age");
			InputVar<bool> serotiny = new InputVar<bool>("Serotiny");

			while (! AtEndOfInput) {
				IEditableParameters parameters = new EditableParameters();
				dataset.Add(parameters);

				StringReader currentLine = new StringReader(CurrentLine);

				ReadValue(name, currentLine);
				int lineNumber;
				if (lineNumbers.TryGetValue(name.Value.Actual, out lineNumber))
					throw new InputValueException(name.Value.String,
					                              "The name \"{0}\" was previously used on line {1}",
					                              name.Value.Actual, lineNumber);
				else
					lineNumbers[name.Value.Actual] = LineNumber;
				parameters.Name = name.Value;

				ReadValue(longevity, currentLine);
				parameters.Longevity = longevity.Value;

				ReadValue(maturity, currentLine);
				parameters.Maturity = maturity.Value;

				ReadValue(shadeTolerance, currentLine);
				parameters.ShadeTolerance = shadeTolerance.Value;

				ReadValue(fireTolerance, currentLine);
				parameters.FireTolerance = fireTolerance.Value;

				ReadValue(effectiveSeedDist, currentLine);
				parameters.EffectiveSeedDist = effectiveSeedDist.Value;

				ReadValue(maxSeedDist, currentLine);
				parameters.MaxSeedDist = maxSeedDist.Value;

				ReadValue(vegReprodProb, currentLine);
				parameters.VegReprodProb = vegReprodProb.Value;

				ReadValue(minSproutAge, currentLine);
				parameters.MinSproutAge = minSproutAge.Value;

				ReadValue(maxSproutAge, currentLine);
				parameters.MaxSproutAge = maxSproutAge.Value;

				ReadValue(serotiny, currentLine);
				parameters.Serotiny = serotiny.Value;

				CheckNoDataAfter("the " + serotiny.Name + " column",
				                 currentLine);
				GetNextLine();
			}

			return dataset.GetComplete();
		}
	}
}
