using System;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Runtime.InteropServices;

namespace bobclient
{
    public partial class Form1 : Form
    {
        [DllImport("WinDll.dll")]
        public static extern int AddInt(int a, int b);
        
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string strtitle = txtTitle.Text;
            string strcontent = txtContent.Text;

            SendData(strtitle, strcontent);

        }

        public bool SendData(string title, string content)
        {
       
            WebRequest request = WebRequest.Create("http://127.0.0.1:8080/api/saveproc");
            request.Method = "POST";

            string postData = "title=" + title + "&" + "content=" + content;
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = byteArray.Length;

            Stream dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();

            WebResponse response = request.GetResponse();

            Console.WriteLine(((HttpWebResponse)response).StatusDescription);

            using (dataStream = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(dataStream);
                string responseFromServer = reader.ReadToEnd();
                Console.WriteLine(responseFromServer);
            }

            response.Close();

            return false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int sum = AddInt(5, 10);
            string strsum = "더하기 값: " +  sum.ToString();
            
            MessageBox.Show(strsum);

        }
    }
}
