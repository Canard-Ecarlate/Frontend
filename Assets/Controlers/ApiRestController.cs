using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Models;
using Newtonsoft.Json;
using Utils;

namespace Controlers
{
    public static class ApiRestController
    {
        private static readonly HttpClient HttpClient = new HttpClient();
        private static readonly string UrlApi = "https://localhost:7223/api";

        private static HttpResponseMessage PostAsync(string endUrl, string jsonContent)
        {
            StringContent content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            HttpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", GlobalVariable.User.Token);
            return HttpClient.PostAsync(UrlApi + endUrl, content).Result;
        }

        public static GameContainer FindContainerIdForCreateRoom(RoomCreationApiDto dto)
        {
            string json = JsonConvert.SerializeObject(dto);
            string response = PostAsync("/GameContainer/FindContainerIdForCreateRoom", json).Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<GameContainer>(response);
        }

        public static GameContainer FindContainerIdForJoinRoom(UserAndRoomDto dto)
        { 
            string json = JsonConvert.SerializeObject(dto); 
            string response = PostAsync("/GameContainer/FindContainerIdForJoinRoom", json).Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<GameContainer>(response);
        }

        /*public static TokenAndCurrentContainerIdDto Login(string name, string password)
        {
            IdentifierDto identifierDto = new IdentifierDto()
            {
                Name = name,
                Password = password
            };
            string json = JsonConvert.SerializeObject(identifierDto);
            HttpResponseMessage response = PostAsync("/Authentication/Login", json).Result;
            return JsonConvert.DeserializeObject<TokenAndCurrentContainerIdDto>(response.Content.ReadAsStringAsync()
                .Result);
        }

        public static TokenAndCurrentContainerIdDto SignUp(string name, string email, string password,
            string passwordConfirm)
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
            return JsonConvert.DeserializeObject<TokenAndCurrentContainerIdDto>(response.Result.Content
                .ReadAsStringAsync().Result);
        }*/
    }
}