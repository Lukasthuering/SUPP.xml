using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.IO;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Forms;
using System.Xml;
using Supp_xml.Models;
using Supp_xml.ViewModels;

namespace Supp_xml.ViewModels
{
    public class SQL
    {

        public static List<DataModel> SqlConnection(DateTime startDate, DateTime endDate, string ServerName)
        {
            int difference = endDate.Day - startDate.Day;
            List<DataModel> daten = new List<DataModel>();
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = "Server=.\\" + ServerName + "; database=LogDebug; integrated security=sspi";
            SqlCommand cmd = new SqlCommand(" SELECT[DatumZeit] ,[Text] ,[Terminal] ,[SeqNummer]  FROM [LogDebug].[dbo].[Log_" + startDate.ToString("MM") + "_" + startDate.ToString("dd") + "] WHERE Devicename = 165 AND DatumZeit in   (SELECT DatumZeit FROM[Log_" + startDate.ToString("MM") + "_" + startDate.ToString("dd") + "] WHERE Text LIKE '%dPreAuthorisation%'   OR Text LIKE '%´Proceed´%' OR Text LIKE '%dPayment%'    OR Text LIKE '%dFinancialAdvice%' AND TEXT LIKE '%<%' AND TEXT LIKE '%>%')	ORDER BY Terminal, SeqNummer", conn);

            using (conn)
            {
                conn.Open();
                //MessageBox.Show("Connected");
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    daten.Add(new DataModel()
                    {

                        DatumZeit = reader.GetDateTime(0),
                        Text = reader.GetString(1)

                    });

                }

                conn.Close();
                cmd.Dispose();


                foreach (var i in daten)
                {
                    i.Text = i.Text.Replace("\0", string.Empty);
                    i.Text = i.Text.Replace("~", string.Empty);
                    i.Text = i.Text.Replace("\t", string.Empty);
                    i.Text = i.Text.Replace("´", "'");
                    i.Text = i.Text.Replace(Environment.NewLine, "");

                    
                      Regex FirstRegexRemove = new Regex(@"([\d]){ 2}:([\d]){ 2}:([\d]){ 2}TSM[05]\d: Appl: ");
                      Regex SecondRegexRemove = new Regex(@"\.\.\.");
                      Regex ThirdRegexRemove = new Regex(@"\r\n~");
                      Match m = FirstRegexRemove.Match(i.Text);
                      Match a = SecondRegexRemove.Match(i.Text);
                    Match c = ThirdRegexRemove.Match(i.Text);
                    i.Text = i.Text.Replace(
                        @"SendMessageToPT: XMLString=<\?xml version = ´1.0´ encoding = ´UTF-8´ standalone = ´yes´\?>",
                        @"SendMessageToPT: XMLString=\n<?xml version = ´1.0´ encoding = ´UTF-8´ standalone = ´yes´?>\n");
                    i.Text = i.Text.Replace(
                        @"ReceiveMessageFromPT: XMLString=<CardServiceResponse",
                        @"ReceiveMessageFromPT: XMLString=\n<CardServiceResponse "
                    );




                    //i.Text.Remove(0,1);
                }

                return daten;
            }




            /* StreamWriter regexFormater(StreamWriter text)
             {
                 Regex firstCommand = new Regex(@"([\d]){2}:([\d]){2}:([\d]){2} TSM[05]\d: Appl: ");
                 Regex secondCommand = new Regex(@"\.\.\.");
                 foreach (Match m in Regex.Matches(text, firstCommand))
                 {

                 }

                 return text;
             }
             string FormatXml(string inputXml)
             {
                 XmlDocument document = new XmlDocument();
                 StringReader a = new StringReader(inputXml);
                 document.LoadXml(a.ToString());

                 StringBuilder builder = new StringBuilder();
                 using (XmlTextWriter writer = new XmlTextWriter(new StringWriter(builder)))
                 {
                     writer.Formatting = Formatting.Indented;
                     document.Save(writer);
                 }

                 return builder.ToString();
             }
             string PrintXML(string xml)
             {
                 string result = "";

                 MemoryStream mStream = new MemoryStream();
                 XmlTextWriter schreiber = new XmlTextWriter(mStream, Encoding.Unicode);
                 XmlDocument document = new XmlDocument();


                 // Load the XmlDocument with the XML.
                 document.LoadXml(xml);

                 schreiber.Formatting = Formatting.Indented;

                 // Write the XML into a formatting XmlTextWriter
                 document.WriteContentTo(schreiber);
                 schreiber.Flush();
                 mStream.Flush();

                 // Have to rewind the MemoryStream in order to read
                 // its contents.
                 mStream.Position = 0;

                 // Read MemoryStream contents into a StreamReader.
                 StreamReader sReader = new StreamReader(mStream);

                 // Extract the text from the StreamReader.
                 string formattedXml = sReader.ReadToEnd();

                 result = formattedXml;



                 schreiber.Close();
                 mStream.Close();


                 return result;

             }
             string xmlFormatter(string xml)
             {
                 // Format the XML text.
                 StringWriter string_writer = new StringWriter();
                 XmlTextWriter xml_text_writer = new XmlTextWriter(string_writer);
                 xml_text_writer.Formatting = Formatting.Indented;
                 xml_text_writer.WriteString(xml);

                 return string_writer.ToString();
             }*/
        }



    }
}
