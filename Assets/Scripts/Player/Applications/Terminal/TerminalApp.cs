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

        public IReadOnlyList<string> InputHistory => inputHistory.AsReadOnly();
        public IReadOnlyList<string> OutputHistory => outputHistory.AsReadOnly();

        public string LastOutputLine
        {
            get => outputHistory[outputHistory.Count - 1];
            set => ModifyOutputHistory(outputHistory.Count - 1, value);
        }

        public TerminalCommandValueList Commands;

        public string BaseTitle;

        [TextArea]
        public string NoMagicWarning;

        public Window Window;
        public TMP_InputField CommandInput;

        public TextMeshProUGUI Prompt, HistoryText;

        public ScrollRect ScrollRect;

        readonly List<string> inputHistory = new List<string>();
        readonly List<string> outputHistory = new List<string>();

        Dictionary<string, TerminalCommand> commandDict => Commands.ToDictionary(c => c.Name);

        TerminalCommand currentCommand;

        int posInHistory;

        StringBuilder paintTextBuilder = new StringBuilder();

        void Start ()
        {
            CommandInput.onSubmit.AddListener(s => StartCoroutine(evaluateCommand(s)));

            if (!MagicSource.Instance.On)
            {
                PrintMultipleLines(NoMagicWarning);
                addVerticalOutputSpacer();
            }

            paintOutputHistoryText();
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

        public void ModifyInputHistory (int position, string value)
        {
            inputHistory[position] = value;
        }

        public void ModifyOutputHistory (int position, string value)
        {
            outputHistory[position] = value;
            paintOutputHistoryText();
        }

        public void PrintMultipleLines (string output)
        {
            foreach (string line in output.Split('\n', '\r'))
            {
                outputHistory.Add(line);
            }

            paintOutputHistoryText();
        }

        public void PrintSingleLine (string line)
        {
            outputHistory.Add(line);
            paintOutputHistoryText();
        }

        public void PrintEmptyLine ()
        {
            PrintSingleLine("");
        }

        void addVerticalOutputSpacer ()
        {
            outputHistory.Add("");
        }

        void paintOutputHistoryText ()
        {
            paintTextBuilder.Clear();

            foreach (string line in outputHistory)
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

            inputHistory.Add(input);
            outputHistory.Add(Prompt.text + input); // echo

            posInHistory = inputHistory.Count;
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
                    PrintSingleLine("command not recognized: " + arguments[0]);
                }
            }

            CurrentlyEvaluating = false;

            Prompt.enabled = true;
            CommandInput.enabled = true;

            addVerticalOutputSpacer();

            // restore empty prompt line in history text
            paintOutputHistoryText();

            if (Window.Focused) FocusInput();
        }

        void incrementPosInHistory (int dir)
        {
            posInHistory = Mathf.Clamp(posInHistory + (int) Mathf.Sign(dir), 0, inputHistory.Count);

            CommandInput.text = (posInHistory == inputHistory.Count)
                ? ""
                : inputHistory[posInHistory];

            CommandInput.caretPosition = CommandInput.text.Length;
            scrollToBottom();
        }

        void scrollToBottom ()
        {
            ScrollRect.verticalNormalizedPosition = 0;
        }
    }
}
