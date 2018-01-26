using Xamarin.Forms;
using XF.VKNG.Notes.ViewModel;

namespace XF.VKNG.Notes {
    public partial class App : Application {

        public static string typexServiceURL = "http://vkngnotes.azurewebsites.net/";

        #region ViewModels
        public static UsuarioViewModel UsuarioVM { get; set; }
        #endregion
        public App() {
            InitializeComponent();

            MainPage = new NavigationPage(new View.LoginView() { BindingContext = App.UsuarioVM });
        }

        protected override void OnStart() {
            // Handle when your app starts
        }

        protected override void OnSleep() {
            // Handle when your app sleeps
        }

        protected override void OnResume() {
            // Handle when your app resumes
        }
    }
}
