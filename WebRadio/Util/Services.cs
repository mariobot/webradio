using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace WebRadio.Util
{
    public class Services
    {
        /// <summary>
        /// No usa autenticacion 
        /// Se usa para invocar web services solo con la direccion
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string GET_V1(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            try
            {
                WebResponse response = request.GetResponse();
                using (Stream responseStream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, System.Text.Encoding.UTF8);
                    return reader.ReadToEnd();
                }
            }
            catch (WebException ex)
            {
                WebResponse errorResponse = ex.Response;
                using (Stream responseStream = errorResponse.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, System.Text.Encoding.GetEncoding("utf-8"));
                    String errorText = reader.ReadToEnd();                    
                }
                throw;
            }
        }

        /// <summary>
        /// Usa autenticacion para acceder a los servicios completos
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string GET_V2(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "text/xml";
            request.Headers.Add("", "");
            try
            {
                WebResponse response = request.GetResponse();
                using (Stream responseStream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, System.Text.Encoding.UTF8);
                    return reader.ReadToEnd();
                }
            }
            catch (WebException ex)
            {
                WebResponse errorResponse = ex.Response;
                using (Stream responseStream = errorResponse.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, System.Text.Encoding.GetEncoding("utf-8"));
                    String errorText = reader.ReadToEnd();
                    // log errorText
                }
                throw;
            }
        }

        //private static List<T> CastDataToList<T>(string result) //where T : System.IComparable<T>
        //{
        //    JObject resultObject = JObject.Parse(result);

        //    // get JSON result objects into a list
        //    List<JToken> results = resultObject["data"].Children().ToList();

        //    // serialize JSON results into .NET objects
        //    List<T> ListaEntidades = new List<T>();
        //    foreach (JToken item in results)
        //    {
        //        // JToken.ToObject is a helper method that uses JsonSerializer internally
        //        T Entidad = item.ToObject<T>();
        //        ListaEntidades.Add(Entidad);
        //    }

        //    return ListaEntidades;
        //}
    }
}