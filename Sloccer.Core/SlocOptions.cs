using System;

namespace Sloccer.Core
{
    [Flags]
    public enum SlocOptions
    {
        CountWhiteSpace = 1,
        CountUsingStatements = 2,
        CountCompilerDirectives = 4,
        CountMultiStatements = 8, // Count lines with multiple executable statements as multiple lines
        CountLoopParts = 16 // Count loop statements as multiple lines, i.e., for(i = 0; i < 10; i++) is 3 statements/lines
    }
}
