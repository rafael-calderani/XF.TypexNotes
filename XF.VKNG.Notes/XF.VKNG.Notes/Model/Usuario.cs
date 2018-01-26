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
        public Guid Id { get; set; }

        //[MaxLength(200)]
        public string Email { get; set; }

        //[MaxLength(20)]
        public string Senha { get; set; }

        public static async Task<bool> Login(Usuario u) {
            bool result = await Exists(u.Email);

            if (result) { atual = u; }

            return result;
        }

        public static async Task<bool> IsValid(Usuario u) {
            bool result = !string.IsNullOrEmpty(u.Email)&& !string.IsNullOrEmpty(u.Senha);

            result = result && !(await Exists(u.Email)); // check if user exists

            return result;
        }

        public static async Task<bool> Create(Usuario u) {
            bool result = false;

            using(MD5 md5 = MD5.Create()) { // Cria a GUID baseada no Email do usuário
                u.Id = new Guid(md5.ComputeHash(Encoding.Default.GetBytes(u.Email)));
            }
            

            using (HttpClient httpClient = new HttpClient()) {
                string noteJson = JsonConvert.SerializeObject(u);

                HttpResponseMessage response = await httpClient.PostAsync(apiURL, new StringContent(noteJson));

                result = response.IsSuccessStatusCode;
                if (result) { atual = u; }
            }

            return result;
        }

        public static async Task<bool> Delete(Guid userId) {
            bool result = false;
            using (HttpClient httpClient = new HttpClient()) {
                HttpResponseMessage response = await httpClient.DeleteAsync(apiURL + "/" + userId);

                result = response.IsSuccessStatusCode;
            }

            return result;
        }

        public static async Task<bool> Exists(string email) {
            List<Usuario> userList = new List<Usuario>();

            using (HttpClient httpClient = new HttpClient()) {
                var response = await httpClient.GetAsync(apiURL);
                var json = await response.Content.ReadAsStringAsync();

                userList = JsonConvert.DeserializeObject<List<Usuario>>(json);
                return userList.Exists(u => u.Email == email);
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
