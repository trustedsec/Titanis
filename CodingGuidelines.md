A commented code block is fine as long as there is an explanation, commonly with either // UNDONE or // WIP

Use WIP to signify that the code just isn't ready and was likely commented for debugging or something.  Ideally, the code shouldn't be merged with `main` and should be left in its own branch.

Use UNDONE to signify that the code caused problems that weren't intuitive.  For example, consider that someone reviewing the code may think "Hey, you forgot to do this simple check, let me do it for you." and they add the check.  But you specifically omitted the check.  Marking the check with UNDONE says that you 1) didn't forget the check, and 2) explains why you disabled the check.


// UNDONE: You would think this is correct, but in practice, it isn't.
// UNDONE

# Pascal Casing
Pascal casing only capitalizes the first character of each word, regardless of whether it is an acronym.  For example:

GetSDDL should be GetSddl

Even though SDDL is capitalized in English prose, that isn't what this is.

Function names should begin with an imperative verb form (e.g. GetValue, DoSomething, Close ).  A property name may or may not begin with a verb, but if it does, it should use the third-person singular form.

RequiresConfidentiality instead of RequireConfidentiality

Standard syntax for constructors:

```C#
		/// <summary>
		/// Initializes a new <see cref="ChannelConfig"/>.
		/// </summary>
```

The summary is displayed on the tool tip as you edit code and should always be short and concise.  Leave the details to the Remarks section.

# Asynchronous
Prefer asynchronous methods.

Accept a CancellationToken from the caller.

# Files and IO

Don't perform destructive operations, such as overwriting a file, default.  Accept a parameter to control the behavior.

# Parameters
In general, any aspect of the behavior should be customizable by accepting additional parameters from the caller.  If it is rarely used, make it an optional parameter with a sensible default.  Expose this default as a named constant and mention this constant in the documentation for the method.
