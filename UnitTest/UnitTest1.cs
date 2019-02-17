using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RoverExercise;

namespace UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public async Task TestMethodFetchPhotoFromAPI()
        {
            string rovername = "opportunity";
            DateTime requestedDate = Convert.ToDateTime("4/31/2018");
            string jsonOutput = await ImageRetriever.FetchPhotoFromAPI(rovername, requestedDate);
            bool expectedSuccessResult = false;
            bool actualSuccessResult = !String.IsNullOrEmpty(jsonOutput);
            Assert.AreEqual(expectedSuccessResult, actualSuccessResult);
        }

        [TestMethod]
        public async Task TestMethodFetchPhotoFromAPI2()
        {
            string rovername = "curiosity";
            DateTime requestedDate = Convert.ToDateTime("6/2/2018");
            string jsonOutput = await ImageRetriever.FetchPhotoFromAPI(rovername, requestedDate);
            bool expectedSuccessResult = true;
            bool actualSuccessResult = !String.IsNullOrEmpty(jsonOutput);
            Assert.AreEqual(expectedSuccessResult, actualSuccessResult);
        }
    }
}
