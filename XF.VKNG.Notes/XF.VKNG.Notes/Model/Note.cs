using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using XF.VKNG.Notes.ViewModel;

namespace XF.VKNG.Notes.Model {

    public interface INote {
        void GetNotes(NoteViewModel vm);
    }
    public class Note {
        public int Id { get; set; }

        public string Titulo { get; set; }

        public string Detalhes { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public string Senha { get; set; }

        public int UserId { get; set; }
    }
}