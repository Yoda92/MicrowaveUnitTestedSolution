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
        private IUserInterface _userinterface;

        [SetUp]
        public void Setup()
        {
            _output = Substitute.For<IOutput>();
            _display = new Display(_output);
            _powertube = new PowerTube(_output);
            _timer = new Timer();
            _userinterface = Substitute.For<IUserInterface>();
            _cookcontroller = new CookController(_timer, _display, _powertube, _userinterface);
        }

        [TestCase(50, 2)]
        [TestCase(99, 5)]
        [TestCase(20, 8)]
        public void StartCookingShouldTurnOnPowerTubeAndStartATimer(int power, int seconds) {
            _cookcontroller.StartCooking(power, seconds);
            _output.Received().OutputLine(Arg.Is<string>(str => str == $"PowerTube works with {power}"));

            Thread.Sleep(1500);

            int min, sec;
            min = (seconds - 1) / 60;
            sec = (seconds - 1) % 60;

            _output.Received().OutputLine(Arg.Is<string>(str => str == $"Display shows: {min:D2}:{sec:D2}"));
        }

        [Test]
        public void StartCookingShouldTurnOffPowerAndShowCookingIsDoneWhenTimerExpires() {
            int seconds = 2;
            int power = 40;
            _cookcontroller.StartCooking(power, seconds);
            
            _output.Received().ClearReceivedCalls();
            Thread.Sleep(3000);
            _output.Received().OutputLine(Arg.Is<string>(str => str == "PowerTube turned off"));
            _userinterface.Received().CookingIsDone();       
        }

        [Test]
        public void StopCookingDoesNothingIfNotAlreadyCooking() {
            _cookcontroller.Stop();

            _output.DidNotReceive().OutputLine(Arg.Is<string>(str => str == "PowerTube turned off"));
        }

        [Test]
        public void StopCookingTurnsOffPowerTubeAndTimer() {
            _cookcontroller.StartCooking(50, 3);
            _output.ClearReceivedCalls();

            _cookcontroller.Stop();

            _output.Received().OutputLine(Arg.Is<string>(str => str == "PowerTube turned off"));
            _output.ClearReceivedCalls();
            Thread.Sleep(1500);
            _output.DidNotReceive().OutputLine(Arg.Any<string>());
        }

    }

}
