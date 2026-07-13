# Services and Dependencies
Rather than interacting with the operating system services directly, some components within the framework delegate to service objects.  This allows the caller to provide a service implementation that intercepts the call and handle it differently.

These services are implemented in `Titanis.Net`:

* `ISocketService` - Provides network sockets
* `INameResolver` - Resolves a host name to an IP address
* `IClientCredentialService` - Provides client credentials

The following implementations are provided:

* `ClientCredentialService` - Implements a credential store as a dictionary.
* `DictionaryNameResolver` - Implements a name resolver as a dictionary lookup.
* `PlatformNameResolverService` - Forwards requests to the operating system via `Dns`
* `PlatformSocketService` - Creates a `PlatformSocket` that wraps a `Socket`
