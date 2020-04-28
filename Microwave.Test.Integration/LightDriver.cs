using System;
using System.IO;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Interfaces;
using NUnit.Framework;

namespace Microwave.Test.Integration
{
    [TestFixture]
    public class LightDriver
    {
        private IOutput _output;
        private ILight _light;
        
        
        [SetUp]
        public void Setup()
        {
            _output = new Output();
            _light = new Light(_output);
        }
        
        [Test]
        public void TurnOn_WasOff_CorrectOutput()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);    
                _light.TurnOn();
                string expected = $"Light is turned on{Environment.NewLine}";
                Assert.AreEqual(expected, sw.ToString());
            }
        }

        [Test]
        public void TurnOff_WasOn_CorrectOutput()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);    
                _light.TurnOn();
                _light.TurnOff();
                string expected = $"Light is turned on{Environment.NewLine}Light is turned off{Environment.NewLine}";
                Assert.AreEqual(expected, sw.ToString());
            }
        }

        [Test]
        public void TurnOn_WasOn_CorrectOutput()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);    
                _light.TurnOn();
                _light.TurnOn();
                string expected = $"Light is turned on{Environment.NewLine}";
                Assert.AreEqual(expected, sw.ToString());
            }
        }

        [Test]
        public void TurnOff_WasOff_CorrectOutput()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);    
                _light.TurnOff();
                string expected = $"";
                Assert.AreEqual(expected, sw.ToString());
            }
        }

        
    }
}