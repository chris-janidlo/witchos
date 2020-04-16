using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// everything except for the command list goes here
// TODO: add history scrolling w up/down keys
public partial class TerminalApp : MonoBehaviour
{
    public bool Evaluating { get; private set; }
    public bool SIGINT { get; private set; }

    public Dictionary<string, TerminalCommand> CommandDict => Commands.ToDictionary(c => c.Name);

    public List<TerminalCommand> Commands;

    public string BaseTitle;

    public List<string> InputHistory = new List<string>();
    public List<string> OutputHistory = new List<string>();

    public Window Window;
    public TMP_InputField CommandInput;

    public TextMeshProUGUI Prompt, HistoryText;

    int posInHistory;

    void Start ()
    {
        CommandInput.onSubmit.AddListener((s) => StartCoroutine(evaluateCommand(s)));
    }

    void Update ()
    {
        SIGINT = false;
        if (Window.Focused && Input.GetKey(KeyCode.Escape)) SIGINT = true;

        if (Window.Focused && Input.GetKeyDown(KeyCode.UpArrow)) incrementPosInHistory(-1);
        else if (Window.Focused && Input.GetKeyDown(KeyCode.DownArrow)) incrementPosInHistory(1);

        string hist = "";

        foreach (string line in OutputHistory)
        {
            hist += line + "\n";
        }

        if (!Evaluating) hist += " ";

        HistoryText.text = hist;
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

        input = input.Trim();

        Evaluating = true;

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
                yield return CommandDict[arguments[0]].Evaluate(this, arguments);
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

        if (Window.Focused) FocusInput();
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
    }
    public void PrintLine (string line)
    {
        OutputHistory.Add(line);
    }

    void incrementPosInHistory (int dir)
    {
        posInHistory = Mathf.Clamp(posInHistory + (int) Mathf.Sign(dir), 0, InputHistory.Count);

        CommandInput.text = (posInHistory == InputHistory.Count)
            ? ""
            : InputHistory[posInHistory];
    }
}
