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
using System.Net.Mail;

namespace ClientApp
{
    public partial class Form1 : Form
    {
        string URI = "http://localhost:57318/api/values";
        public Form1()
        {
            InitializeComponent();
            maskedTextBox1.Mask = "00/00/0000";
            
            maskedTextBox1.MaskInputRejected += new MaskInputRejectedEventHandler(maskedTextBox1_MaskInputRejected);
            
            maskedTextBox1.ValidatingType = typeof(System.DateTime);
            maskedTextBox1.TypeValidationCompleted += new TypeValidationEventHandler(maskedTextBox1_TypeValidationCompleted);

        }
        void maskedTextBox1_TypeValidationCompleted(object sender, TypeValidationEventArgs e)
        {
            if (!e.IsValidInput)
            {
                toolTip1.ToolTipTitle = "Invalid Date";
                toolTip1.Show("The data you supplied must be a valid date in the format mm/dd/yyyy.", maskedTextBox1, 0, -20, 5000);
            }
            else
            {
                
                DateTime userDate = (DateTime)e.ReturnValue;
               
                if (userDate < System.DateTime.Now)
                {
                    toolTip1.ToolTipTitle = "Invalid Date";
                    toolTip1.Show("The date in this field must be greater than today's date.", maskedTextBox1, 0, -20, 5000);
                    e.Cancel = true;
                }
            }
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
            bool DestinationFlag = IsValidEmail(DestinationTextBox.Text);
            bool SenderFlag = IsValidEmail(SenderTextBox.Text);
            mail.Sender = SenderTextBox.Text;
            mail.Destination = DestinationTextBox.Text;
            mail.Content = contentTextBox.Text;
            mail.DataT = Convert.ToDateTime(maskedTextBox1.Text);
            if (DestinationFlag & SenderFlag == true)
            {
                using (var client = new HttpClient())
                {
                    var serializedMail = JsonConvert.SerializeObject(mail);
                    var content = new StringContent(serializedMail, Encoding.UTF8, "application/json");
                    var result = await client.PostAsync(URI, content);
                }
            }
            else
            {
                if (DestinationFlag == false)
                {
                    toolTip1.Show("Неправильно введен адрес получателя.", DestinationTextBox, 0, -20, 5000);
                }
                else
                {
                    toolTip1.Show("Неправильно введен адрес адресата.", SenderTextBox, 0, -20, 5000);
                }
            }         
            
            GetAllMails();
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var mail = new System.Net.Mail.MailAddress(email);
                return true;
            }
            catch
            {
                return false;
            }
        }
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            GetAllMails();
        }

        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {
            if (maskedTextBox1.MaskFull)
            {
                toolTip1.ToolTipTitle = "Input Rejected - Too Much Data";
                toolTip1.Show("You cannot enter any more data into the date field. Delete some characters in order to insert more data.", maskedTextBox1, 0, -20, 5000);
            }
            else if (e.Position == maskedTextBox1.Mask.Length)
            {
                toolTip1.ToolTipTitle = "Input Rejected - End of Field";
                toolTip1.Show("You cannot add extra characters to the end of this date field.", maskedTextBox1, 0, -20, 5000);
            }
            else
            {
                toolTip1.ToolTipTitle = "Input Rejected";
                toolTip1.Show("You can only add numeric characters (0-9) into this date field.", maskedTextBox1, 0, -20, 5000);
            }
            
        }

        
        
    }
}
