using FakeItEasy;
using FluentAssertions;
using NUnit.Framework;

namespace Battleships.Tests
{
    internal class ConsoleCoordinatesReaderUnitTests
    {
        private ConsoleCoordinatesReader _subject;
        private ConsolePrinter _consolePrinterDouble;
        private ConsoleReader _consoleReaderDouble;

        [SetUp]
        public void SetUp()
        {
            _consolePrinterDouble = A.Fake<ConsolePrinter>();
            _consoleReaderDouble = A.Fake<ConsoleReader>();

            _subject = new ConsoleCoordinatesReader(_consolePrinterDouble, _consoleReaderDouble);
        }

        [TestCase("A0")]
        [TestCase("B1")]
        [TestCase("C2")]
        [TestCase("D3")]
        [TestCase("E4")]
        [TestCase("f5")]
        [TestCase("G6")]
        [TestCase("h7")]
        [TestCase("I8")]
        [TestCase("J9")]
        public void ShouldAcceptCoordinateInputsWithLowerAndUpperCases(string input)
        {
            A.CallTo(() => _consoleReaderDouble.Read()).Returns(input);

            _subject.ReadInput().Should().Be(input.ToUpper());
        }

        [TestCase("")]
        [TestCase(" ")]
        [TestCase("2C")]
        [TestCase("aa")]
        [TestCase("22")]
        [TestCase("asd22")]
        [TestCase("!@#$%^&*()")]
        public void ShouldPrintErrorMessageUntilInputIsValid(string input)
        {
            A.CallTo(() => _consoleReaderDouble.Read()).Returns("A1");
            A.CallTo(() => _consoleReaderDouble.Read()).Returns(input).Twice();

            _subject.ReadInput().Should().Be("A1");

            A.CallTo(() => _consolePrinterDouble.Print("Invalid shot. Shoot between A0 and J9"))
                .MustHaveHappenedTwiceExactly();
        }
    }
}
