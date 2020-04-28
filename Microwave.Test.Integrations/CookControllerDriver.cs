using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Interfaces;
using MicrowaveOvenClasses.Controllers;
using NUnit.Framework;
using NSubstitute;
using Timer = MicrowaveOvenClasses.Boundary.Timer;
using System.IO;

namespace Microwave.Test.Integrations
{
    [TestFixture]
    class CookControllerDriver
    {

        private Display _display;
        private IOutput _output;
        private PowerTube _powertube;
        private Timer _timer;
        private CookController _cookcontroller;
        private IUserInterface _userinteface;

        [SetUp]
        public void Setup()
        {
            _output = Substitute.For<IOutput>();
            _display = new Display(_output);
            _powertube = new PowerTube(_output);
            _timer = new Timer();
            _userinteface = Substitute.For<IUserInterface>();
            _cookcontroller = new CookController(_timer, _display, _powertube, _userinteface);
        }

        [TestCase(50, 2)]
        public void StartCookingShouldTurnOnPowerTubeAndStartATimer(int power, int seconds) {
            _cookcontroller.StartCooking(power, seconds);
            _output.Received().OutputLine(Arg.Is<string>(str => str == $"PowerTube works with {power}"));

            Thread.Sleep(1500);

            int min, sec;
            min = (seconds - 1) / 60;
            sec = (seconds - 1) % 60;

            _output.Received().OutputLine(Arg.Is<string>(str => str == $"Display shows: {min:D2}:{sec:D2}"));
        }

    }

}
