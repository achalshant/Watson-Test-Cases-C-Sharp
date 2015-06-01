using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Web;
using System.Net;
using System.IO;

namespace QandA
{
    public class QuestionAndAns
    {
        private static String baseURL = "https://gateway.watsonplatform.net/question-and-answer-beta/api/v1/question/travel";
        private static String username = "a6b896cd-7edc-4286-8e6a-4e707059d438";
        private static String password = "pNiSiFBOnsS0";

        public static void Main(String[] args)
        {
            doStuff();
        }

        static void doStuff()
        {
            Console.Write("starting");

            string question = "What to see in NYC?";
            string dataset = "travel";

            //create the { 'question' : {
            //	'questionText:'...',		
            //  'evidenceRequest': { 'items': 5} } json as requested by the service
            JObject questionJson = new JObject();
            questionJson.Add("questionText", question);
            JObject evidenceRequest = new JObject();
            evidenceRequest.Add("items", 1);
            questionJson.Add("evidenceRequest", evidenceRequest);
            JObject postData = new JObject();
            postData.Add("question", questionJson);
            Console.Write(postData.ToString());

            string text = postData.ToString();

            HttpWebRequest profileRequest = (HttpWebRequest)WebRequest.Create(baseURL);
            string _auth = string.Format("{0}:{1}", username, password);
            string _enc = Convert.ToBase64String(Encoding.ASCII.GetBytes(_auth));
            string _cred = string.Format("{0} {1}", "Basic", _enc);
            profileRequest.Headers[HttpRequestHeader.Authorization] = _cred;
            profileRequest.Accept = "application/json";
            profileRequest.ContentType = "application/json";
            byte[] bytes = Encoding.UTF8.GetBytes(text);
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
                    response = sr.ReadToEnd();//.Trim();
                  
                  //  file.Write(formatted);
                  //  Console.Write(formatted);
                    // System.IO.File.WriteAllText("C:/Users/Achal Shantharam/Documents/Visual Studio 2010/Projects/ConsoleApplication4/Output.json", formatted);
                    sr.Close();
                }
                webResponse.Close();
            }
            dynamic formatted = JsonConvert.DeserializeObject(response);
            JsonConvert.SerializeObject(formatted, Formatting.Indented);
            Console.Write(formatted);
            Console.Write("Done");
            Console.Read();
        }
    }
}
