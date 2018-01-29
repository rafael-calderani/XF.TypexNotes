using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XF.VKNG.Notes.ViewModel;

namespace XF.VKNG.Notes.View {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuHamburguer : ContentPage {
        public MenuHamburguer() {
            InitializeComponent();

            btnNotes.Clicked += async (sender, e) => {
                if (UsuarioViewModel.Atual.Id > 0) {
                    await App.NavigateToRoot();
                }
                else {
                    await DisplayAlert("Aviso", "É necessário estar logado para realizar esta ação.", "Ok");
                }
            };
            
            btnShare.Clicked += async (sender, e) => {
                //TODO: Compartilhar algo (logo do app talvez)
            };

            btnDelete.Clicked += async (sender, e) => {
                if (UsuarioViewModel.Atual.Id > 0) {
                    // Exclui todas as notas do usuario logado
                    bool apagar = await DisplayAlert("Confrmar", "Você tem certeza que deseja apagar todas as suas notas?", "Sim", "Não");
                    if (apagar) await NoteViewModel.DeleteByUser(UsuarioViewModel.Atual.Id);
                }
                else {
                    await DisplayAlert("Aviso", "É necessário estar logado para realizar esta ação.", "Ok");
                }
            };

            btnAbout.Clicked += async (sender, e) => {
                await App.Navigate(new SobreView());
            };

            btnExit.Clicked += async (sender, e) => {
                UsuarioViewModel.Logout();


                ListagemNoteView rootView = ((App.Current.MainPage as MasterDetailPage).Detail as NavigationPage).RootPage as ListagemNoteView;
                rootView.RefreshListSource(); // Atualiza a lista de notas antes de exibir para o usuário

                //await App.NavigateToRoot();
            };
        }
    }
}