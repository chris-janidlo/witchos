using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using crass;

namespace WitchOS
{
    public class TerminalState : Singleton<TerminalState>
    {
        public Dictionary<string, string> EnvironmentVariables = new Dictionary<string, string>();

        void Awake ()
        {
            SingletonOverwriteInstance(this);
        }

        public string GetEnvironmentVariable (string variable)
        {
            return (EnvironmentVariables.ContainsKey(variable))
                ? EnvironmentVariables[variable]
                : "";
        }
    }
}
