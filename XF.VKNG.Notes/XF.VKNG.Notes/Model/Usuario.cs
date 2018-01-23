using System;
using System.Collections.Generic;
using System.Text;

namespace XF.VKNG.Notes.Model {
    public class Usuario {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
    }
}
