using System.Threading.Tasks;
using Xamarin.Forms;
using XF.VKNG.Notes.ViewModel;

namespace XF.VKNG.Notes {
    public partial class App : Application {

        public static string typexServiceURL = "http://vkngnotes.azurewebsites.net/";
        private static MasterDetailPage mainPage;

        public static async Task Navigate(Page page) {
            mainPage.IsPresented = false;
            await mainPage.Detail.Navigation.PushAsync(page);
        }

        public App() {
            InitializeComponent();
            mainPage = new View.MainView();
            MainPage = mainPage;
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
