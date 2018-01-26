using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XF.VKNG.Notes.Model;
using XF.VKNG.Notes.ViewModel;

namespace XF.VKNG.Notes.View {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListagemNoteView : ContentPage {
        public NoteViewModel notesVM = new NoteViewModel();

        public ListagemNoteView() {
            this.BindingContext = notesVM;

            InitializeComponent();


            // TODO: melhorar 
            //remove back
            NavigationPage.SetHasBackButton(this, false);

            INote lista = DependencyService.Get<INote>();
            lista.GetNotes(notesVM);
        }

        protected override async void OnAppearing() {
            base.OnAppearing();

            listagemNote.ItemsSource = await Note.List();
        }

        #region Events

        private void listagemNote_ItemTapped(object sender, ItemTappedEventArgs e) { // TODO: Isso deve ficar na view model!!!
            var note = (Note)e.Item;

            NavigateToDetails(note);

            listagemNote.SelectedItem = null;
        }

        private void tbNewNote_Clicked(object sender, EventArgs e) {
            NavigateToDetails(null);
        }

        #endregion

        #region Methods

        private void NavigateToDetails(Note note) {
            var page = new DetalheNoteView();
            page.BindingContext = note;
            Navigation.PushAsync(page);
        }

        #endregion
    }
}