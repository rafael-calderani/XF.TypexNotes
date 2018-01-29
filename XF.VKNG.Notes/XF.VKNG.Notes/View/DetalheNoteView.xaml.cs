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
        public DetalheNoteView() {
            InitializeComponent();
        }
        protected override void OnAppearing() {
            base.OnAppearing();

            if (BindingContext != null) { // Edição
                this.note = BindingContext as Note;
                this.Title = note.Titulo;
            }
            else { // Nova
                this.Title = "Nova nota";
                this.note = new Note();
                BindingContext = note;
            }
        }

        //exemplo de geolocation para salvar as notas
        async Task GetLocation() {
            if (CrossGeolocator.IsSupported) {
                var locator = CrossGeolocator.Current;
                locator.DesiredAccuracy = 50;
                var position = await locator.GetPositionAsync(TimeSpan.FromSeconds(100));

                note.Latitude = position.Latitude;
                note.Longitude = position.Longitude;
            }
        }

        private async void btnSalvar_Clicked(object sender, EventArgs e) {
            string msg = "Registro salvo com sucesso.";
            indProgress.IsRunning = true;

            if (note.Id == 0) await GetLocation(); // atualiza local apenas para notas novas

            bool result = await NoteViewModel.Save(note);

            if (!result) {
                msg = "Ocorreu um erro ao salvar o registro.";
            }
            indProgress.IsRunning = false;
            await DisplayAlert("Salvar", msg, "Ok");
            await App.NavigateToRoot();
        }

        private async void btnExcluir_Clicked(object sender, EventArgs e) {
            string msg = "Registro excluido com sucesso.";
            indProgress.IsRunning = true;

            if (!await NoteViewModel.Delete(note.Id)) {
                msg = "Ocorreu um erro ao excluir o registro.";
            }
            indProgress.IsRunning = false;
            await DisplayAlert("Excluir", msg, "Ok");
            await App.NavigateToRoot();
        }
    }
}