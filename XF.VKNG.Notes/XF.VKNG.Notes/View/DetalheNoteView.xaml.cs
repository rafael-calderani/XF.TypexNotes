using Plugin.Geolocator;
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
    public partial class DetalheNoteView : ContentPage {
        private Note note = null;
        public DetalheNoteView() {
            InitializeComponent();

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
            var locator = CrossGeolocator.Current;
            locator.DesiredAccuracy = 50;
            var position = await locator.GetPositionAsync(TimeSpan.FromSeconds(100));
            
            note.Latitude = position.Latitude;
            note.Longitude = position.Longitude;
        }

        private async void btnSalvar_ClickedAsync(object sender, EventArgs e) {
            string msg = "Registro salvo com sucesso.";

            await GetLocation();

            note.Titulo = txtTitulo.Text;
            note.Detalhes = txtDetalhes.Text;

            bool result = await Note.Save(note);

            if (!result) {
                msg = "Ocorreu um erro ao salvar o registro.";
            }
            await DisplayAlert("Salvar", msg, "Ok");
        }
    }
}