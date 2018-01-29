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
    public partial class LoginView : ContentPage {
        public LoginView() {
            InitializeComponent();
        }

        protected override void OnAppearing() {
            base.OnAppearing();
            NavigationPage.SetHasBackButton(this, false);
        }

        protected override void OnDisappearing() {
            base.OnDisappearing();
        }

        private async void btnLogin_Clicked(object sender, EventArgs e) {
            indProgress.IsRunning = true;
            User usuario = new User() {
                Email = txtEmail.Text,
                Senha = txtPassword.Text
            };
            bool loginSuccessful = await UsuarioViewModel.Login(usuario);

            if (!loginSuccessful) {
                await DisplayAlert("Login", "Email ou senha inválidos, favor tentar novamente.", "Ok");
                return;
            }

            indProgress.IsRunning = false;

            await App.NavigateToRoot();
        }

        private async void btnRegister_Clicked(object sender, EventArgs e) {
            var page = new CadastroUsuarioView();
            await App.Navigate(page);
        }
    }
}