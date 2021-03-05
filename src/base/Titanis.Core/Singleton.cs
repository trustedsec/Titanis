namespace Titanis
{
	/// <summary>
	/// Facilitates the singleton pattern.
	/// </summary>
	public static class Singleton
	{
		static class InstanceHolder<T>
			where T : class
		{
			internal static T? instance;
		}

		/// <summary>
		/// Gets a singleton instance of an object.
		/// </summary>
		/// <typeparam name="T">Type of object</typeparam>
		/// <returns>A singleton instance of <typeparamref name="T"/>.</returns>
		public static T SingleInstance<T>()
			where T : class, new()
			=> (InstanceHolder<T>.instance ??= new T());
	}
}
