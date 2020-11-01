using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WitchOS
{
    [CreateAssetMenu(fileName = "Environment Variable State System.asset", menuName = "WitchOS/Environment Variable State")]
    public class EnvironmentVariableState : ScriptableObject
    {
        public Dictionary<string, string> EnvironmentVariables = new Dictionary<string, string>();

        public string GetEnvironmentVariable (string variable)
        {
            return (EnvironmentVariables.ContainsKey(variable))
                ? EnvironmentVariables[variable]
                : "";
        }
    }
}
