using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Titanis.Cli;
using static System.Net.Mime.MediaTypeNames;

namespace Titanis;

public class TestCommandContext : CommandContextBase, ICommandContext
{
	public TestCommandContext(TestContext testContext, IServiceContainer hostServices)
		: base(new CommandMetadataContext(new ReflectionMetadataResolver()))
	{
		this._testContext = testContext;
		this._hostServices = hostServices;
		this.Log = new TestLog(testContext);
		hostServices.AddService(typeof(ILog), this.Log);
	}

	private readonly TestContext _testContext;
	private readonly IServiceContainer _hostServices;

	public ITerminal Terminal => throw new NotImplementedException();

	public string WorkingDirectory => throw new NotImplementedException();

	public IServiceProvider HostServices => this._hostServices;


	public override ILog Log { get; }

	public bool OutputFlushed { get; private set; }
	public override void FlushOutput()
	{
		base.FlushOutput();
		this.OutputFlushed = true;
	}

	public object? GetVariable(string name) => null;

	public Stream OpenRawInputStream()
	{
		throw new NotImplementedException();
	}

	public Stream OpenRawOutputStream()
	{
		throw new NotImplementedException();
	}

	public string Prompt(string prompt)
	{
		throw new NotImplementedException();
	}

	public OutputStyle OutputStyle { get; set; }

	public void WriteError(string text)
	{
		this._testContext.WriteLine($"[ERROR] {text}");
	}

	public void WriteMessage(string? text)
	{
		this._testContext.WriteLine($"[MSG] {text}");
	}

	public override void WriteOutput(string? message)
	{
		this._testContext.Write(message);
	}

	public override void WriteOutputLine(string? message)
	{
		this._testContext.WriteLine(message);
	}

	public List<object> OutputRecords { get; } = new List<object>();
	protected override void OnRecordWritten(object? record)
	{
		base.OnRecordWritten(record);
		this.OutputRecords.Add(record);
	}

	protected override void PrintTable(TextTable table)
	{
	}
}
