using System.Collections.Generic;

namespace WitchOS
{
    public interface ITerminal
    {
        bool CurrentlyEvaluating { get; }
        bool WasInterrupted { get; }

        IList<string> InputHistory { get; set; }
        IList<string> OutputHistory { get; set; }

        string LastOutputLine { get; set; }

        void Print (string output);
        void PrintLine (string line);
        void PrintEmptyLine ();
    }
}
