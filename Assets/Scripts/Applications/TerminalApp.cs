using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// everything except for the command list goes here
// TODO: add history scrolling w up/down keys
public partial class TerminalApp : MonoBehaviour
{
    interface Command
    {
        string[] HelpOutput { get; }
        IEnumerator Evaluate (TerminalApp term, string[] arguments);
    }

    public bool Evaluating { get; private set; }
    public bool SIGINT { get; private set; }

    public string BaseTitle;

    public List<string> History = new List<string>();

    public Window Window;
    public TMP_InputField CommandInput;

    public TextMeshProUGUI Prompt, HistoryText;

    void Start ()
    {
        Window.DidFocus += FocusInput;

        CommandInput.onSubmit.AddListener((s) => StartCoroutine(evaluateCommand(s)));

        FocusInput();
    }

    void Update ()
    {
        SIGINT = false;
        if (Window.Focused && Input.GetKey(KeyCode.Escape)) SIGINT = true;

        string hist = "";

        foreach (string line in History)
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

        History.Add(Prompt.text + input); // echo

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

            if (Commands.ContainsKey(arguments[0]))
            {
                Window.Title = BaseTitle + " - " + arguments[0];
                yield return Commands[arguments[0]].Evaluate(this, arguments);
                Window.Title = BaseTitle;
            }
            else
            {
                println("command not recognized: " + arguments[0]);
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

    void println (string line)
    {
        History.Add(line);
    }
}
