using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace WitchOS.Tests
{
    public class EmailBlueprintTests
    {
        [SetUp]
        public void SetUp ()
        {
            Resources.Load<TimeState>("ScriptableObjects/Test Time State").Initialize();
        }

        [Test]
        public void BasicBlueprint_GeneratesSingleEmail ()
        {
            var blueprint = loadEmailBlueprint("Single Email");
            Assert.That(blueprint.PossibleEmails.Items.Count, Is.EqualTo(1));

            var email = blueprint.GenerateEmail();

            Assert.That(email, Is.InstanceOf<Email>());
            Assert.That(email, Is.Not.InstanceOf<Order>());

            Assert.That(email.Body, Is.Not.Null);
            Assert.That(email.Body, Is.Not.Empty);

            Assert.That(email.SubjectLine, Is.Not.Null);
            Assert.That(email.SubjectLine, Is.Not.Empty);

            Assert.That(email.SenderAddress, Is.Not.Null);
            Assert.That(email.SenderAddress, Is.Not.Empty);

            var secondEmail = blueprint.GenerateEmail();
            Assert.That(secondEmail, Is.EqualTo(email));
        }

        EmailBlueprint loadEmailBlueprint (string name)
        {
            return Resources.Load<EmailBlueprint>($"ScriptableObjects/Test EmailBlueprint - {name}");
        }
    }
}
