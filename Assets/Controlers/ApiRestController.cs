using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Models;
using Newtonsoft.Json;

namespace Controlers
{
    public static class ApiRestController
    {
        private static readonly HttpClient HttpClient = new HttpClient();
        private static readonly string UrlApi = "https://localhost:7223/api";

        private static Task<HttpResponseMessage> PostAsync(string endUrl, string jsonContent)
        {
            try
            {
                HttpContent content = new StringContent(jsonContent);
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json"); 

                return HttpClient.PostAsync(UrlApi + endUrl, new StringContent(jsonContent));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public static TokenAndCurrentContainerIdDto FindContainerIdForCreateRoom(string roomName)
        {
            RoomCreationDto roomCreationDto = new RoomCreationDto
            {
                RoomName = roomName
            };
            string json = JsonConvert.SerializeObject(roomCreationDto);
            HttpResponseMessage response = PostAsync("/GameContainer/FindContainerIdForCreateRoom", json).Result;
            return JsonConvert.DeserializeObject<TokenAndCurrentContainerIdDto>(response.Content.ReadAsStringAsync().Result);
        }


        public static TokenAndCurrentContainerIdDto Login(string name, string password)
        {
            IdentifierDto identifierDto = new IdentifierDto()
            {
                Name = name,
                Password = password
            };
            string json = JsonConvert.SerializeObject(identifierDto);
            HttpResponseMessage response = PostAsync("/Authentication/Login", json).Result;
            return JsonConvert.DeserializeObject<TokenAndCurrentContainerIdDto>(response.Content.ReadAsStringAsync().Result);
        }

        public static TokenAndCurrentContainerIdDto SignUp(string name, string email, string password, string passwordConfirm)
        {
            RegisterDto registerDto = new RegisterDto
            {
                Name = name,
                Email = email,
                Password = password,
                PasswordConfirmation = passwordConfirm
            };
            string json = JsonConvert.SerializeObject(registerDto);
            Task<HttpResponseMessage> response = PostAsync("/Authentication/SignUp", json);
            return JsonConvert.DeserializeObject<TokenAndCurrentContainerIdDto>(response.Result.Content.ReadAsStringAsync().Result);
        }
    }
}