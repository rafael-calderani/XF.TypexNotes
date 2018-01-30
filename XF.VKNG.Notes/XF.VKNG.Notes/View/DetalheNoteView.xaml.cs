using Plugin.Geolocator;
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
    public partial class DetalheNoteView : ContentPage {
        private Note note = null;
        private bool gravarLocal = false;

        public DetalheNoteView() {
            InitializeComponent();
        }
        protected override async void OnAppearing() {
            base.OnAppearing();

            if (BindingContext != null) { // Edição
                this.note = BindingContext as Note;
                this.Title = note.Titulo;
            }
            else { // Nova
                this.Title = "Nova nota";
                this.note = new Note();
                BindingContext = note;

                gravarLocal = await DisplayAlert("Local da Nota", "Deseja gravar o local da nota ao salvar?", "Sim", "Não");
            }

            btnMapa.IsVisible = (note.Longitude != 0 || note.Latitude != 0);
        }
        
        /// <summary>
        /// Obtem a localização do dispositivo e armazena em note
        /// </summary>
        /// <returns></returns>
        async Task GetLocation() {
            if (CrossGeolocator.IsSupported) {
                var locator = CrossGeolocator.Current;
                locator.DesiredAccuracy = 50;
                var position = await locator.GetPositionAsync(TimeSpan.FromSeconds(100));

                note.Latitude = position.Latitude;
                note.Longitude = position.Longitude;
            }
        }

        #region Events

        private async void btnSalvar_Clicked(object sender, EventArgs e) {
            string msg = "Registro salvo com sucesso.";
            Utils.ChangeState(this.Content as ILayoutController, indProgress, true);

            if (gravarLocal) await GetLocation(); // insere local para notas novas

            bool result = await NoteViewModel.Save(note);

            if (!result) {
                msg = "Ocorreu um erro ao salvar o registro.";
            }
            Utils.ChangeState(this.Content as ILayoutController, indProgress, false);
            await DisplayAlert("Salvar", msg, "Ok");
            await App.NavigateToRoot();
        }

        private async void btnExcluir_Clicked(object sender, EventArgs e) {
            bool excluir = await DisplayAlert("Confirmar", "Tem certeza que deseja excluir esta nota?", "Sim", "Não");

            if (!excluir) return;

            Utils.ChangeState(this.Content as ILayoutController, indProgress, true);
            string msg = "Registro excluido com sucesso.";

            if (!await NoteViewModel.Delete(note.Id)) {
                msg = "Ocorreu um erro ao excluir o registro.";
            }
            Utils.ChangeState(this.Content as ILayoutController, indProgress, false);
            await DisplayAlert("Excluir", msg, "Ok");
            await App.NavigateToRoot();
        }

        private void btnMapa_Clicked(object sender, EventArgs e) {
            Device.OpenUri(new Uri(string.Format("http://maps.google.com/?q={0},{1}", note.Latitude, note.Longitude)));
        }

        #endregion
    }
}