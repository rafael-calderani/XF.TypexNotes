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

        public ListagemNoteView() {
            InitializeComponent();
        }

        protected override async void OnAppearing() {
            base.OnAppearing();

            if (UsuarioViewModel.Atual == null || UsuarioViewModel.Atual.Id == 0)
                await App.Navigate(new LoginView());

            listagemNote.ItemsSource = await NoteViewModel.List();
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

        private async void NavigateToDetails(Note note) {
            var page = new DetalheNoteView();
            page.BindingContext = note;
            await App.Navigate(page);
        }

        #endregion
    }
}