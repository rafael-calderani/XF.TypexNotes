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
    public partial class CadastroUsuarioView : ContentPage {
        public CadastroUsuarioView() {
            InitializeComponent();
        }

        private async void btnRegister_Clicked(object sener, EventArgs e) {
            string msg = "Registro salvo com sucesso.";
            Usuario u = new Usuario() {
                Email = txtEmail.Text,
                Senha = txtPassword.Text
            };

            if (await Usuario.IsValid(u) && txtPassword.Text == txtConfirmPassword.Text) {
                await Usuario.Create(u);

                var page = new ListagemNoteView();
                await Navigation.PushAsync(page);
            }
            else {
                msg = "Registro inválido, favor confirmar os dados e tentar novamente.";
            }

            await DisplayAlert("Cadastro", msg, "Ok");
        }
    }
}