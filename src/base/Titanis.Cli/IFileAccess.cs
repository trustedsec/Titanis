using System;
using System.Collections.Generic;
using System.Text;

namespace Titanis.Cli
{
    public interface IFileAccess
    {
        string ResolveFsPath(string filePath);
    }
}
