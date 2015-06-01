using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using Newtonsoft.Json;
namespace PersonalityInsights
{
    public class PersonalityInsights
    {

        // If running locally complete the variables below with the information in
        // VCAP_SERVICES
        private static String baseURL = "https://gateway.watsonplatform.net/personality-insights/api/v2/profile";
        private static String username = "dd7675e6-86fd-4750-9c9a-38ad8b61a8e6";
        private static String password = "t1fP0NeR44cu";

        public static void Main(String[] args)
        { doStuff(); }
 
	 static void doStuff() {
 
		Console.Write("starting");
        string text = System.IO.File.ReadAllText("/bio.txt");
        StringBuilder uri = new StringBuilder();
        uri.Append(baseURL).Append('/').Append("&outputMode=xml");
		HttpWebRequest profileRequest = (HttpWebRequest)WebRequest.Create(baseURL);
         string _auth = string.Format("{0}:{1}",username,password);
        string _enc = Convert.ToBase64String(Encoding.ASCII.GetBytes(_auth));
        string _cred = string.Format("{0} {1}", "Basic", _enc);
         profileRequest.Headers[HttpRequestHeader.Authorization] = _cred;
         profileRequest.Accept="application/json";
         profileRequest.ContentType = "text/plain";
         byte[] bytes = Encoding.UTF8.GetBytes(text);
         profileRequest.Method = "POST";
         profileRequest.ContentLength = bytes.Length;
             using (Stream requeststream = profileRequest.GetRequestStream())
             {
                 requeststream.Write(bytes, 0, bytes.Length);
                 requeststream.Close();
             }
             string response;
             System.IO.StreamWriter file = new System.IO.StreamWriter("C:/Users/Achal Shantharam/Documents/Visual Studio 2010/Projects/ConsoleApplication4/Output.json");
  
         using (HttpWebResponse webResponse = (HttpWebResponse)profileRequest.GetResponse())
         {
             using (StreamReader sr = new StreamReader(webResponse.GetResponseStream()))
             {
                 response = sr.ReadToEnd();//.Trim();
                 dynamic formatted = JsonConvert.DeserializeObject(response);
                 JsonConvert.SerializeObject(formatted, Formatting.Indented);
                 file.Write(formatted);
                 Console.Write(formatted);
                // System.IO.File.WriteAllText("C:/Users/Achal Shantharam/Documents/Visual Studio 2010/Projects/ConsoleApplication4/Output.json", formatted);
                 sr.Close();
             }
             webResponse.Close();
         }
         //System.IO.StreamWriter file = new System.IO.StreamWriter("C:/Users/Achal Shantharam/Documents/Visual Studio 2010/Projects/ConsoleApplication4/Output.json");
     //    dynamic formatted = JsonConvert.DeserializeObject(response);
     //    JsonConvert.SerializeObject(formatted,Formatting.Indented);
      //   System.IO.File.WriteAllText("C:/Users/Achal Shantharam/Documents/Visual Studio 2010/Projects/ConsoleApplication4/Output.json", formatted);
    //     Console.Write(formatted);
       //  file.Write(formatted);
         Console.WriteLine("done");
         Console.Read();
	}
 }
}



