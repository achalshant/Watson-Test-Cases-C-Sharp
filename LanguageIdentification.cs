using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using System.Web;

namespace LanguageIdentification
{
    public class LanguageIdentification
    {
        private static String baseURL = "https://gateway.watsonplatform.net/language-identification-beta/api";
        private static String username = "f5af42d7-8c24-4d81-8365-1ace98adb55d";
        private static String password = "LUWlYpNOKtYG";

        public static void Main(String[] args)
        {
            doStuff();
        }

        static void doStuff()
        {
            Console.Write("starting");
            String text = "Hey! What's up?";
            Console.Write("\nInput text:" + text);
            String sid = "lid-generic";



            var qparams = new List<KeyValuePair<string, string>>() {
            
             new KeyValuePair<string, string>("txt", text),
            
             new KeyValuePair<string, string>("sid", sid),

             new KeyValuePair<string, string>("rt", "text"),
           
            };

            StringBuilder postString = new StringBuilder();
            bool first = true;
            foreach (KeyValuePair<string, string> pair in qparams)
            {
                if (first)
                    first = false;
                else
                    postString.Append("&");
                postString.AppendFormat("{0}={1}", pair.Key, System.Web.HttpUtility.UrlEncode(pair.Value));
            }
            HttpWebRequest profileRequest = (HttpWebRequest)WebRequest.Create(baseURL);
            string _auth = string.Format("{0}:{1}", username, password);
            string _enc = Convert.ToBase64String(Encoding.ASCII.GetBytes(_auth));
            string _cred = string.Format("{0} {1}", "Basic", _enc);
            profileRequest.Headers[HttpRequestHeader.Authorization] = _cred;
            profileRequest.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
            profileRequest.ContentType = "application/x-www-form-urlencoded";
            byte[] bytes = Encoding.UTF8.GetBytes(postString.ToString());
            profileRequest.Method = "POST";
            profileRequest.ContentLength = bytes.Length;
            using (Stream requeststream = profileRequest.GetRequestStream())
            {
                requeststream.Write(bytes, 0, bytes.Length);
                requeststream.Close();
            }
            string response;
            using (HttpWebResponse webResponse = (HttpWebResponse)profileRequest.GetResponse())
            {
                using (StreamReader sr = new StreamReader(webResponse.GetResponseStream()))
                {
                    response = sr.ReadToEnd().Trim();
                    sr.Close();
                }
                webResponse.Close();
            }

            Console.Write("\nLanguage : "+response);
            Console.Read();          
        }
    }
}

