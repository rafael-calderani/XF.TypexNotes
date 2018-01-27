using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace XF.VKNG.Notes.Model {
    public class Usuario {
        private static string apiURL = App.typexServiceURL + "/api/Users";
        //[PrimaryKey]
        public int Id { get; set; }

        //[MaxLength(200)]
        public string Email { get; set; }

        //[MaxLength(20)]
        public string Senha { get; set; }

        public static async Task<bool> Login(Usuario u) {
            atual = await Exists(u.Email);

            return atual != null;
        }

        public static async Task<bool> IsValid(Usuario u) {
            bool result = !string.IsNullOrEmpty(u.Email)&& !string.IsNullOrEmpty(u.Senha);
            Usuario uCheck = await Exists(u.Email);
            result = result && !(uCheck == null); // check if user exists

            return result;
        }

        public static async Task<bool> Create(Usuario u) {
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

        public static async Task<Usuario> Exists(string email) {
            List<Usuario> userList = new List<Usuario>();

            using (HttpClient httpClient = new HttpClient()) {
                var response = await httpClient.GetAsync(apiURL);
                var json = await response.Content.ReadAsStringAsync();

                userList = JsonConvert.DeserializeObject<List<Usuario>>(json);
                return userList.Find(u => u.Email == email);
            }
        }

        private static Usuario atual;
        public static Usuario Atual {
            get {
                if (atual == null) atual = new Usuario();
                return atual;
            }
        }
    }
}
