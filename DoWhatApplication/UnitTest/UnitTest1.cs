using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Google.Apis.CloudSpeechAPI.v1beta1;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using System.IO;
using Google.Apis.CloudSpeechAPI.v1beta1.Data;
using DoWhatImplementation;

namespace UnitTestDoWhatImplementation
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        //coded by agl11
        public void TestCreateAuthorizedClient()
        {
            //arrange

            //act
            CloudSpeechAPIService service = DoWhat.CreateAuthorizedClient();
            //assert
            Assert.IsNotNull(service);
        }
        [TestMethod]
        //coded by agl11
        public void TestSendToSpeech()
        {
            //arrange
            String inputFile = @"..\..\..\resources\audio.raw";
            DoWhat sut = new DoWhat();
            sut.setAudioFileLocation(inputFile);
            //act
            sut.SendToSpeech();
            String result = sut.getSTTString();
            //assert
            Assert.AreEqual(" how old is the Brooklyn Bridge", result, true);
        }
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
        
        
    }
}