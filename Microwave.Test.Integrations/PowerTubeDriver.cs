using System;
using System.IO;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Interfaces;
using NSubstitute;
using NSubstitute.Core.Arguments;
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
                string expected = $"PowerTube works with {power}";
                Assert.AreEqual(expected, sw.ToString());
            }
        }

        

        [TestCase(-5)]
        [TestCase(-1)]
        [TestCase(0)]
        [TestCase(101)]
        [TestCase(150)]
        public void TurnOn_WasOffOutOfRangePower_ThrowsException(int power)
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);    
                _powerTube.TurnOn(power);
                string expected = $"power, {power}, Must be between 1 and 100 (incl.){Environment.NewLine}";
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

        [Test]
        public void TurnOn_WasOn_ThrowsException()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);    
                _powerTube.TurnOn(50);
                string expected = $"PowerTube.TurnOn: is already on{Environment.NewLine}";
                Assert.AreEqual(expected, sw.ToString());
            }
        }
    }
}