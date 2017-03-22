using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DoWhatImplementation;

namespace UnitTest
{
    [TestClass]
    public class NLPUnitTests
    {

        [TestMethod]
        public void TestCorrectSubjectIsParsedFromTheUsersCommand()
        {

            // Arrange
            DoWhat sut = new DoWhat();
            string userCommand = "Open Gmail";

            // Act
            sut.ProcessViaNLP(userCommand);

            // Assert
            Assert.AreEqual(sut.getSubject(), "gmail");

        }

        [TestMethod]
        public void TestCorrectVerbIsParsedFromTheUsersCommand()
        {

            // Arrange
            DoWhat sut = new DoWhat();
            string userCommand = "Open Gmail";

            // Act
            sut.ProcessViaNLP(userCommand);

            // Assert
            Assert.AreEqual(sut.getVerb(), "open");

        }

    }
}
