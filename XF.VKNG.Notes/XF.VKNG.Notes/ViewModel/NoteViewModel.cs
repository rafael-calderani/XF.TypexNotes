using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

using XF.VKNG.Notes.Model;

namespace XF.VKNG.Notes.ViewModel {
    public class NoteViewModel : INotifyPropertyChanged {
        public Note Note { get; set; }


        #region UI Events

        public OnDetalheCMD OnDetalheCMD { get; }
        public OnMenuOptionsCMD OnMenuOptionsCMD { get; }

        public void GetDetalhe(Note paramNote) {
            if (paramNote != null) {
                App.Current.MainPage.Navigation.PushAsync(
                    new View.DetalheNoteView() { BindingContext = paramNote });
            }
        }

        #endregion

        public NoteViewModel() {
            this.OnDetalheCMD = new OnDetalheCMD(this);
            this.OnMenuOptionsCMD = new OnMenuOptionsCMD(this);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void EventPropertyChanged([CallerMemberName] string propertyName = null) {
            if (this.PropertyChanged != null) {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

    public class OnDetalheCMD : ICommand {
        private NoteViewModel noteVM;
        public OnDetalheCMD(NoteViewModel paramVM) {
            noteVM = paramVM;
        }
        public event EventHandler CanExecuteChanged;
        public void DetalheCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        public bool CanExecute(object parameter) {
            if (parameter != null) return true;

            return false;
        }
        public void Execute(object parameter) {
            noteVM.GetDetalhe(parameter as Note);
        }
    }

    public class OnMenuOptionsCMD : ICommand {
        private NoteViewModel noteVM;
        public OnMenuOptionsCMD(NoteViewModel paramVM) {
            noteVM = paramVM;
        }
        public event EventHandler CanExecuteChanged;
        public void MenuOptionsCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        public bool CanExecute(object parameter) {
            if (parameter != null) return true;

            return false;
        }
        public void Execute(object parameter) {
            //TODO: Show Options Menu for the notes list
        }
    }
}
