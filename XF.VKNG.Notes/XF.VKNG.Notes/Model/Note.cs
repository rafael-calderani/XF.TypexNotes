using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using XF.VKNG.Notes.ViewModel;

namespace XF.VKNG.Notes.Model {

    public interface INote {
        void GetNotes(NoteViewModel vm);
    }
    public class Note {
        private static string apiURL = App.typexServiceURL + "/api/Notes";

        //[PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        //[MaxLength(200)] 
        public string Titulo { get; set; }

        public string Detalhes { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public string Senha { get; set; }

        public DateTime CriadoEm { get; set; }

        public Guid UserId { get; set; }

        public static async Task<List<Note>> List() {
            List<Note> noteList = new List<Note>();

            using (HttpClient httpClient = new HttpClient()) {
                var response = await httpClient.GetAsync(apiURL);
                var json = await response.Content.ReadAsStringAsync();

                noteList = JsonConvert.DeserializeObject<List<Note>>(json);
            }

            return noteList;
        }

        public static bool IsValid(Note n) {
            return !string.IsNullOrEmpty(n.Titulo);
        }

        public static async Task<bool> Insert(Note n) {
            bool result = false;
            n.CriadoEm = DateTime.Now;

            using (HttpClient httpClient = new HttpClient()) {
                string noteJson = JsonConvert.SerializeObject(n);

                HttpResponseMessage response = await httpClient.PostAsync(apiURL, new StringContent(noteJson));

                result = response.IsSuccessStatusCode;
            }

            return result;
        }

        public static async Task<bool> Update(Note n) {
            bool result = false;
            // TODO: check this update method
            using (HttpClient httpClient = new HttpClient()) {
                string noteJson = JsonConvert.SerializeObject(n);
                HttpResponseMessage response = await httpClient.PostAsync(apiURL, new StringContent(noteJson));

                result = response.IsSuccessStatusCode;
            }

            return result;

        }

        public static async Task<bool> Save(Note n) {
            if (n.CriadoEm == null) return await Insert(n);

            return await Update(n);
        }

        public static async Task<bool> Delete(int noteId) {
            bool result = false;
            using (HttpClient httpClient = new HttpClient()) {
                HttpResponseMessage response = await httpClient.DeleteAsync(apiURL + "/" + noteId);

                result = response.IsSuccessStatusCode;
            }

            return result;
        }
    }
}