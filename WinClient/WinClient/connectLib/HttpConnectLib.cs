using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace WinClient.connectLib
{
    public class HttpConnectLib
    {
        /// <summary>
        /// Getメソッド
        /// </summary>
        /// <param name="url">クエリ付きURL</param>
        /// <returns>レスポンス</returns>
        public static string Get(string url)
        {
            string result = string.Empty;
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            WebResponse res = null;
            Stream resStream = null;
            StreamReader sr = null;

            try
            {
                // レスポンスの取得と読み込み
                res = req.GetResponse();
                resStream = res.GetResponseStream();
                sr = new StreamReader(resStream, Encoding.UTF8);
                result = sr.ReadToEnd();
            }
            finally {
                sr.Close();
                resStream.Close();
            }

            return result;
        }

        /// <summary>
        /// POSTメソッド
        /// </summary>
        /// <param name="url">URL</param>
        /// <param name="jsonParam">JSONパラメータ</param>
        /// <returns>レスポンス</returns>
        public static string Post(string url, string jsonParam)
        {
            string result = string.Empty;
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            Stream paramStream = null;
            WebResponse res = null;
            Stream resStream = null;
            StreamReader sr = null;

            try
            {
                req.Method = "POST";
                req.ContentType = "application/json";

                var data = Encoding.ASCII.GetBytes(jsonParam);
                paramStream = req.GetRequestStream();
                paramStream.Write(data, 0, data.Length);
                paramStream.Close();
                paramStream = null;

                /*
                paramStream = req.GetRequestStream();
                paramWriter = new StreamWriter(paramStream, Encoding.UTF8);

                paramWriter.WriteLine(jsonParam);
                */

                // レスポンスの取得と読み込み
                res = req.GetResponse();
                resStream = res.GetResponseStream();
                sr = new StreamReader(resStream, Encoding.UTF8);
                result = sr.ReadToEnd();
            }
            finally
            {
                if(paramStream!=null) paramStream.Close();
                if (sr != null) sr.Close();
                if (resStream != null) resStream.Close();
            }

            return result;
        }

    }
}
