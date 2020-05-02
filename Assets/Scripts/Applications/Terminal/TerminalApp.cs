using System;
using System.Text;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public partial class TerminalApp : MonoBehaviour
{
    public bool Evaluating { get; private set; }
    public bool SIGINT { get; private set; }

    public Dictionary<string, TerminalCommand> CommandDict => Commands.ToDictionary(c => c.Name);

    public List<TerminalCommand> Commands;

    public string BaseTitle;

    [TextArea]
    public string NoMagicWarning;

    public List<string> InputHistory = new List<string>();
    public List<string> OutputHistory = new List<string>();

    public Window Window;
    public TMP_InputField CommandInput;

    public TextMeshProUGUI Prompt, HistoryText;

    public ScrollRect ScrollRect;

    TerminalCommand currentCommand;

    int posInHistory;

    StringBuilder paintTextBuilder = new StringBuilder();

    void Start ()
    {
        CommandInput.onSubmit.AddListener((s) => StartCoroutine(evaluateCommand(s)));

        if (!MagicSource.Instance.On) Print(NoMagicWarning);
    }

    void Update ()
    {
        SIGINT = false;
        if (Window.Focused && Input.GetKey(KeyCode.Escape)) SIGINT = true;

        if (Window.Focused && Input.GetKeyDown(KeyCode.UpArrow)) incrementPosInHistory(-1);
        else if (Window.Focused && Input.GetKeyDown(KeyCode.DownArrow)) incrementPosInHistory(1);
    }

    void OnDestroy ()
    {
        if (Evaluating) currentCommand.CleanUpEarly(this);
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

    public void ScrollToBottom ()
    {
        ScrollRect.verticalNormalizedPosition = 0;
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
        ScrollToBottom();

        input = input.Trim();

        Evaluating = true;

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

            if (CommandDict.ContainsKey(arguments[0]))
            {
                Window.Title = BaseTitle + " - " + arguments[0];

                currentCommand = CommandDict[arguments[0]];
                yield return currentCommand.Evaluate(this, arguments);

                Window.Title = BaseTitle;
            }
            else
            {
                PrintLine("command not recognized: " + arguments[0]);
            }
        }

        Evaluating = false;

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
        ScrollToBottom();
    }

    void paintOutputHistoryText ()
    {
        paintTextBuilder.Clear();

        foreach (string line in OutputHistory)
        {
            paintTextBuilder.Append(line);
            paintTextBuilder.Append("\n");
        }

        if (!Evaluating) paintTextBuilder.Append(" "); // empty additional line so that the history doesn't overlap the prompt

        HistoryText.text = paintTextBuilder.ToString();
    }
}
