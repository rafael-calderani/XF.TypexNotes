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
    public class NoteViewModel {
        private static string apiURL = "http://vkngnotes.azurewebsites.net/api/Notes";

        public static async Task<List<Note>> List() {
            List<Note> noteList = new List<Note>();

            using (HttpClient httpClient = new HttpClient()) {
                using (var response = await httpClient.GetAsync(apiURL, HttpCompletionOption.ResponseContentRead)) {
                    var json = await response.Content.ReadAsStringAsync();

                    noteList = JsonConvert.DeserializeObject<List<Note>>(json);
                    noteList.RemoveAll(n => n.UserId != UsuarioViewModel.Atual.Id);
                }
            }

            return noteList;
        }

        public static bool IsValid(Note n) {
            return !string.IsNullOrEmpty(n.Titulo);
        }

        public static async Task<bool> Insert(Note n) {
            bool result = false;
            n.UserId = UsuarioViewModel.Atual.Id;

            using (HttpClient httpClient = new HttpClient()) {
                string noteJson = JsonConvert.SerializeObject(n);

                HttpResponseMessage response = await httpClient.PostAsync(apiURL, new StringContent(noteJson, Encoding.UTF8, "application/json"));

                result = response.IsSuccessStatusCode;
            }

            return result;
        }

        public static async Task<bool> Update(Note n) {
            bool result = false;

            using (HttpClient httpClient = new HttpClient()) {
                string noteJson = JsonConvert.SerializeObject(n);
                HttpResponseMessage response = await httpClient.PostAsync(string.Concat(apiURL, "/", n.Id),
                    new StringContent(noteJson, Encoding.UTF8, "application/json"));

                result = response.IsSuccessStatusCode;
            }

            return result;

        }

        public static async Task<bool> Save(Note n) {
            if (n.Id > 0) return await Update(n);

            return await Insert(n);
        }

        public static async Task<bool> Delete(int noteId) {
            bool result = false;
            using (HttpClient httpClient = new HttpClient()) {
                HttpResponseMessage response = await httpClient.DeleteAsync(string.Concat(apiURL, "/", noteId));

                result = response.IsSuccessStatusCode;
            }

            return result;
        }

        public static async Task<bool> DeleteByUser(int userId) {
            bool result = false;
            List<Note> noteList = new List<Note>();

            using (HttpClient httpClient = new HttpClient()) {
                var response = await httpClient.GetAsync(apiURL, HttpCompletionOption.ResponseContentRead);
                var json = await response.Content.ReadAsStringAsync();

                noteList = JsonConvert.DeserializeObject<List<Note>>(json);
                foreach (Note n in noteList) {
                    if (n.UserId == userId) result = await Delete(n.Id);
                }
            }

            return result;
        }
    }
}
