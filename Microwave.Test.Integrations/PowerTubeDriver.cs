using System;
using System.IO;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Interfaces;
using NUnit.Framework;

namespace Microwave.Test.Integrations
{
    [TestFixture]
    public class PowerTubeTest
    {
        private IPowerTube _powerTube;
        private IOutput _output;

        [SetUp]
        public void Setup()
        {
            _output = new Output();
            _powerTube = new PowerTube(_output);
        }

        [TestCase(1)]
        [TestCase(50)]
        [TestCase(100)]
        public void TurnOn_WasOffCorrectPower_CorrectOutput(int power)
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);    
                _powerTube.TurnOn(power);
                string expected = $"PowerTube works with {power.ToString()}{Environment.NewLine}";
                Assert.AreEqual(expected, sw.ToString());
            }
        }

        [Test]
        public void TurnOff_WasOn_CorrectOutput()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);    
                _powerTube.TurnOn(50);
                _powerTube.TurnOff();
                string expected = $"PowerTube works with 50{Environment.NewLine}PowerTube turned off{Environment.NewLine}";
                Assert.AreEqual(expected, sw.ToString());
            }
        }

        [Test]
        public void TurnOff_WasOff_NoOutput()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw); 
                _powerTube.TurnOff();
                string expected = $"";
                Assert.AreEqual(expected, sw.ToString());
            }
        }
    }
}