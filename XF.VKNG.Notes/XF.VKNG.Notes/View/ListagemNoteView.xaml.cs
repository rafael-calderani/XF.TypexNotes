//using Acr.UserDialogs;
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

        public void RefreshListSource() {
            OnAppearing();
        }

        #region Events

        private async void listagemNote_ItemTapped(object sender, ItemTappedEventArgs e) { 
            var note = (Note)e.Item;

            if (string.IsNullOrEmpty(note.Senha)) {
                var page = new DetalheNoteView() { BindingContext = note };
                await App.Navigate(page);
            }
            else {
                string senha = await Utils.CustomPromptDialog(this.Navigation);
                if (senha == note.Senha) {
                    var page = new DetalheNoteView() { BindingContext = note };
                    await App.Navigate(page);
                }
                else {
                    await DisplayAlert("Validação", "Senha incorreta.", "Ok");
                }
            }

            listagemNote.SelectedItem = null;
        }

        private async void tbNewNote_Clicked(object sender, EventArgs e) {
            await App.Navigate(new DetalheNoteView());
        }

        #endregion
    }
}