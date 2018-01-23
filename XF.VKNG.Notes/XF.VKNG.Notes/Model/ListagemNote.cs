using System;
using System.Collections.Generic;
using System.Text;

namespace XF.VKNG.Notes.Model
{
    public class ListagemNote
    {
        public List<Note> Note { get; }

        public ListagemNote()
        {
            this.Note = new List<Note> { 
               new Note { Titulo = "titulo", Detalhes = "detalhe", Latitude = 41220.00, Longitude = 3210.00 },
               new Note { Titulo = "titulo2", Detalhes = "detalhe2", Latitude = 33420.00, Longitude = 4420.00 },
               new Note { Titulo = "titulo3", Detalhes = "detalhe3", Latitude = 33420.00, Longitude = 4420.00 },
               new Note { Titulo = "titulo4", Detalhes = "detalhe4", Latitude = 33420.00, Longitude = 4420.00 },
               new Note { Titulo = "titulo5", Detalhes = "detalhe5", Latitude = 33420.00, Longitude = 4420.00 }
            };
        }

       
    }
}
