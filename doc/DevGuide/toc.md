# Developer Guide

*  **[Samples](Samples.md)** - List of samples that demonstrate how to use various components
*  **[Using SMB](UsingSmb.md)** - How to use the SMB protocol implementation
*  **[Versioning](Versioning.md)** - Describes how Titanis uses versioning
*  **[Using Services](Services.md)** - Explains how various Titanis components delegate tasks to service components
*  **[Using Source Generation](SourceGenerator.md)**
*  **[Building a Command Line Utility](CommandLineUtility.md)**

## Signing
For builds using the `Release` configuration, `src/Directory.Build.props` checks for a signing key file named `Titanis.snk` in the same directory as `Titanis.sln`.  If the key file exists, it is used to sign the libraries under `src/`.  If it doesn't exist, the projects are not signed, and no error occurs.

## Central Package Management
Projects under `src/` use central package management.  Titanis packages reference the version specified by the `$(TitanisVersion)` variable defined in `Directory.Packages.props`.

