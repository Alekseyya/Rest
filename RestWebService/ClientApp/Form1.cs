using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DomainModel;
using Newtonsoft.Json;

namespace ClientApp
{
    public partial class Form1 : Form
    {
        string URI = "http://localhost:57318/api/values";
        public Form1()
        {
            InitializeComponent();
            GetAllMails();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private async void GetAllMails()
        {
            using (var client = new HttpClient())
            {
                using (var response = await client.GetAsync(URI))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var mailJsonString = await response.Content.ReadAsStringAsync();

                        dataGridView1.DataSource = JsonConvert.DeserializeObject<Mail[]>(mailJsonString).ToList();

                    }
                }
            }
        }

        

        private void btnSendMail_Click(object sender, EventArgs e)
        {
            AddMails();
        }
        private async void AddMails()
        {
            Mail mail = new Mail();
            mail.MessageHeader = HeaderTextBox.Text;
            mail.Sender = SenderTextBox.Text;
            mail.Destination = DestinationTextBox.Text;
            mail.Content = contentTextBox.Text;
            mail.DataT = Convert.ToDateTime(DateTextBox.Text);

            using (var client = new HttpClient())
            {
                var serializedMail = JsonConvert.SerializeObject(mail);
                var content = new StringContent(serializedMail, Encoding.UTF8, "application/json");
                var result = await client.PostAsync(URI, content);
            }
            GetAllMails();
        }
        //private void label1_Click(object sender, EventArgs e)
        //{

        //}
    }
}
