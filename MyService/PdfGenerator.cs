using MigraDoc.DocumentObjectModel;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace MyService
{
    internal class PdfGenerator
    {
        public PdfGenerator()
        {

        }

        public void GetResponse(string url)
        {
            // Create a request for the URL.
            WebRequest request = WebRequest.Create(url);
            // If required by the server, set the credentials.
            request.Credentials = CredentialCache.DefaultCredentials;
            // Get the response.
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            // Get the stream containing content returned by the server.
            Stream dataStream = response.GetResponseStream();
            // Open the stream using a StreamReader for easy access.
            StreamReader reader = new StreamReader(dataStream);
            // Read the content.
            string responseFromServer = reader.ReadToEnd();
            reader.Close();
            dataStream.Close();
            response.Close();

            MemoryStream stream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(responseFromServer));

            // Load HTML file
            PdfDocument document = new PdfDocument();

            PdfPage page = document.AddPage();

            XFont font = new XFont("Verdana", 20, XFontStyle.BoldItalic);

            XGraphics gfx = XGraphics.FromPdfPage(page);

            gfx.DrawString("Hello, World!", font, XBrushes.Black, new XRect(0, 0, page.Width, page.Height), XStringFormats.Center);


            string filename = "C:/Users/pec/Documents/My Received Files/HelloWorld.pdf";

            document.Save(filename);

            Process.Start(filename);

        }
    }
}