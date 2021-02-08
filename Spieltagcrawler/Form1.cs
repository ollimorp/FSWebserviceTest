using Spieltagcrawler.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
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
                listBox1.Items.Add(d);
            }
        }

        static HttpClient client = new HttpClient();

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            int id = (int)listBox1.SelectedItem;
            string requestMsg = $"http://localhost:59675/api/MatchDay/{id}";

            //RunAsync(requestMsg, id).Wait();

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
