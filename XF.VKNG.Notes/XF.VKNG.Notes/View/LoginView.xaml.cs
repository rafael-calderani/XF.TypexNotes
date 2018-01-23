using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XF.VKNG.Notes.View {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginView : ContentPage {
        public LoginView() {
            InitializeComponent();
        }

        private void Login_Clicked(object sender, EventArgs e) {



            var page = new ListagemNoteView(); 
            Navigation.PushAsync(page);
        }
    }
}