using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Titanis.Mocks;

namespace Titanis.Dynamic.Test
{
	[TestClass]
	public class UnitTest1
	{
		[TestMethod]
		public void TestAbstractMethod_NoReturn()
		{
			var mock = MockRepository.Default.Create<MyTestMock>();
			mock.Expect(r => r.AbstractMethod());
			mock.Object.AbstractMethod();
		}

		[TestMethod]
		public void TestAbstractMethod_ReturnInt32()
		{
			var mock = MockRepository.Default.Create<MyTestMock>();
			mock.Expect(r => r.AbstractMethod2()).Return(42);
			var actual = mock.Object.AbstractMethod2();

			Assert.AreEqual(42, actual);
		}

		[TestMethod]
		public void TestAbstractMethod_ReturnObject()
		{
			var mock = MockRepository.Default.Create<MyTestMock>();
			object expected = new object();
			mock.Expect(r => r.AbstractMethod3()).Return(expected);
			var actual = mock.Object.AbstractMethod3();

			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void TestVirtualMethod_CallBase()
		{
			var mock = MockRepository.Default.Create<MyTestMock>();
			mock.Expect(r => r.VirtualMethod1()).CallBase();
			mock.Object.VirtualMethod1();

			Assert.IsTrue(mock.Object.calledVirtualMethod1);
		}

		[TestMethod]
		public void TestVirtualMethod_CallBaseOut()
		{
			var mock = MockRepository.Default.Create<MyTestMockOutMethod>();

			int refInt = 1;
			mock.Expect(r => r.VirtualMethod(out refInt)).CallBase();

			int refActual = 1;
			var actual = mock.Object.VirtualMethod(out refActual);

			Assert.AreEqual(42, refActual);
			Assert.AreEqual(43, actual);
		}

		[TestMethod]
		public void TestVirtualMethod_CallBaseRef()
		{
			var mock = MockRepository.Default.Create<MyTestMockRefMethod>();

			int refInt = 1;
			mock.Expect(r => r.VirtualMethod(ref refInt)).CallBase();

			int refActual = 1;
			var actual = mock.Object.VirtualMethod(ref refActual);

			Assert.AreEqual(42, refActual);
			Assert.AreEqual(43, actual);
		}

		[TestMethod]
		public void TestVirtualMethod_AnyPattern()
		{
			var mock = MockRepository.Default.Create<MyTestMockAnyPattern>();

			mock.Expect(r => r.VirtualMethod(Arg.Any<int>()));
			mock.Object.VirtualMethod(-1);
		}

		static bool CompareFunc(int x) => true;

		[TestMethod]
		public void TestVirtualMethod_MatchesPattern()
		{
			var mock = MockRepository.Default.Create<MyTestMockAnyPattern>();


			mock.Expect(r => r.VirtualMethod(Arg.Matches<int>(CompareFunc)));
			mock.Object.VirtualMethod(-1);
		}

		[TestMethod]
		public void TestPropertySet_Constant()
		{
			var mock = MockRepository.Default.Create<MyTestMockProperty>();

			mock.ExpectSet(r => r.Property, 42);
			mock.Object.Property = 42;
		}

		[TestMethod]
		public void TestPropertySet_Func()
		{
			var mock = MockRepository.Default.Create<MyTestMockProperty>();

			mock.ExpectSet(r => r.Property, x => x == 42);
			mock.Object.Property = 42;
		}
	}

	public abstract class MyTestMockProperty
	{
		private int propValue;

		public virtual int Property
		{
			get { return propValue; }
			set { propValue = value; }
		}

	}

	public abstract class MyTestMockAnyPattern
	{
		public abstract void VirtualMethod(int arg);
	}

	public abstract class MyTestMockOutMethod
	{
		public virtual int VirtualMethod(out int refArg)
		{
			refArg = 42;
			return 43;
		}
	}

	public abstract class MyTestMockRefMethod
	{
		public virtual int VirtualMethod(ref int refArg)
		{
			refArg = 42;
			return 43;
		}
	}

	public abstract class MyTestMock
	{
		public abstract void AbstractMethod();
		public abstract int AbstractMethod2();
		public abstract object AbstractMethod3();

		public bool calledVirtualMethod1;

		public virtual void VirtualMethod1()
		{
			this.calledVirtualMethod1 = true;
		}
		public virtual int VirtualMethod3()
		{
			this.calledVirtualMethod1 = true;
			return 0;
		}

		public virtual void IntArg(int a)
		{

		}

		public virtual void IntRefArg(ref int a)
		{

		}
	}
}
