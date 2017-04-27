using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Interfaces;
using Implementations;
using System.IO;

namespace UnitTest
{
    [TestClass]
    public class CommandTests
    {

        [TestMethod]
        public void CheckCommandOpenIsRecognized()
        {

            // Arrange
            Stream file = new FileStream("../../../DoWhat/Assets/dictionary.txt", FileMode.Open);
            string command = "open";
            IStringMatcher commandMatcher = new CommandStringMatcher(file);

            // Act
            commandMatcher.process(command);

            //Assert
            Assert.AreEqual(command, commandMatcher.KeyWord);

        }

        [TestMethod]
        public void CheckCommandSearchIsRecognized()
        {

            // Arrange
            Stream file = new FileStream("../../../DoWhat/Assets/dictionary.txt", FileMode.Open);
            string command = "search";
            IStringMatcher commandMatcher = new CommandStringMatcher(file);

            // Act
            commandMatcher.process(command);

            //Assert
            Assert.AreEqual(command, commandMatcher.KeyWord);

        }

        [TestMethod]
        public void CheckCommandCreateIsRecognized()
        {

            // Arrange
            Stream file = new FileStream("../../../DoWhat/Assets/dictionary.txt", FileMode.Open);
            string command = "create";
            IStringMatcher commandMatcher = new CommandStringMatcher(file);

            // Act
            commandMatcher.process(command);

            //Assert
            Assert.AreEqual(command, commandMatcher.KeyWord);

        }

    }
}
