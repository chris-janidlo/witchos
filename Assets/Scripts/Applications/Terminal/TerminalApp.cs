using System;
using System.Text;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityAtoms.WitchOS;

namespace WitchOS
{
    public partial class TerminalApp : MonoBehaviour, ITerminal
    {
        public bool CurrentlyEvaluating { get; private set; }
        public bool WasInterrupted { get; private set; }

        List<string> _inputHistory = new List<string>();
        public IList<string> InputHistory
        {
            get => _inputHistory;
            set
            {
                _inputHistory = (List<string>) value;
                paintOutputHistoryText();
            }
        }

        List<string> _outputHistory = new List<string>();
        public IList<string> OutputHistory
        {
            get => _outputHistory;
            set
            {
                _outputHistory = (List<string>) value;
                paintOutputHistoryText();
            }
        }

        public string LastOutputLine
        {
            get => OutputHistory[OutputHistory.Count - 1];
            set => OutputHistory[OutputHistory.Count - 1] = value;
        }

        public TerminalCommandValueList Commands;

        public string BaseTitle;

        [TextArea]
        public string NoMagicWarning;

        public Window Window;
        public TMP_InputField CommandInput;

        public TextMeshProUGUI Prompt, HistoryText;

        public ScrollRect ScrollRect;

        Dictionary<string, TerminalCommand> commandDict => Commands.ToDictionary(c => c.Name);

        TerminalCommand currentCommand;

        int posInHistory;

        StringBuilder paintTextBuilder = new StringBuilder();

        void Start ()
        {
            CommandInput.onSubmit.AddListener((s) => StartCoroutine(evaluateCommand(s)));

            if (!MagicSource.Instance.On) Print(NoMagicWarning);
            else paintOutputHistoryText(); // paint anyway
        }

        void Update ()
        {
            WasInterrupted = false;

            if (!Window.Focused) return;

            if (Input.GetKey(KeyCode.Escape)) WasInterrupted = true;

            if (CurrentlyEvaluating) return;

            if (Input.GetKeyDown(KeyCode.UpArrow)) incrementPosInHistory(-1);
            else if (Input.GetKeyDown(KeyCode.DownArrow)) incrementPosInHistory(1);
        }

        void OnDestroy ()
        {
            if (CurrentlyEvaluating) currentCommand.CleanUpEarly(this);
        }

        public void FocusInput ()
        {
            CommandInput.ActivateInputField();
            CommandInput.Select();
        }

        public void Print (string output)
        {
            foreach (string line in output.Split('\n', '\r'))
            {
                PrintLine(line);
            }

            paintOutputHistoryText();
        }

        public void PrintLine (string line)
        {
            OutputHistory.Add(line);
            paintOutputHistoryText();
        }

        public void PrintEmptyLine ()
        {
            PrintLine("");
        }

        void paintOutputHistoryText ()
        {
            paintTextBuilder.Clear();

            foreach (string line in OutputHistory)
            {
                paintTextBuilder.Append(line);
                paintTextBuilder.Append("\n");
            }

            if (!CurrentlyEvaluating) paintTextBuilder.Append(" "); // empty additional line so that the history doesn't overlap the prompt

            HistoryText.text = paintTextBuilder.ToString();
        }

        IEnumerator evaluateCommand (string input)
        {
            if (Input.GetKey("escape")) yield break;

            CommandInput.text = "";

            Prompt.enabled = false;
            CommandInput.enabled = false;

            InputHistory.Add(input);
            OutputHistory.Add(Prompt.text + input); // echo

            posInHistory = InputHistory.Count;
            scrollToBottom();

            input = input.Trim();

            CurrentlyEvaluating = true;

            // remove empty prompt line in history text
            paintOutputHistoryText();

            string[] commands = input.Split(';');

            foreach (string command in commands)
            {
                if (command == "")
                {
                    continue;
                }

                string[] arguments = command.Split();

                if (commandDict.ContainsKey(arguments[0]))
                {
                    Window.Title = BaseTitle + " - " + arguments[0];

                    currentCommand = commandDict[arguments[0]];
                    yield return currentCommand.Evaluate(this, arguments);

                    Window.Title = BaseTitle;
                }
                else
                {
                    PrintLine("command not recognized: " + arguments[0]);
                }
            }

            CurrentlyEvaluating = false;

            Prompt.enabled = true;
            CommandInput.enabled = true;

            // restore empty prompt line in history text
            paintOutputHistoryText();

            if (Window.Focused) FocusInput();
        }

        void incrementPosInHistory (int dir)
        {
            posInHistory = Mathf.Clamp(posInHistory + (int) Mathf.Sign(dir), 0, InputHistory.Count);

            CommandInput.text = (posInHistory == InputHistory.Count)
                ? ""
                : InputHistory[posInHistory];

            CommandInput.caretPosition = CommandInput.text.Length;
            scrollToBottom();
        }

        void scrollToBottom ()
        {
            ScrollRect.verticalNormalizedPosition = 0;
        }
    }
}
