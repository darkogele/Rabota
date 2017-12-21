using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace AKNDesktopApplication
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void callBtn_Click(object sender, EventArgs e)
        {
            try
            {
                var client = new FPIOMServiceReference.FPIOMServiceClient();
                var response = client.GetDataForRetired(embgTxt.Text);
                var stringReader = new StringReader(response);
                var serializer = new XmlSerializer(typeof(DataForRetiredDTO));
                var retiredDto = serializer.Deserialize(stringReader) as DataForRetiredDTO;
                nameTxt.Text = retiredDto.NameSurname;
                numberTxt.Text = retiredDto.PensionNumber;
                amountTxt.Text = retiredDto.PensionAmount;
                statusTxt.Text = retiredDto.PensionStatus;
                if (nameTxt.Text != "" && numberTxt.Text != "" && amountTxt.Text != "" && statusTxt.Text != "")
                {
                    errorTxt.Text = "Нема грешка";
                }
                else
                {
                    errorTxt.Text = "Не е пронајден пензионер со ЕМБГ: " + embgTxt.Text;
                }
                groupBox.Visible = true;
            }
            catch (Exception ex)
            {
                errorTxt.Text = "Message :{0} " + ex.Message;
                groupBox.Visible = true;
            }
        }

    }
}
