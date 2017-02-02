using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WinClient.connectLib;

namespace WinClient
{
    public partial class Form1 : Form
    {
        private string baseAddress = "http://localhost:8080/jerseyServer/app/Service1";

        private DataTable results = null;

        public Form1()
        {
            InitializeComponent();
            this.ServerPath.Text = baseAddress;
        }

        /// <summary>
        /// GetリクエストでJSONを取得する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            var url = string.Format("{0}/SendData", this.ServerPath.Text);
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

            var url = string.Format("{0}/GetDataCount", this.ServerPath.Text);
            var result = HttpConnectLib.Post<Hashtable>(url, results);

            this.label1.Text = "";
            if (result.Contains("count"))
            {
                this.label1.Text = result["count"].ToString();
            }
        }
    }
}
