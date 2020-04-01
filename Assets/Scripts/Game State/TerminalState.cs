using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using crass;

public class TerminalState : Singleton<TerminalState>
{
    public Dictionary<string, string> EnvironmentVariables = new Dictionary<string, string>();

    void Awake ()
    {
        SingletonOverwriteInstance(this);
    }
}
