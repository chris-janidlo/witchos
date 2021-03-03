using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace WitchOS.Tests
{
    public class EmailBlueprintTests
    {
        TimeState timestate;

        [SetUp]
        public void SetUp ()
        {
            timestate = Resources.Load<TimeState>("ScriptableObjects/Test Time State");
            timestate.Initialize();
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

        [Test]
        public void BasicOrderBlueprint_GeneratesSingleOrder ()
        {
            var blueprint = loadEmailBlueprint("Single Order");
            Assert.That(blueprint.PossibleEmails.Items.Count, Is.EqualTo(1));
            Assert.That(blueprint.PossibleInvoices.Items.Count, Is.EqualTo(1));

            var email = blueprint.GenerateEmail();

            Assert.That(email, Is.InstanceOf<Email>());
            Assert.That(email, Is.InstanceOf<Order>());

            var order = email as Order;

            Assert.That(order.Invoice, Is.Not.Null);
            Assert.That(blueprint.PossibleInvoices.Items[0].FullDaysToComplete, Is.EqualTo(0));
            Assert.That(order.DueDate, Is.EqualTo(timestate.DateTime));
            Assert.That(order.State, Is.EqualTo(OrderState.InProgress));

            var secondEmail = blueprint.GenerateEmail();
            Assert.That(secondEmail, Is.EqualTo(email));
        }

        EmailBlueprint loadEmailBlueprint (string name)
        {
            return Resources.Load<EmailBlueprint>($"ScriptableObjects/Test EmailBlueprint - {name}");
        }
    }
}
