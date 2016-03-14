using NUnit.Framework;

using Landis.Landscape;

//  CS0649 warning is generated by the field "dims" not being assigned.
//	CS1718 warning is generated by the tests of == operator using the same
//	variable as both operands
#pragma warning disable 649, 1718

namespace Landis.Test
{
	[TestFixture]
	public class GridDimensions_Test
	{
		GridDimensions dims;
		GridDimensions dims_4321_789;
		
		//---------------------------------------------------------------------

		[SetUp]
		public void Init()
		{
			dims_4321_789 = new GridDimensions(4321, 789);
		}

		//---------------------------------------------------------------------

		[Test]
		public void Test01_DefaultCtorCheckRows()
		{
			Assert.AreEqual(0, dims.Rows);
		}

		//---------------------------------------------------------------------

		[Test]
		public void Test02_DefaultCtorCheckColumns()
		{
			Assert.AreEqual(0, dims.Columns);
		}

		//---------------------------------------------------------------------

		[Test]
		public void Test03_CheckRows()
		{
			Assert.AreEqual(4321, dims_4321_789.Rows);
		}

		//---------------------------------------------------------------------

		[Test]
		public void Test04_CheckColumns()
		{
			Assert.AreEqual(789, dims_4321_789.Columns);
		}

		//---------------------------------------------------------------------

		[Test]
		public void Test05_EqualityWithSameLoc()
		{
			Assert.IsTrue(dims == dims);
			Assert.IsTrue(dims_4321_789 == dims_4321_789);
		}

		//---------------------------------------------------------------------

		[Test]
		public void Test06_EqualityWithDiffLocs()
		{
			Assert.IsFalse(dims == dims_4321_789);
		}

		//---------------------------------------------------------------------

		[Test]
		public void Test07_InequalityWithSameLoc()
		{
			Assert.IsFalse(dims != dims);
			Assert.IsFalse(dims_4321_789 != dims_4321_789);
		}

		//---------------------------------------------------------------------

		[Test]
		public void Test08_InequalityWithDiffLocs()
		{
			Assert.IsTrue(dims != dims_4321_789);
		}

		//---------------------------------------------------------------------

		[Test]
		public void Test09_EqualsWithNull()
		{
			Assert.IsFalse(dims.Equals(null));
		}

		//---------------------------------------------------------------------

		[Test]
		public void Test10_EqualsWithSameObj()
		{
			Assert.IsTrue(dims.Equals(dims));
			Assert.IsTrue(dims_4321_789.Equals(dims_4321_789));
		}

		//---------------------------------------------------------------------

		[Test]
		public void Test11_EqualsWithDiffObj()
		{
			Assert.IsFalse(dims.Equals(dims_4321_789));
		}

		//---------------------------------------------------------------------

		[Test]
		public void Test12_EqualsWithDiffObjSameValue()
		{
			GridDimensions dims_0_0 = new GridDimensions();
			Assert.IsTrue(dims.Equals(dims_0_0));
			Assert.IsTrue(dims_4321_789.Equals(new GridDimensions(4321, 789)));
		}

		//---------------------------------------------------------------------

		[Test]
		public void Test13_GetHashCode()
		{
			Assert.AreEqual((int)(0 ^ 0), dims.GetHashCode());
			Assert.AreEqual((int)((uint)4321 ^ (uint)789),
			                dims_4321_789.GetHashCode());
		}

		//---------------------------------------------------------------------

		[Test]
		public void Test14_ToString()
		{
			Assert.AreEqual("0 rows by 0 columns", dims.ToString());
			Assert.AreEqual("4,321 rows by 789 columns",
			                dims_4321_789.ToString());
			Assert.AreEqual("1 row by 1 column",
			                new GridDimensions(1, 1).ToString());
			Assert.AreEqual("5 rows by 1 column",
			                new GridDimensions(5, 1).ToString());
			Assert.AreEqual("1 row by 66 columns",
			                new GridDimensions(1, 66).ToString());
		}
	}
}

#pragma warning restore 649, 1718
