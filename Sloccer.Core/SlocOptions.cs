using System;

namespace Sloccer.Core
{
    [Flags]
    public enum SlocOptions
    {
        CountWhiteSpace = 1,
        CountComments = 2,
        CountUsingStatements = 4,
        CountCompilerDirectives = 8,
        CountAnnotations = 16,
        CountMultiStatements = 32, // Count lines with multiple executable statements as multiple lines
    }
}
