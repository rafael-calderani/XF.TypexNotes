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
    public partial class MenuHamburguer : ContentPage {
        public MenuHamburguer() {
            InitializeComponent();

            btnNotes.Clicked += async (sender, e) => {
                if (Usuario.Atual.Id > 0) {
                    await App.Navigate(new ListagemNoteView());
                }
                else {
                    await DisplayAlert("Aviso", "É necessário estar logado para realizar esta ação.", "Ok");
                }
            };
            
            btnShare.Clicked += async (sender, e) => {
                //TODO: Compartilhar algo (logo do app talvez)
            };

            btnDelete.Clicked += async (sender, e) => {
                if (Usuario.Atual.Id > 0) {
                    //TODO: Excluir todas as notas do usuario logado
                }
                else {
                    await DisplayAlert("Aviso", "É necessário estar logado para realizar esta ação.", "Ok");
                }
            };

            btnAbout.Clicked += async (sender, e) => {
                await App.Navigate(new SobreView());
            };
        }
    }
}