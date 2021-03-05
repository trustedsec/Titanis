# Overview

The Titanis.Core library implements basic, lightweight convenience functions for use in other libraries
within the framework.  Many of these functions are in the form ef extension methods.

This library also defines some interfaces that are used throughout the framework.

## Extension methods
These la
ArgValidation - Convenience methods for argument validation
ArrayExtensions - Extension methods for arrays
BinaryHelper - Methods for manipulating binary data, including conversion to/from hexadecimal strings
BitHelper - Provides bit manipulation functions
DictionaryExtensions - Provides convenience methods for IDictionary<,> methods
Enumerable - Enumerable extension methods

## Interfaces

IFactory - Generic factory interface for creating objects
IHaveNtStatusCode - Exposes encapsulated NTSTATUS code
IHaveWin32StatusCode - Exposes encapsulated Win32 status code
INamedObject - Exposes the name of on object


## Classes

CacheTable - Cached lookup table
