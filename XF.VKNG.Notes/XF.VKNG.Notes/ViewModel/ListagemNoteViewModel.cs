using System;
using System.Collections.Generic;
using System.Text;
using XF.VKNG.Notes.Model;

namespace XF.VKNG.Notes.ViewModel
{
   public class ListagemNoteViewModel
    {
    
        public List<Note> Note { get; set; }

        public ListagemNoteViewModel()
        {
            this.Note = new ListagemNote().Note;
         
        }

    }
}
