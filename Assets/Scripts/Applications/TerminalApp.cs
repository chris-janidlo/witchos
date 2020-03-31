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
        IEnumerator Evaluate (string[] arguments);
    }

    public bool Evaluating { get; private set; }

    public Window Window;
    public TMP_InputField CommandInput;

    public TextMeshProUGUI Prompt, HistoryText;

    public static List<string> History = new List<string>();

    void Start ()
    {
        Window.DidFocus += CommandInput.Select;

        CommandInput.onSubmit.AddListener((s) => StartCoroutine(evaluateCommand(s)));
    }

    void Update ()
    {
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

        History.Add(Prompt.text + " " + input); // echo

        Evaluating = true;

        string[] arguments = input.Split();
        if (Commands.ContainsKey(arguments[0]))
        {
            yield return Commands[arguments[0]].Evaluate(arguments);
        }
        else
        {
            History.Add("command not recognized: " + arguments[0]);
        }

        Evaluating = false;

        Prompt.enabled = true;
        CommandInput.enabled = true;
    }

    static void println (string line)
    {
        History.Add(line);
    }
}
