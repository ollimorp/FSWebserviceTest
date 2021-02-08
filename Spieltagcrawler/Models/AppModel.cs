using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Spieltagcrawler.Models
{
    public class AppModel : INotifyPropertyChanged
    {
        public AppModel(Form1 form)
        {
            this.form = form;
        }

        Form1 form;

        static HttpClient client = new HttpClient();
       
        public string FileContent { get; private set; }

        public async Task<string> GetAllAsync(string requestString)
        {
            int id = form.SelectedMatchday;

            WebRequest req = WebRequest.Create(requestString);
            req.ContentType = "application/json";

            WebResponse response = req.GetResponse();

            using (var s = response.GetResponseStream())
            {
                using (var sr = new StreamReader(s))
                {
                    return await sr.ReadToEndAsync();                   
                }
            }
            response.Close();
        }

        public void GetAll(string requestString)
        {
            int id = form.SelectedMatchday;
            //string requestStr = 

            WebRequest req = WebRequest.Create(requestString);
            req.ContentType = "application/json";

            WebResponse response = req.GetResponse();

            using (var s = response.GetResponseStream())
            {
                using (var sr = new StreamReader(s))
                {
                    FileContent = sr.ReadToEnd();
                    FireEvent("FileContent");
                }
            }
            response.Close();
        }

        void FireEvent(string propname)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propname));
        }





        public event PropertyChangedEventHandler PropertyChanged;
    }
}
