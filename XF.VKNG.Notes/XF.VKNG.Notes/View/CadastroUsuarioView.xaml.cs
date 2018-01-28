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
    public partial class CadastroUsuarioView : ContentPage {
        public CadastroUsuarioView() {
            InitializeComponent();
        }

        private async void btnRegister_Clicked(object sener, EventArgs e) {
            string msg = "Registro salvo com sucesso.";
            User u = new User() {
                Email = txtEmail.Text,
                Senha = txtPassword.Text
            };

            if (await UsuarioViewModel.IsValid(u) && txtPassword.Text == txtConfirmPassword.Text) {
                if (await UsuarioViewModel.Exists(u.Email) != null) {
                    msg = "Um usuário com este e-mail já existe, favor confirmar os dados e tentar novamente.";
                }
                else {
                    await UsuarioViewModel.Create(u);
                }

                var page = new ListagemNoteView();
                await App.Navigate(page);
            }
            else {
                msg = "Registro inválido, favor confirmar os dados e tentar novamente.";
            }

            await DisplayAlert("Cadastro", msg, "Ok");
        }
    }
}