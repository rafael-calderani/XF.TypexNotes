using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using XF.VKNG.Notes.Model;

namespace XF.VKNG.Notes.ViewModel {
    public class UsuarioViewModel {
        private static string apiURL = "http://vkngnotes.azurewebsites.net/api/Users";

        public static async Task<bool> Login(User u) {
            List<User> userList = new List<User>();

            using (HttpClient httpClient = new HttpClient()) {
                var response = await httpClient.GetAsync(apiURL, HttpCompletionOption.ResponseContentRead);
                string json = await response.Content.ReadAsStringAsync();


                userList = JsonConvert.DeserializeObject<List<User>>(json);
                atual = userList.Find(user => (user.Email == u.Email && user.Senha == u.Senha));

                return (atual != null);
            }
        }

        public static bool IsValid(User u) {
            bool result = !string.IsNullOrEmpty(u.Email) && !string.IsNullOrEmpty(u.Senha);

            return result;
        }

        public static async Task<bool> Create(User u) {
            bool result = false;

            using (HttpClient httpClient = new HttpClient()) {
                string userJson = JsonConvert.SerializeObject(u);

                HttpResponseMessage response = await httpClient.PostAsync(apiURL, new StringContent(userJson, Encoding.UTF8, "application/json"));

                result = response.IsSuccessStatusCode;
                if (result) { atual = u; }
            }

            return result;
        }

        public static async Task<bool> Delete(int userId) {
            bool result = false;
            using (HttpClient httpClient = new HttpClient()) {
                HttpResponseMessage response = await httpClient.DeleteAsync(apiURL + "/" + userId);

                result = response.IsSuccessStatusCode;
            }

            return result;
        }

        public static async Task<bool> Exists(string email) {
            List<User> userList = new List<User>();

            using (HttpClient httpClient = new HttpClient()) {
                var response = await httpClient.GetAsync(apiURL, HttpCompletionOption.ResponseContentRead);
                string json = await response.Content.ReadAsStringAsync();

                userList = JsonConvert.DeserializeObject<List<User>>(json);
                return userList.Exists(u => u.Email == email);
            }
        }

        private static User atual;
        public static User Atual {
            get {
                if (atual == null) atual = new User();
                return atual;
            }
        }
    }
}
