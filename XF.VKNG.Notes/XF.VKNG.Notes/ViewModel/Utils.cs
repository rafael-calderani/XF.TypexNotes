using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XF.VKNG.Notes.ViewModel {
    public class Utils {
        public static Task<string> CustomPromptDialog(INavigation navigation) {
            // wait in this proc, until user did his input 
            var tcs = new TaskCompletionSource<string>();

            var lblTitle = new Label { Text = "Desbloqueio de nota", HorizontalOptions = LayoutOptions.Center, FontAttributes = FontAttributes.Bold };
            var txtInput = new Entry { Text = "", Placeholder = "Digite a senha da nota.", IsPassword = true };

            var btnOk = new Button {
                Text = "Ok",
                WidthRequest = 100,
                BackgroundColor = Color.FromHex("#FF6600"),
            };
            btnOk.Clicked += async (s, e) => {
                // close page
                var result = txtInput.Text;
                await navigation.PopModalAsync();
                // pass result
                tcs.SetResult(result);
            };

            var btnCancel = new Button {
                Text = "Voltar",
                WidthRequest = 100,
                BackgroundColor = Color.FromRgb(0.8, 0.8, 0.8)
            };
            btnCancel.Clicked += async (s, e) => {
                await navigation.PopModalAsync();
                // pass empty result
                tcs.SetResult(null);
            };

            var slButtons = new StackLayout {
                Orientation = StackOrientation.Horizontal,
                Children = { btnOk, btnCancel },
            };

            var layout = new StackLayout {
                Padding = new Thickness(0, 40, 0, 0),
                VerticalOptions = LayoutOptions.StartAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                Orientation = StackOrientation.Vertical,
                Children = { lblTitle, txtInput, slButtons },
            };

            // create and show page
            var page = new ContentPage();
            page.Content = layout;
            navigation.PushModalAsync(page);
            // open keyboard
            txtInput.Focus();

            // code is waiting her, until result is passed with tcs.SetResult() in btn-Clicked
            // then proc returns the result
            return tcs.Task;
        }

        /// <summary>
        /// Habilita / Desabilita os controles da página enquanta a mesma está processando os dados
        /// </summary>
        /// <param name="state"></param>
        public static void ChangeState(ILayoutController layout, ActivityIndicator ind, bool busyState) {
            ind.IsRunning = busyState;

            if (layout == null) return;

            foreach (VisualElement child in layout.Children) {
                child.IsEnabled = !busyState;
            }
        }
    }
}
