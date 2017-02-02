using System;
using System.Collections;
using System.Data;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using WinClient.connectLib;

namespace WinClient
{
    public partial class Form1 : Form
    {
        private string webApiBaseAddress = "";

        private DataTable results = null;

        public Form1()
        {
            InitializeComponent();

            webApiBaseAddress = getWebApiBaseAddress()+ "jerseyServer/app/Service1";
        }

        private string getWebApiBaseAddress()
        {
            var hostIP = string.Empty;

            var appDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().Name);
            var fullPath = Path.Combine(appDir, "localhostIP.txt");

            using(var reader=new StreamReader(fullPath))
            {
                hostIP = reader.ReadLine();
            }

            if (string.IsNullOrEmpty(hostIP))
            {
                hostIP = "localhost";
            }

            return string.Format("http://{0}:8080/", hostIP.Trim());
        }

        /// <summary>
        /// GetリクエストでJSONを取得する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            var url = string.Format("{0}/SendData", webApiBaseAddress);
            results = HttpConnectLib.Get<DataTable>(url);

            var sb = new StringBuilder();
            foreach(DataRow result in results.Rows)
            {
                sb.AppendLine(string.Format("name={0} {1}", result["name"], result["age"]));
            }

            this.textBox1.Text = sb.ToString();
        }

        /// <summary>
        /// テキストボックスの値をPOSTリクエストで送信する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            if(this.textBox1.Text == string.Empty)
            {
                MessageBox.Show("1.serverから受信 を実行してください");
                return;
            }

            var url = string.Format("{0}/GetDataCount", webApiBaseAddress);
            var result = HttpConnectLib.Post<Hashtable>(url, results);

            this.label1.Text = "";
            if (result.Contains("count"))
            {
                this.label1.Text = result["count"].ToString();
            }
        }
    }
}
