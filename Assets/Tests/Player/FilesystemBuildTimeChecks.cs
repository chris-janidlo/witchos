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
        public void AllFileSubclasses_HaveAppropriateAttributes ()
        {
            foreach (var implementation in Reflection.GetImplementations<FileBase>())
            {
                var dataContracts = getAttributeValues<DataContractAttribute>(implementation);
                Assert.That(dataContracts, Is.Not.Empty, $"File subclass {implementation.Name} has no DataContract attribute");

                var serializables = getAttributeValues<SerializableAttribute>(implementation);
                Assert.That(serializables, Is.Not.Empty, $"File subclass {implementation.Name} has no Serializable attribute");
            }
        }

        [Test]
        public void File_HasKnownTypeForEveryFileSubclass ()
        {
            var baseKnownTypes =
                getAttributeValues<KnownTypeAttribute>(typeof(FileBase))
                .Select(k => k.Type);

            var genericKnownTypes =
                getAttributeValues<KnownTypeAttribute>(typeof(File<>))
                .Select(k => k.Type);

            string errMessage;

            foreach (var implementation in Reflection.GetImplementations<FileBase>())
            {
                // bases should already implicitly know themselves
                if (implementation == typeof(File<>) || implementation == typeof(FileBase)) continue;

                errMessage = $"main FileBase type doesn't contain KnownType reference for subclass {implementation.Name}";
                Assert.That(baseKnownTypes, Contains.Item(implementation), errMessage);

                errMessage = $"generic File type doesn't contain KnownType reference for subclass {implementation.Name}";
                Assert.That(genericKnownTypes, Contains.Item(implementation), errMessage);
            }
        }

        TAttribute[] getAttributeValues<TAttribute> (Type classType)
        {
            var uncastedAttributeValues = classType.GetCustomAttributes(typeof(TAttribute), false);
            return Array.ConvertAll(uncastedAttributeValues, o => (TAttribute) o);
        }
    }
}
