using System.Collections.Generic;

namespace WitchOS
{
    public interface ITerminal
    {
        bool CurrentlyEvaluating { get; }
        bool WasInterrupted { get; }

        IReadOnlyList<string> InputHistory { get; }
        IReadOnlyList<string> OutputHistory { get; }

        string LastOutputLine { get; set; }

        void ModifyInputHistory (int position, string value);
        void ModifyOutputHistory (int position, string value);

        void PrintMultipleLines (string output);
        void PrintSingleLine (string line);
        void PrintEmptyLine ();
    }
}
