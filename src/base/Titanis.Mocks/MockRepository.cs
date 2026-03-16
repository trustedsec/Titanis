using System;
using System.Collections.Generic;
using System.Reflection;
using Titanis.Dynamic;

namespace Titanis.Mocks
{
	public class MockRepository
	{
		public MockRepository()
		{
			this._gen = new StubGenerator(new AssemblyName(MockAssemblySimpleName));
		}

		private const string MockAssemblySimpleName = "Titanis.Dynamic.Mocks";

		private StubGenerator _gen;

		private static MockRepository _defaultRepo;
		public static MockRepository Default => (_defaultRepo ??= new MockRepository());

		private Dictionary<Type, TypeInfo> _stubTypes = new Dictionary<Type, TypeInfo>();

		private List<Mock> _mocks = new List<Mock>();
		public Mock<T> Create<T>()
			where T : class
		{
			TypeInfo stubType = GetStubType<T>();
			var stub = Activator.CreateInstance(stubType.AsType());

			Mock<T> mock = new(MockBehavior.Loose, (T)stub);
			this._mocks.Add(mock);
			return mock;
		}

		private TypeInfo GetStubType<T>() where T : class
		{
			Type mockType = typeof(T);
			if (this._stubTypes.TryGetValue(mockType, out TypeInfo stubType))
				return stubType;

			Type baseType = (mockType.GetTypeInfo().IsClass)
				? mockType
				: typeof(object);

			string typeName = mockType.FullName + "Stub";

			Type[] interfaceTypes = (mockType.GetTypeInfo().IsInterface)
				? new Type[] { mockType }
				: Type.EmptyTypes;

			stubType = this._gen.GenerateTypeStub(baseType, typeName, interfaceTypes);
			this._stubTypes.Add(mockType, stubType);
			return stubType;
		}

		public Mock<T> Create<T>(params object[] costructorArgs)
			where T : class
		{
			return null;
		}

		/// <summary>
		/// Verifies that all expectations were met.
		/// </summary>
		public void VerifyExpectations()
		{
			List<Expectation> unmet = new List<Expectation>();
			foreach (var mock in this._mocks)
			{
				foreach (var expect in mock.expectations)
				{
					if (!expect.HasBeenMet)
						unmet.Add(expect);
				}
			}

			if (unmet.Count > 0)
			{
				throw new ExpectationException(unmet.ToArray());
			}
		}
	}
}
