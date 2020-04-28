using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using NUnit.Framework;
using MicrowaveOvenClasses.Boundary;

namespace Microwave.Test.Integrations
{
    [TestFixture]
    class DisplayDriver
    {
        private Display display;
        private Output output;

        [SetUp]
        public void Setup()
        {
            output = new Output();
            display = new Display(output);
        }

        [Test]
        public void ShowTime_ZeroMinuteZeroSeconds_CorrectOutput()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                display.ShowTime(0, 0);
                string expected = $"Display shows: 00:00{Environment.NewLine}";
                Assert.AreEqual(expected, sw.ToString());
            }
        }

        [Test]
        public void ShowTime_ZeroMinuteSomeSecond_CorrectOutput()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                display.ShowTime(0, 5);
                string expected = $"Display shows: 00:05{Environment.NewLine}";
                Assert.AreEqual(expected, sw.ToString());
            }
        }

        [Test]
        public void ShowTime_SomeMinuteZeroSecond_CorrectOutput()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                display.ShowTime(5, 0);
                string expected = $"Display shows: 05:00{Environment.NewLine}";
                Assert.AreEqual(expected, sw.ToString());
            }
        }

        [Test]
        public void ShowTime_SomeMinuteSomeSecond_CorrectOutput()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                display.ShowTime(10, 15);
                string expected = $"Display shows: 10:15{Environment.NewLine}";
                Assert.AreEqual(expected, sw.ToString());
            }
        }

        [Test]
        public void ShowPower_Zero_CorrectOutput()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                display.ShowPower(0);
                string expected = $"Display shows: 0 W{Environment.NewLine}";
                Assert.AreEqual(expected, sw.ToString());
            }
        }

        [Test]
        public void ShowPower_NotZero_CorrectOutput()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                display.ShowPower(0);
                string expected = $"Display shows: 0 W{Environment.NewLine}";
                Assert.AreEqual(expected, sw.ToString());
            }
        }

        [Test]
        public void Clear_CorrectOutput()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                display.Clear();
                string expected = $"Display cleared{Environment.NewLine}";
                Assert.AreEqual(expected, sw.ToString());
            }
        }
    }
}
