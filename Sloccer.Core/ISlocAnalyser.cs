using System.IO;

namespace Sloccer.Core
{
    public interface ISlocAnalyser
    {
        SlocResult GetSlocFor(FileInfo file);
    }
}
