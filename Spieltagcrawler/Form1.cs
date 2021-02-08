using Newtonsoft.Json;
using Spieltagcrawler.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Spieltagcrawler
{
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();

            

            for (int d = 0; d < 35; d++)
            {
                if (d == 0)
                    continue;
                comboBox1.Items.Add(d);
            }
        }

        private void Model_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName == "FileContent")
            {
                PrintJson(Model.FileContent);

                var matches = JsonConvert.DeserializeObject<List<Match>>(Model.FileContent);
                listBox1.Items.Clear();
                listBox1.Items.Add($"Spieltag: {matches[0].Group.GroupName}");
                matches.ForEach((m) => listBox1.Items.Add($"{m.Team1.ShortName}) {m.Team1.TeamName} : ({m.Team2.ShortName}) " +
                    $"{m.Team2.TeamName} {m.MatchResults[1].PointsTeam1}:{m.MatchResults[1].PointsTeam2}"));
            }
        }

        static HttpClient client = new HttpClient();

        private async void toolStripButton1_Click(object sender, EventArgs e)
        {
            
            int id = (int)comboBox1.SelectedItem;

            webBrowser1.Navigate($"http://localhost:5000/api/MatchDay/{id}");
            Model.GetAll($"http://localhost:5000/api/MatchDay/{id}");
        }

        //async Task<string> GetAll(string requeststr)
        //{            
        //    toolStripProgressBar1.Enabled = true;

        //    //return await Model.GetAll(requeststr);

        //    toolStripProgressBar1.Enabled = false;
        //}

        public AppModel Model { get; set; }
        public int SelectedMatchday { get { return (int)comboBox1.SelectedItem; } }
        
        public async void PrintMatches(List<Match> matches)
        {
            matches.ForEach((m) => listBox1.Items.Add($"{m.Group.GroupName} - {m.Team1.ShortName}) {m.Team1.TeamName} : ({m.Team2.ShortName}) {m.Team2.TeamName}"));
        }

        public void PrintJson(string content)
        {
            textBox1.Text = content;
            //match.ForEach((m) => listBox1.Items.Add($"{m.Group.GroupName} - {m.Team1.ShortName}) {m.Team1.TeamName} : ({m.Team2.ShortName}) {m.Team2.TeamName}"));
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Model.PropertyChanged += Model_PropertyChanged;
        }

        private void webBrowser1_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {

        }

        //static async Task RunAsync(string reqstring, int matchday)
        //{
        //    client.BaseAddress = new Uri(reqstring);
        //    client.DefaultRequestHeaders.Accept.Clear();
        //    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        //    //do
        //    //{
        //    try
        //    {
        //        var matches = await GetMatchesAsync(reqstring);

        //        foreach (var match in matches)
        //        {
        //            Console.WriteLine(string.Concat(match.MatchDateTime, ": (", match.Team1.ShortName, ") ", match.Team1.TeamName, " : (", match.Team1.ShortName, ") ", match.Team2.TeamName));
        //        }

        //        System.Threading.Thread.Sleep(200);
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex);
        //        System.Threading.Thread.Sleep(200);

        //    }

        //    //} while (true);


        //    Console.ReadLine();
        //}


        //async Task<List<Match>> GetMatchesAsync(string path)
        //{
        //    List<Match> matches = null;
        //    HttpResponseMessage response = await client.GetAsync(path);
        //    if (response.IsSuccessStatusCode)
        //    {
        //        matches = await response.Content.ReadAsAsync<List<Match>>();
        //    }
        //    return matches;
        //}
    }
}
