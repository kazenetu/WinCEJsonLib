using Newtonsoft.Json;
using System.IO;
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
        public static T Get<T> (string url) where T : new()
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
            catch
            {
                return new T();
            }
            finally {
                if(sr != null) sr.Close();
                if (resStream != null) resStream.Close();
            }

            return JsonConvert.DeserializeObject<T>(result);
        }

        /// <summary>
        /// POSTメソッド
        /// </summary>
        /// <param name="url">URL</param>
        /// <param name="param">パラメータ</param>
        /// <returns>レスポンス</returns>
        public static T Post<T>(string url, object param) where T : new()
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
                req.AllowWriteStreamBuffering = true;

                var data = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(param));
                paramStream = req.GetRequestStream();
                paramStream.Write(data, 0, data.Length);
                paramStream.Close();
                paramStream = null;

                // レスポンスの取得と読み込み
                res = req.GetResponse();
                resStream = res.GetResponseStream();
                sr = new StreamReader(resStream, Encoding.UTF8);
                result = sr.ReadToEnd();
            }
            catch
            {
                return new T();
            }
            finally
            {
                if(paramStream!=null) paramStream.Close();
                if (sr != null) sr.Close();
                if (resStream != null) resStream.Close();
            }

            return JsonConvert.DeserializeObject<T>(result);
        }

    }
}
