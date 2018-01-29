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
                string senha = await InputBox(this.Navigation);
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

        public static Task<string> InputBox(INavigation navigation) {
            // wait in this proc, until user did his input 
            var tcs = new TaskCompletionSource<string>();

            var lblTitle = new Label { Text = "Desbloqueio de nota", HorizontalOptions = LayoutOptions.Center, FontAttributes = FontAttributes.Bold };
            var txtInput = new Entry { Text = "", Placeholder="Digite a senha da nota.", IsPassword = true };

            var btnOk = new Button {
                Text = "Ok",
                WidthRequest = 100,
                BackgroundColor = Color.FromHex("#FF6600"),
            };
            btnOk.Clicked += async (s, e) => {
                // close page
                var result = txtInput.Text;
                await navigation.PopModalAsync();
                // pass result
                tcs.SetResult(result);
            };

            var btnCancel = new Button {
                Text = "Voltar",
                WidthRequest = 100,
                BackgroundColor = Color.FromRgb(0.8, 0.8, 0.8)
            };
            btnCancel.Clicked += async (s, e) => {
                await navigation.PopModalAsync();
                // pass empty result
                tcs.SetResult(null);
            };

            var slButtons = new StackLayout {
                Orientation = StackOrientation.Horizontal,
                Children = { btnOk, btnCancel },
            };

            var layout = new StackLayout {
                Padding = new Thickness(0, 40, 0, 0),
                VerticalOptions = LayoutOptions.StartAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                Orientation = StackOrientation.Vertical,
                Children = { lblTitle, txtInput, slButtons },
            };

            // create and show page
            var page = new ContentPage();
            page.Content = layout;
            navigation.PushModalAsync(page);
            // open keyboard
            txtInput.Focus();

            // code is waiting her, until result is passed with tcs.SetResult() in btn-Clicked
            // then proc returns the result
            return tcs.Task;
        }
    }
}