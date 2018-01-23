using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XF.VKNG.Notes.Model;

namespace XF.VKNG.Notes.View
{


  

    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ListagemNoteView : ContentPage
    {

        public List<Note> Note { get; set; }

     

        public ListagemNoteView ()
		{
			InitializeComponent ();


            // TODO: melhorar 
            //remove back
            NavigationPage.SetHasBackButton(this, false);

            listagemNote.ItemsSource = new ListagemNote().Note;
            //this.BindingContext = this;

        }

        private void listagemNote_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var note = (Note)e.Item; 
       
            var page = new DetalheNoteView(note);
            Navigation.PushAsync(page);

        }
    }
}