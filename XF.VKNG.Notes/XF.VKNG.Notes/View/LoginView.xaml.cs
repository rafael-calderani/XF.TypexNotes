using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XF.VKNG.Notes.Model;

namespace XF.VKNG.Notes.View {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginView : ContentPage {
        public LoginView() {
            InitializeComponent();
        }

        private async void btnLogin_Clicked(object sender, EventArgs e) {
            Usuario usuario = new Usuario() {
                Email = txtEmail.Text,
                Senha = txtPassword.Text
            };
            bool loginSuccessful = await Usuario.Login(usuario);

            if (!loginSuccessful) {
                // TODO: Show popup message at the top of the screen
                await DisplayAlert("Login", "Email ou senha inválidos, favor tentar novamente.", "Ok");
                return;
            }

            var page = new ListagemNoteView();
            await Navigation.PushAsync(page);
        }

        private void btnRegister_Clicked(object sener, EventArgs e) {

            var page = new CadastroUsuarioView();
            Navigation.PushAsync(page);
        }
    }
}