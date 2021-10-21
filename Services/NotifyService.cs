using AdrianP.Models;
using AdrianP.Models.interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AdrianP.Services
{
    public class NotifyService : INotifyService
    {
        private static readonly HttpClient client = new HttpClient();

        public async Task<ApiResponseList> Listar()
        {
            WebRequest request = WebRequest.Create(
              "http://api-homo-send-notify.herokuapp.com/api/v1/UserNotify");

            request.Credentials = CredentialCache.DefaultCredentials;

            WebResponse response = await request.GetResponseAsync();
            string responseFromServer;

            Console.WriteLine(((HttpWebResponse)response).StatusDescription);

            using (Stream dataStream = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(dataStream);
                responseFromServer = reader.ReadToEnd();
                Console.WriteLine(responseFromServer);
            }

            response.Close();
            return JsonConvert.DeserializeObject<ApiResponseList>(responseFromServer);
        }

        public async Task<ApiResponseNotify> ListarNotify()
        {
            WebRequest request = WebRequest.Create(
               "http://api-homo-send-notify.herokuapp.com/api/v1/UserNotify/Listar");

            request.Credentials = CredentialCache.DefaultCredentials;

            WebResponse response = await request.GetResponseAsync();
            string responseFromServer;

            Console.WriteLine(((HttpWebResponse)response).StatusDescription);

            using (Stream dataStream = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(dataStream);
                responseFromServer = reader.ReadToEnd();
                Console.WriteLine(responseFromServer);
            }

            response.Close();
            return JsonConvert.DeserializeObject<ApiResponseNotify>(responseFromServer);
        }

        public async Task Register(UserNotify entity)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://api-homo-send-notify.herokuapp.com/api/v1/UserNotify");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = "{\"userIdentity\":\""+ entity.userIdentity + "\"," +
                              "\"endPoint\":\""+ entity.endPoint + "\", " +
                              "\"p256dh\":\""+ entity.p256dh + "\", "+
                              "\"auth\": \""+ entity.auth + "\"}";

                streamWriter.Write(json);
            }

            var httpResponse = await httpWebRequest.GetResponseAsync();

            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                Console.WriteLine(result);
            }
        }
    }
}
