using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace XF.VKNG.Notes.Model {
    public class User {
        [JsonProperty("Id")]
        public int Id { get; set; }

        [JsonProperty("Email")]
        public string Email { get; set; }

        [JsonProperty("Senha")]
        public string Senha { get; set; }
    }
}
