using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
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

		public Mock<T> Create<T>()
			where T : class
		{
			TypeInfo stubType = GetStubType<T>();
			var stub = Activator.CreateInstance(stubType.AsType());

			return new Mock<T>((T)stub, MockBehavior.Loose);
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
	}
}
