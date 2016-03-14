using NUnit.Framework;

using Landis.Landscape;

//  CS0649 warning is generated by the field "loc" not being assigned.
#pragma warning disable 649

namespace Landis.Test
{
	[TestFixture]
	public class RelativeLocation_Test
	{
		RelativeLocation loc;
		RelativeLocation loc_1234_987;
		
		//---------------------------------------------------------------------

		[TestFixtureSetUp]
		public void Init()
		{
			loc_1234_987 = new RelativeLocation(1234, 987);
		}

		//---------------------------------------------------------------------

		[Test]
		public void DefaultCtor_Row()
		{
			Assert.AreEqual(0, loc.Row);
		}

		//---------------------------------------------------------------------

		[Test]
		public void DefaultCtor_Column()
		{
			Assert.AreEqual(0, loc.Column);
		}

		//---------------------------------------------------------------------

		[Test]
		public void Ctor_Row()
		{
			Assert.AreEqual(1234, loc_1234_987.Row);
		}

		//---------------------------------------------------------------------

		[Test]
		public void Ctor_Column()
		{
			Assert.AreEqual(987, loc_1234_987.Column);
		}

		//---------------------------------------------------------------------

		[Test]
		public void Equality_SameLoc()
		{
			Assert.IsTrue(loc == loc);
			Assert.IsTrue(loc_1234_987 == loc_1234_987);
		}

		//---------------------------------------------------------------------

		[Test]
		public void Equality_DiffLocs()
		{
			Assert.IsFalse(loc == loc_1234_987);
		}

		//---------------------------------------------------------------------

		[Test]
		public void Inequality_SameLoc()
		{
			Assert.IsFalse(loc != loc);
			Assert.IsFalse(loc_1234_987 != loc_1234_987);
		}

		//---------------------------------------------------------------------

		[Test]
		public void Inequality_DiffLocs()
		{
			Assert.IsTrue(loc != loc_1234_987);
		}

		//---------------------------------------------------------------------

		[Test]
		public void Equals_Null()
		{
			Assert.IsFalse(loc.Equals(null));
		}

		//---------------------------------------------------------------------

		[Test]
		public void Equals_SameObj()
		{
			Assert.IsTrue(loc.Equals(loc));
			Assert.IsTrue(loc_1234_987.Equals(loc_1234_987));
		}

		//---------------------------------------------------------------------

		[Test]
		public void Equals_DiffObj()
		{
			Assert.IsFalse(loc.Equals(loc_1234_987));
		}

		//---------------------------------------------------------------------

		[Test]
		public void Equals_DiffObjSameValue()
		{
			RelativeLocation loc_0_0;
			Assert.IsTrue(loc.Equals(loc_0_0));
			Assert.IsTrue(loc_1234_987.Equals(new RelativeLocation(1234, 987)));
		}

		//---------------------------------------------------------------------

		[Test]
		public void HashCode()
		{
			Assert.AreEqual((int)(0 ^ 0), loc.GetHashCode());
			Assert.AreEqual((int)((int)1234 ^ (int)987),
			                loc_1234_987.GetHashCode());
		}

		//---------------------------------------------------------------------

		[Test]
		public void String()
		{
			Assert.AreEqual("(0, 0)", loc.ToString());
			Assert.AreEqual("(1234, 987)", loc_1234_987.ToString());
		}

		//---------------------------------------------------------------------

		[Test]
		public void NegativeValues()
		{
			RelativeLocation loc = new RelativeLocation(-22, -6543);
			Assert.AreEqual(-22, loc.Row);
			Assert.AreEqual(-6543, loc.Column);
		}
	}
}

#pragma warning restore 649
