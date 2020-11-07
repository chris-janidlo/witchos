using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;
using WitchOS;

namespace Tests
{
    public class SetCommandTests
    {
        public class TerminalStub : ITerminal
        {
            public bool CurrentlyEvaluating => true;
            public bool WasInterrupted => false;

            public string LastOutputLine
            {
                get => "";
                set { }
            }

            IReadOnlyList<string> ITerminal.InputHistory => new List<string>().AsReadOnly();

            IReadOnlyList<string> ITerminal.OutputHistory => new List<string>().AsReadOnly();

            public void ModifyInputHistory (int position, string value) { }

            public void ModifyOutputHistory (int position, string value) { }

            public void PrintMultipleLines (string output) { }

            public void PrintEmptyLine () { }

            public void PrintSingleLine (string line) { }
        }

        SetCommand setCommand;
        EnvironmentVariableState environmentVariableState;

        [SetUp]
        public void SetTestsUp ()
        {
            environmentVariableState = ScriptableObject.CreateInstance<EnvironmentVariableState>();
            environmentVariableState.ClearEnvironmentVariables();

            setCommand = ScriptableObject.CreateInstance<SetCommand>();
            setCommand.EnvironmentVariableState = environmentVariableState;

            Assert.IsEmpty(environmentVariableState.GetEnvironmentVariable("key"), "variable 'key' should be empty before tests");
            Assert.IsEmpty(environmentVariableState.GetEnvironmentVariable("test"), "variable 'test' should be empty before tests");
            Assert.IsEmpty(environmentVariableState.GetEnvironmentVariable("aura"), "variable 'aura' should be empty before tests");
            Assert.IsEmpty(environmentVariableState.GetEnvironmentVariable("target"), "variable 'target' should be empty before tests");
        }

        [UnityTest]
        public IEnumerator SimpleArgumentListIsSet ()
        {
            string key = "key";
            string value = "value";

            yield return setCommand.Evaluate(new TerminalStub(), new string[] { "set", key, value });

            string actualValue = environmentVariableState.GetEnvironmentVariable(key);

            Assert.AreEqual(value, actualValue, $"variable '{key}' should be set to '{value}'");
        }

        [UnityTest]
        public IEnumerator ComplexArgumentListIsSet ()
        {
            string[] args = new string[] { "set", "key", "to", "complex", "value", "list" };

            yield return setCommand.Evaluate(new TerminalStub(), args);

            string key = args[1];
            string value = string.Join(" ", args.Skip(2));
            string actualValue = environmentVariableState.GetEnvironmentVariable(key);

            Assert.AreEqual(value, actualValue, $"variable '{key}' should be set to '{value}'");
        }
    }
}
