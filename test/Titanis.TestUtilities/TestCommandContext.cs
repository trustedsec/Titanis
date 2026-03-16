using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titanis.Cli;

namespace Titanis;

public class TestCommandContext : ICommandContext
{
	public TestCommandContext(TestContext testContext, IServiceContainer hostServices)
	{
		ReflectionMetadataResolver mdResolver = new ReflectionMetadataResolver();

		this._hostServices = hostServices;
		this.MetadataContext = new CommandMetadataContext(mdResolver);
		this.Log = new TestLog(testContext);
	}

	private readonly IServiceContainer _hostServices;

	public ITerminal Terminal => throw new NotImplementedException();

	public string WorkingDirectory => throw new NotImplementedException();

	public IServiceProvider HostServices => this._hostServices;


	public CommandMetadataContext MetadataContext { get; }

	public ILog Log { get; }

	public Task ExecuteFrameAsync(Func<CancellationToken, Task> func)
	{
		throw new NotImplementedException();
	}

	public bool OutputFlushed { get; set; }
	public void FlushOutput() => this.OutputFlushed = true;

	public object? GetVariable(string name) => null;

	public bool IsFieldInOutput(string fieldName)
	{
		throw new NotImplementedException();
	}

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
	public void SetOutputFormat(OutputStyle style, IOutputFieldProvider? fields, bool includeHeaders)
	{
		this.OutputStyle = style;
	}

	public void WriteError(string text)
	{
		throw new NotImplementedException();
	}

	public void WriteMessage(string? text)
	{
		throw new NotImplementedException();
	}

	public void WriteOutput(string? text)
	{
		throw new NotImplementedException();
	}

	public void WriteOutputLine(string? text)
	{
		throw new NotImplementedException();
	}

	public List<object> OutputRecords { get; } = new List<object>();
	public void WriteRecord(object? record) => this.OutputRecords.Add(record);
	public void WriteRecords(IEnumerable records) => this.OutputRecords.AddRange(records.OfType<object>());
}
