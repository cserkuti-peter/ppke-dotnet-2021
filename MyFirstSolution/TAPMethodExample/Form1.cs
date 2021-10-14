using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TAPMethodExample
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //  TAP method
        public Task<string> CalculateAsync(string a, string b, CancellationToken cancellationToken)
        {
            return Task<string>.Run(() =>
            {
                //throw new Exception("test");

                for (int i = 0; i < 5; i++)
                {
                    Thread.Sleep(1000); //  Compute-bound operation
                    cancellationToken.ThrowIfCancellationRequested();
                }
                return a + b;
            });
        }

        public Task<string> Calculate2Async(string a, string b)
        {
            var tcs = new TaskCompletionSource<string>();       //  IO-bound operation
            System.Threading.Timer timer = null;

            timer = new System.Threading.Timer(state =>
            {
                timer.Dispose();
                tcs.TrySetResult(a + b);
            }, null, 2000, Timeout.Infinite);

            return tcs.Task;
        }

        CancellationTokenSource cts = null;

        private async void bDo_Click(object sender, EventArgs e)
        {
            //  1. Using threadpool threads
            //ThreadPool.QueueUserWorkItem(state =>
            //{
            //    var result = Calculate(tbA.Text, tbB.Text);

            //    this.Invoke(new Action(() =>
            //    {
            //        tbResult.Text = result;
            //    }));
            //});

            //  2. Task completion callback
            //var task = CalculateAsync(tbA.Text, tbB.Text);
            //task.ContinueWith(t => tbResult.Text = t.Result,
            //    TaskScheduler.FromCurrentSynchronizationContext());

            //  3. async/await
            cts = new CancellationTokenSource();
            try
            {
                var result = await CalculateAsync(tbA.Text, tbB.Text, cts.Token);//.ConfigureAwait(true);
                tbResult.Text = result;
            }
            catch (OperationCanceledException ex)
            {
                tbResult.Text = "Operation cancelled";
            }
            catch (Exception ex)
            {
                tbResult.Text = ex.Message;
            }
        }

        private void bCancel_Click(object sender, EventArgs e)
        {
            cts.Cancel();
        }
    }
}
