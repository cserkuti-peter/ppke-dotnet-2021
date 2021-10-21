using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TaskCombinatorExamples
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Random r = new Random();
        private async Task<Image> downloadImageAsync(string uri, CancellationToken cancellationToken)
        {
            //Thread.Sleep(r.Next(0, 5000));    DONT
            await Task.Delay(r.Next(0, 5000));
            using (var client = new HttpClient())
            {
                using (var response = await client.GetAsync(uri, cancellationToken))
                {
                    response.EnsureSuccessStatusCode();

                    using (var stream = await response.Content.ReadAsStreamAsync())
                    {
                        return Image.FromStream(stream);
                    }
                }
            }
        }

        private async Task<Image> downloadImageWithTimeoutAsync(string uri, CancellationToken cancellationToken)
        {
            var t = downloadImageAsync(uri, cancellationToken);
            if (t == await Task.WhenAny(t, Task.Delay(3000)))
            {
                return await t;
            }

            throw new Exception("Timeout");
        }

        int count = 0;
        private void addPicutreBox(Image image)
        {
            var pb = new PictureBox
            {
                Size = new Size(100, 100),
                Location = new Point((count % 10) * 100, (count / 10) * 100),
                Image = image
            };
            this.Controls.Add(pb);
            count++;
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            //  1. First check
            //var image = await downloadImageAsync("https://picsum.photos/300/300");
            //addPicutreBox(image);

            var cts = new CancellationTokenSource();
            //var tasks = Enumerable.Range(1, 100).Select(i => downloadImageAsync("https://picsum.photos/300/300", cts.Token)).ToList();
            var tasks = Enumerable.Range(1, 100).Select(i => downloadImageWithTimeoutAsync("https://picsum.photos/300/300", cts.Token)).ToList();

            //  2. WhenAll
            //try
            //{
            //    var images = await Task.WhenAll(tasks);
            //    foreach (var image in images)
            //    {
            //        addPicutreBox(image);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    foreach (var t in tasks.Where(t => t.IsFaulted))
            //    { 
            //        //  Log exception for the task 
            //    }
            //}

            //  3. WhenAny (redundancy, interleaving, throttling, early bailout/timeout)
            while (tasks.Count > 0)
            {
                Task<Image> t = null;
                try
                {
                    t = await Task.WhenAny(tasks);
                    //cts.Cancel();
                    var image = await t;
                    addPicutreBox(image);
                }
                catch (Exception ex)
                {
                    //  Log ex
                }
                finally
                {
                    tasks.Remove(t);
                }
            }
        }
    }
}
