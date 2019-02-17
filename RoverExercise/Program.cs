using System;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;
using System.Net.Http;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.ComponentModel;
using System.Threading;

namespace RoverExercise
{
    class Program
    {
        #region DECLARATIONS
        static string BASE_PATH = Directory.GetCurrentDirectory();
        static string DATES_PATH = Path.Combine(BASE_PATH, @".\Files\dates.txt");
        static string PHOTOS_PATH = Path.Combine(BASE_PATH, @".\Photos");
        #endregion

        static void Main(string[] args)
        {
            try
            {
                // Read dates from file. Store into string array
                string[] dates = File.ReadAllLines(DATES_PATH);

                // Identify which rover we will take photos from
                string rovername = GetRoverName();

                //For each date retrieve photos
                foreach (string date in dates)
                {
                    Logger.LogMessage(String.Format("Attempting to fetch photo for {0}", date));
                    if (DateTime.TryParse(date, out DateTime requestedDate))
                    {
                        string imgfolder = SetupImageFolder(rovername, requestedDate);
                        ImageRetriever.DownloadPhotos(rovername, requestedDate, imgfolder).Wait();
                    }
                    else
                    {
                        Logger.LogMessage(String.Format("{0} is not a valid date.", date));
                    }
                }
                
                PrintDivider();
                Console.WriteLine("Exercise completed.");
                Console.ReadKey();
            }
            catch (Exception e)
            {
                Logger.LogMessage("Unexpected error encountered: " + e.Message);
            }
        }

        #region PRIVATE METHODS
        private static void PrintDivider()
        {
            Console.WriteLine("=========================================");
        }
        private static void PrintChoices()
        {
            Console.WriteLine("Choose the number of the rover from which you want to recover photos from:");
            Console.WriteLine("1- Curiosity");
            Console.WriteLine("2- Spirit");
            Console.WriteLine("3- Opportunity");
        }

        private static string GetRoverName()
        {
            string rover = string.Empty;
            PrintChoices();
            do
            {
                ConsoleKeyInfo key = Console.ReadKey();
                switch (key.KeyChar)
                {
                    case '1':
                        rover = "curiosity";
                        break;
                    case '2':
                        rover = "spirit";
                        break;
                    case '3':
                        rover = "opportunity";
                        break;
                    default:
                        Console.WriteLine("[" + key.KeyChar.ToString() +"] is not a valid input. Kindly choose a number from the available choices");
                        break;
                }
            }while(String.IsNullOrEmpty(rover));
            return rover;
        }
        
        private static string SetupImageFolder(string RoverName, DateTime RequestedDate)
        {
            string imgpath = string.Empty;
            string imgfolder = Path.Combine(PHOTOS_PATH, RoverName, RequestedDate.ToString("yyyyMMdd"));
            Directory.CreateDirectory(imgfolder);
            return imgfolder;
        }
        #endregion
    }
}
