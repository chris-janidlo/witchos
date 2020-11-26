using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using crass;
using System.Runtime.Serialization;

namespace WitchOS.Tests
{
    public class FilesystemBuildTimeChecks
    {
        [Test]
        public void AllFileSubclasses_AreDataContracts ()
        {
            foreach (var implementation in Reflection.GetImplementations<FileBase>())
            {
                string errMessage = $"File subclass {implementation.Name} has no DataContract attribute";
                Assert.That(getAttributeValues<DataContractAttribute>(implementation), Is.Not.Empty, errMessage);
            }
        }

        [Test]
        public void File_HasKnownTypeForEveryFileSubclass ()
        {
            var knownTypes = getAttributeValues<KnownTypeAttribute>(typeof(File<>))
                .Select(k => k.Type);

            foreach (var implementation in Reflection.GetImplementations<FileBase>())
            {
                // File should already implicitly know itself
                if (implementation == typeof(File<>)) continue;

                string errMessage = $"main File type doesn't contain KnownType reference for subclass {implementation.Name}";
                Assert.That(knownTypes, Contains.Item(implementation), errMessage);
            }
        }

        TAttribute[] getAttributeValues<TAttribute> (Type classType)
        {
            var uncastedAttributeValues = classType.GetCustomAttributes(typeof(TAttribute), false);
            return Array.ConvertAll(uncastedAttributeValues, o => (TAttribute) o);
        }
    }
}
