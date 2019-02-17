using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Reflection;

namespace RoverExercise
{
    public class FileHelper
    {
        
        public static void SavePhotoFromUrl(string ImageUrl, string ImagePath)
        {
            try
            {
                using (WebClient wc = new WebClient())
                {
                    wc.DownloadFile(new System.Uri(ImageUrl), ImagePath);
                }
                Logger.LogMessage(String.Format("Photo saved to {0}.", ImagePath));
            }
            catch (Exception e)
            {
                Logger.LogMessage(String.Format("Save photo failed: {0}.", e.Message));
            }
        }
    }
}
