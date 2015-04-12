using System.Collections.Generic;
using System.IO;

namespace Sloccer.Core
{
    public interface ISlocAnalyser
    {
        long GetSlocFor(IEnumerable<FileInfo> files, SlocOptions options);
    }
}
