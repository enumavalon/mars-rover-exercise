using System;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using System.IO;

namespace RoverExercise
{
    public static class ImageRetriever
    {
        const string API_URL = "https://mars-photos.herokuapp.com/";

        public static async Task<string> FetchPhotoFromAPI(string RoverName, DateTime RequestedDate)
        {
            string json = string.Empty;
            try
            {
                string api_query = String.Format("api/v1/rovers/{0}/photos?earth_date={1:yyyy-MM-dd}", RoverName, RequestedDate);
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(API_URL);
                HttpResponseMessage response = await client.GetAsync(api_query);
                if (response.IsSuccessStatusCode)
                {
                    json = await response.Content.ReadAsStringAsync();
                    Logger.LogMessage("Response retrieved");
                }
                else
                {
                    Logger.LogMessage("Unable to retrieve photos: " + response.StatusCode);
                }
            }
            catch (Exception e)
            {
                Logger.LogMessage("Unable to retrieve photos: " + e.Message);
            }
            return json;
        }

        public static async Task DownloadPhotos(string RoverName, DateTime RequestedDate, string ImageFolder)
        {
            string json = await FetchPhotoFromAPI(RoverName, RequestedDate);
            JObject photos = JObject.Parse(json);
            int photonum = 0;
            foreach (var item in photos["photos"])
            {
                string id = (string)item["id"];
                string imgsrc = (string)item["img_src"];
                string imgpath = Path.Combine(ImageFolder, id + ".jpg");
                FileHelper.SavePhotoFromUrl(imgsrc, imgpath);
                photonum++;
            }
            Logger.LogMessage("Photos retrieved: " + photonum.ToString());
        }

    }
}
