using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Runtime.CompilerServices;
using System.Text;

namespace Titanis
{
	public static class ServiceExtensions
	{
		public static TService? GetService<TService>(this IServiceProvider services)
			where TService : class
		{
			return services?.GetService(typeof(TService)) as TService;
		}
		public static TService RequireService<TService>(this IServiceProvider services)
			where TService : class
		{
			if (services is null) throw new ArgumentNullException(nameof(services));

			var service = services.GetService(typeof(TService)) as TService;
			if (service == null)
				throw new InvalidOperationException($"The required service '{typeof(TService).FullName} is not available.");

			return service;
		}
	}
}
