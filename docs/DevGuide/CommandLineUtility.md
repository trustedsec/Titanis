# Writing a Command Line Utility

[[_TOC_]]

`Titanis.Cli` implements classes that make it easy to write a command line utility.  Consult the `CommandSample` project in the `Samples` folder.

To get started with writing a command line utility:
1. In Visual Studio, create a project using the Console App template.  If this is a standalone tool, consider adding it to the `Tools` solution folder.
1. Add a reference to `Titanis.Cli`.
1. Make the `Program` class inherit `Titanis.Cli.Command`.
1. Change the implementation of `Main` to:
```C#
static int Main(string[] args)
    => RunProgramAsync<Program>(args)
```

5. Add an implementation for `RunAsync` as follows:
```C#
protected async sealed override Task<int> RunAsync(CancellationToken cancellationToken)
{
    this.WriteMessage("Hello, world!");
    return 0;
}
```

Note that `RunProgramAsync` returns an `int` that indicates the result of running the command.  If you want this value to be returned by your program, change the return value of `Main` from `void` to `int`.

After ensuring that the startup project for the Solution is set to your new project, go ahead and run it.  If all is well, it will print a message and exit with an exit code `0`.  Let's take a look at what's going on.

Calling `RunProgramAsync` from `Main` transfers control to the Titanis Command implementation which instantiates Program, hooks CTRL+C, sets up an execution context using the console, validates command line parameters, then invokes `RunAsync`.  Notice that `RunAsync` is asynchronous and accepts a `CancellationToken`.  If the user presses CTRL+C, the hook requests cancellation.  Your implementation may use the cancellation token to detect this and respond accordingly.

# Parameters
Add a property to `Program` as follows:

```C#
[Parameter(10)]
[Mandatory]
[Category(ParameterCategories.Output)]
[Description("Message to print")]
public string Message { get; set; }
```

This defines a parameter named `Message`.  The `[Mandatory]` attribute indicates that the user must provide the argument or an error occurs and the command won't execute.

Change the implementation of `RunAsync` to use this parameter:
```C#
protected async sealed override Task<int> RunAsync(CancellationToken cancellationToken)
{
    this.WriteMessage(this.Message);
    return 0;
}
```

Now run the program.  The program will print a message:
```
The parameter 'Message' is mandatory, but no value was specified.
```
followed by a help statement describing the command syntax.  Titanis automatically generates the help text based on the `[Description]` and `[Category]` attributes.  This help is printed if Titanis detects a syntax error in the command.

Note that Titanis does not define a parameter to print the help, such as `-?` or `-h`.  That's up to you.  To generate the help statement, your command implementation may call `GetHelpText()`.

The Message parameter is mandatory, but it wasn't specified on the command line.  Let's set it up now.  If you are using Visual Studio:

1. Select `Debug` from the main menu bar.
1. Select `<project> Debug Properties` (usually at the bottom).
1. Set command-line to `"The work is mysterious and important"`
1. Close the window.  It'll save changes automatically.
1. Run the program.
1. It'll happily print out the message specified on the command line.

Notice the message is surrounded by quotes.  This is because it contains spaces.  Otherwise the message would be treated as separate arguments.  The `[Parameter]` attribute marks a property as a parameter.  The `10` specifies the position of the parameter within the command line.  The absolute value doesn't matter; only the value relative to other parameters.

All parameters may be specified by name, regardless of whether they are positional.  Alternatively, you may set the command line to any of the following:

```
-m "The work is mysterious and important"
-mes "The work is mysterious and important"
-message "The work is mysterious and important"
-message:"The work is mysterious and important"
```

Anything after the `-` is interpreted as a full or partial parameter name.  A partial name may be any string long enough to distinguish the parameter from other similarly-named parameters.  Since there are no other parameters staring with `M`, `-m` sufficiently identifies the `Message` parameter.

The value to a parameter may be specified either as the token following the parameter name or by appending `:<value>` to the parameter name with no intervening whitespace.

Let's add another parameter named `Count` that instructs the program to print the message multiple times.  Add a new property as follows:

```C#
[Parameter]
[Category(ParameterCategories.Output)]
[Description("Number of times to print the message")]
[DefaultValue(1)]
public int Count { get; set; }
```

This defines a parameter named Count that accepts an integer value.  The parameter is not mandatory and has a default value of 1 if not specified.  It is not positional and must be set by name.  Update the implementation of `RunAsync` to use the new parameter:

```C#
protected async sealed override Task<int> RunAsync(CancellationToken cancellationToken)
{
    for (int i = 0; i < this.Count; i++)
    {
        this.WriteMessage(this.Message);
    }
    return 0;
}
```

Since the command line doesn't specify `-Count`, the command runs with the default value of 1.  Now change the debug settings to add a count:

```
"Message" -c 5
```

Run the program and notice the message prints 5 times.

Note that `-c` is sufficient because no other parameter starts with C.  The Command implementation converted the string to an integer, since that's what the `Count` property accepts.  The implementation provides built-in converters for these types:

* `string`
* `char`
* `bool`
* Integral types: `byte`, `sbyte`, `short`, `ushort`, `int`, `uint`, `long`, `ulong`
* Floating-point types: `float`, `double`, `decimal`
* `DateTime`

.  Integer values may be prefixed to indicate hex (`0x`) or binary (`0b`):

```
"Hex value" -c 0x05
"Binary value 5" -c 0b101
"Larger binary value" -c 0b0101_1011
```

Note that binary values may include an underscore to group digits.  This may improve readability for larger binary numbers.

There's one slight problem with the implementation.  Since `int` is a signed value, the user may specify a negative value.  This doesn't cause the program to crash, but may indicate a mistake.  To provide custom parameter validation, implement `ValidatePraameters` as follows:

```C#
protected sealed override void ValidateParameters(ParameterValidationContext context)
{
    base.ValidateParameters(context);

    if (this.Count < 0)
        context.LogError(new ParameterValidationError(nameof(Count), "The value must be positive."));
}
```

`Command` calls this method after parsing and setting the parameters but before calling `RunAsync`.  This is where you perform custom validation and indicate errors and warning to the user.  If at least one error is logged, the command will abort before calling `RunAsync`.  Now try running the program with a negative count.  It will print a message indicating the problem:

```
One or more problems were encountered during parameter validation.
Count: The value must be positive.
```

Note that the message already includes the parameter name, so you don't need to specify the name in the message itself.  Also note that `RunAsync` was not called.

## Switch Parameters
Let's add a parameter that instructs the program to continuously print the message by adding a `-Loop` parameter as follows:

```C#
[Parameter]
[Description("Loops continuously")]
public SwitchParam Loop { get; set; }
```

The parameter has the type `SwitchParam`.  This type indicates the parameter is a flag.  That is, it doesn't necessarily take an argument; just specifying the name on the command line is sufficient.  The following are all valid:

```
-m Hello -Loop
-m Hello -Loop:true
-m Hello -Loop:false
-m Hello -l
```

Specifying `-Loop` alone (or an abbreviation) is enough to set the switch.  The user may explicitly specify a value of `:true` or `:false` by appending this to the parameter name token.  Note that `-Loop true` and `-Loop false` are both invalid for switches.

Why not just use `bool`?  You certainly could, but then the user would be required to provide a value of `true` or `false`.  Using `SwitchParam` allows for a more concise and natural syntax.

Now update `RunAsync` to use this parameter:

```C#
protected async sealed override Task<int> RunAsync(CancellationToken cancellationToken)
{
    if (this.Loop.IsSet)
        this.WriteWarning("Running in loop mode.  Press CTRL+C to end.");

    do
    {
        for (int i = 0; i < this.Count; i++)
        {
            this.WriteMessage(this.Message);
        }
    } while (this.Loop.IsSet && !cancellationToken.IsCancellationRequested);
    return 0;
}
```

Note the `do while` loop that checks `Loop.IsSet`.  If the user specifies `-Loop` or `-Loop:true` on the command line, then `IsSet` is `true`.  If the user specifies `-Loop:false` then `IsSet` is `false`.  In the latter case, `Loop.IsSpecified` indicates that the user explicitly set the parameter.

Note also that the loop condition references `cancellationToken.IsCancellationRequested`.  This property return `true` when the user presses `CTRL+C` to cancel the running program.  If `-Loop` is set, the program prints a warning informing the user that the program won't end on its own.

## Array Parameters
Sometimes it is logical for certain parameters to accept multiple values.  In this case, declare the parameter as an array:

```C#
[Parameter]
public string[] MultiString { get; set; }
```

When the parser encounters a parameter that is declared as an array, the values are parsed in a slightly different manner.  It checks whether each argument value ends with a comma.  If it does, the comma is stripped, the value is added to the array, and the next value is considered to be another value for the same parameter.  The last parameter must not end with a comma.

## Blob Parameters
Although most parameters are text-oriented, sometimes a command operates on raw bytes.  While you could declare the parameter as a byte array and require the user to enter a comma-separatede list of numeric values, this is generally not ideal.

Enter the Blob type.  The Blob type accepts values specified as a file name, a Base64-encoded string, or a hexadecimal-encoded string.  Declare the parameter like this:

```C#
[Parameter]
public Blob { get; set; }
```

If the value entered by the user is prefixed with `b64:` or `base64:`, the string is parsed as a Base64-encoded string.  Alternatively, the value may be prefixed with `hex:` to interpret it as a hex-encoded string.  If there is no prefixe, it is interpreted as a file name.  Omitting a prefixe for a file name allows the user to utilize any auto-complete features of the shell for file names.  Requiring a prefix on Base64- or hex-encoded strings avoids ambiguity, since a hex-encoded string uses a subset of the Base64 character set.

## Custom Parameters
So far, we've looked at a few different parameter types.  Titanis has built-in support for:

* Integral types: `byte`, `sbyte`, `short`, `ushort`
* `bool`
* `Enum` types
* `DateTime`
* `SwitchParam`

What if you want to use a custom parameter type?  Titanis has you covered.  Let's add a `-Duration` parameter using a custom type.  Implement the custom type as follows:

```C#
[TypeConverter(typeof(DurationConverter))]
internal class Duration
{
    public Duration(TimeSpan timeSpan)
    {
        this.TimeSpan = timeSpan;
    }

    public TimeSpan TimeSpan { get; }
}

class DurationConverter : TypeConverter
{
    public sealed override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType)
    {
        return (sourceType is string);
    }
    public sealed override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
    {
        if (value is string str)
        {
            int mult = 0;
            if (str.EndsWith('s'))
                mult = 1;
            else if (str.EndsWith('m'))
                mult = 60;
            else if (str.EndsWith('h'))
                mult = 60 * 60;

            if (mult == 0)
                throw new FormatException($"The value '{str}' does not indicate the unit of time.");

            double amount = double.Parse(str.Substring(0, str.Length - 1));
            return new Duration(TimeSpan.FromSeconds(amount));
        }

        return base.ConvertFrom(context, culture, value);
    }
}
```

Notice `[TypeConverter(typeof(DurationConverter))]`.  This attaches a type converter to the custom type.  When Titanis encounters a custom parameter type, it checks for this attribute and uses the associated TypeConverter to convert the string from the command line to a parameter value.

Now add a parameter using this new type:

```C#
[Parameter]
[Description("Duration to run the loop")]
public Duration? Duration { get; set; }
```

Update `RunAsync` to use the new parameter:

```C#
protected async sealed override Task<int> RunAsync(CancellationToken cancellationToken)
{
    if (this.Loop.IsSet)
        this.WriteWarning("Running in loop mode.  Press CTRL+C to end.");

    DateTime stopTime = DateTime.MaxValue;
    if (this.Duration != null)
    {
        stopTime = DateTime.Now + this.Duration.TimeSpan;
    }

    do
    {
        if (DateTime.Now >= stopTime)
            break;

        for (int i = 0; i < this.Count; i++)
        {
            this.WriteMessage(this.Message);
        }
    } while (this.Loop.IsSet && !cancellationToken.IsCancellationRequested);
    return 0;
}
```

And finally, update the debug command line to use the new parameter:
```
"When will this end?" -loop -Duration 3s
```

Now run the program.  It will loop for about 3s (or however long you entered).

### Default Values for Custom Parameters
A parameter using a custom type may specify a default value with `[DefaultValue]` just like the built-in types.  There is a slight catch, however.  `[DefaultValue]` requires a constant value.  This is fine for numbers and strings, but not for a custom type.  When Titanis encounters a default value for a custom type, it first checks whether the constant value provided is of the appropriate type.  If the value type doesn't match the parameter type, Titanis invokes the type converter, just like when parsing command line parameters.  This means you are able to specify the default value using any value convertible by the type converter.  In this case, you could specify a default value with `[DefaultValue("5s")]`.  The string is parsed using `DurationTypeConverter`.

## Parameter Groups
For small programs, implementing each parameter as a property of the command class is manageable.  As programs grow in complexity, this becomes more difficult.  To address this, Titanis offers a feature called _parameter groups_.

A parameter group is a grouping of related parameters.  This allows you to group related parameters into separate classes rather than implementing all of them as properties on the command class.  Since the class is separate from the command class, it may then be reused on several command classes.

Create a new class called `TimeParameters` and move the definition of the `Duration` property to this new class.  It should look like this:

```C#
internal class TimeParameters
{
    [Parameter]
    [Description("Duration to run the loop")]
    public Duration? Duration { get; set; }
}
```

Now reference this parameter group from `Program`:

```C#
[ParameterGroup]
public TimeParameters? TimeParams { get; set; }
```

Finally, update `RunAsync` to reference this parameter group:

```C#
protected async sealed override Task<int> RunAsync(CancellationToken cancellationToken)
{
    if (this.Loop.IsSet)
        this.WriteWarning("Running in loop mode.  Press CTRL+C to end.");

    DateTime stopTime = DateTime.MaxValue;
    if (this.TimeParams?.Duration != null)
    {
        stopTime = DateTime.Now + this.TimeParams.Duration.TimeSpan;
    }

    do
    {
        if (DateTime.Now >= stopTime)
            break;

        for (int i = 0; i < this.Count; i++)
        {
            this.WriteMessage(this.Message);
        }
    } while (this.Loop.IsSet && !cancellationToken.IsCancellationRequested);
    return 0;
}
```

The program behaves just as it did before.  If the command line includes a parameter defined within a parameter group, Titanis instantiates the parameter group object and sets the appropriate property.  Note that if you remove the `-Duration` parameter from the command line, `TimeParams` is never set and remains `null`.

Parameter groups may be nested.  This means that in this example, `TimeParams` may itself contain properties marked with `[ParameterGroup]` that reference other groups.

Parameters defined in a parameter group may have default values, just like parameters defined on the command class.  In this case, the parameter group object is always instantiated.
