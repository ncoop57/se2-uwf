using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Google.Apis.CloudSpeechAPI.v1beta1;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using System.IO;
using Google.Apis.CloudSpeechAPI.v1beta1.Data;
using DoWhatImplementation;
using System.Text;

namespace UnitTestDoWhatImplementation
{
    [TestClass]
    public class UnitTest1
    {

        /*[TestMethod]
        //coded by agl11
        public void TestSendToSpeech()
        {
            //arrange
            String inputFile = @"..\..\..\resources\audio.raw";
            DoWhat sut = new DoWhat();
            sut.setAudioFileLocation(inputFile);
            string fileString = "DoWhat-65e8c7b1824e.json";
            byte[] byteArray = Encoding.ASCII.GetBytes(fileString);
            Stream stream = new MemoryStream(byteArray);
             //act
            sut.SendToSpeech(stream);
            String result = sut.getSTTString();
            //assert
            Assert.AreEqual(" how old is the Brooklyn Bridge", result, true);
        }*/
        [TestMethod]
        //coded by agl11
        public void TestInputFile()
        {
            //arrange
            String inputFile = "";
            inputFile = @"..\..\..\resources\audio.raw";
            //act
            //assert
            Assert.IsNotNull(inputFile);
        }
        [TestMethod]
        //coded by agl11
        public void TestSetAudioFileLocation()
        {
            //arrange
            String inputFile = @"..\..\..\resources\audio.raw";
            DoWhat sut = new DoWhat();
            //act
            sut.setAudioFileLocation(inputFile);
            String result = sut.getAudioFileLocation();
            //assert
            Assert.AreEqual(result, inputFile, true);
        }
        [TestMethod]
        //coded by agl11
        public void TestSetAndGetVerb()
        {
            //arrange
            string testVerb = "open";
            DoWhat sut = new DoWhat();
            //act
            sut.setVerb(testVerb);
            string result = sut.getVerb();
            //assert
            Assert.AreEqual(result, testVerb, true);
        }
        [TestMethod]
        //coded by agl11
        public void TestSetandGetSubject()
        {
            //arrange
            string testSubject = "Todoist";
            DoWhat sut = new DoWhat();
            //act
            sut.setSubject(testSubject);
            string result = sut.getSubject();
            //assert
            Assert.AreEqual(result, testSubject, true);
        }
        
        
    }
}