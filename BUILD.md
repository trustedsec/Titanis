# Building Titanis

To build Titanis:

1. Get the [source code](https://github.com/trustedsec/Titanis)
1. Install the [prerequisites](#prerequisites)
1. Compile the code
	1. [dotnet build](#dotnet-build) (Windows and Linux)
	1. [Visual Studio 2022](#visual-studio-2022) (Windows only)

## Prerequisites

To build Titanis, you'll need to install the following prerequisites:

* .NET 8 SDK 
	* [Installation instructions for Linux](https://learn.microsoft.com/en-us/dotnet/core/install/linux)
	* [Installation instructions for Windows](https://learn.microsoft.com/en-us/dotnet/core/install/windows)

# dotnet build

After cloning the Titanis source code into a directory, open a terminal (or Command Prompt)
and change to the directory containing the source code.  Then issue the following command:

```
dotnet build
```

# Visual Studio 2022

1. Open `Titanis.sln`
1. Click `Build`

