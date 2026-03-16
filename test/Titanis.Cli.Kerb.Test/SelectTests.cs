using System.ComponentModel.Design;
using Titanis.Mocks;

namespace Titanis.Cli.Kerb.Test;

[TestClass]
public sealed class SelectTests : CliCommandTest<SelectCommand>
{
	/// <summary>
	/// Prefix for test files
	/// </summary>
	/// <remarks>
	/// This string deliberately begins with an invalid name so that if it is passed to normal file system functions, they will fail.
	/// </remarks>
	private const string VirtualFsPrefix = ":testdata";

	public TestContext TestContext { get; set; }

	[TestMethod]
	[CliTest("AllMilchickKirbi")]
	public async Task TestMethod1(Token[] args)
	{
		MockRepository mocks = new MockRepository();
		// Host services
		ServiceContainer services = new ServiceContainer();
		var mFileAccess = mocks.Create<IFileAccess>();
		services.AddService(typeof(IFileAccess), mFileAccess.Object);

		// Access to milchick*.kirbi
		mFileAccess.Expect(r => r.ResolveFsPath("milchick*.kirbi")).Return((string fileName) => Path.Combine(VirtualFsPrefix, fileName));
		mFileAccess.Expect(r => r.GetFiles(VirtualFsPrefix, "milchick*.kirbi")).Return(["milchick1.kirbi", "milchick2.kirbi"]);

		// Command context
		ReflectionMetadataResolver mdResolver = new ReflectionMetadataResolver();
		CommandMetadataContext mdContext = new CommandMetadataContext(mdResolver);
		ILog log = new TestLog(this.TestContext);

		var mContext = mocks.Create<ICommandContext>();
		mContext.Expect(r => r.MetadataContext).Return(mdContext);
		mContext.Expect(r => r.HostServices).Return(services);
		mContext.Expect(r => r.GetVariable(Arg.Any<string>())).Return(null);
		mContext.Expect(r => r.Log).Return(log);

		SelectCommand cmd = new SelectCommand();
		await cmd.InvokeAsync(mContext.Object, "Kerb select", args, 0, this.TestContext.CancellationToken);

		mocks.VerifyExpectations();
	}
}
