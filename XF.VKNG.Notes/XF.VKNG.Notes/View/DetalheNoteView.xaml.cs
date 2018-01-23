using Plugin.Geolocator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using XF.VKNG.Notes.Model;

namespace XF.VKNG.Notes.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DetalheNoteView : ContentPage
    { 
        public DetalheNoteView (Note note)
		{
			InitializeComponent ();

            this.Title = note.Titulo;

        
             
        }


        //exemplo de geolocation para salvar as notas
        async void GetLocation()
        {
            var locator = CrossGeolocator.Current;
            locator.DesiredAccuracy = 50;
            var position = await locator.GetPositionAsync(TimeSpan.FromSeconds(100));

            double latiude = position.Latitude;
            double longitude = position.Longitude;
            Convert.ToString(longitude);
            DisplayAlert("longitude!", Convert.ToString(longitude), "OK");
        }

        private void buttonSalvar_Clicked(object sender, EventArgs e)
        {
            GetLocation();
        }
	}
}