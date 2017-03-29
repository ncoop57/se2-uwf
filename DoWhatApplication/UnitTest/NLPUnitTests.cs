using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DoWhatImplementation;
using System.IO;

namespace UnitTest
{
    [TestClass]
    public class NLPUnitTests
    {

        [TestMethod]
        public void TestCorrectSubjectIsParsedFromTheUsersCommand()
        {

            // Arrange
            Stream file = new FileStream("../../../stopwords.txt", FileMode.Open);
            DoWhat sut = new DoWhat(file);
            string userCommand = "Open Gmail";
            sut.setSTTString(userCommand);
            string STTString = sut.getSTTString();

            // Act
            sut.ProcessViaNLP(STTString);
            string result = sut.getSubject();
            // Assert
            Assert.AreEqual(result, "gmail", true);

        }

        [TestMethod]
        public void TestCorrectVerbIsParsedFromTheUsersCommand()
        {

            // Arrange
            Stream file = new FileStream("../../../stopwords.txt", FileMode.Open);
            DoWhat sut = new DoWhat(file);
            string userCommand = "Open Gmail";

            // Act
            sut.ProcessViaNLP(userCommand);

            // Assert
            Assert.AreEqual(sut.getVerb(), "open", true);

        }

    }
}
