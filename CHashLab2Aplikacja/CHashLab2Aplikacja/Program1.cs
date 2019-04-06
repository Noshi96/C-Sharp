using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Threading;
using System.Diagnostics;
using System.Collections;
using CHashLab1;

//localhost:8000/index.html
namespace CHashLab2
{
    class HTTPServer
    {
        private HttpListener listener;
        private string path;
        int port;
        private Thread thread;

        public HTTPServer(string path, int port)
        {
            this.path = path;
            this.port = port;
            thread = new Thread(this.listen);
            thread.Start();
        }
        private void listen()
        {                 
            try
            {
                listener = new HttpListener();
                listener.Prefixes.Add("http://localhost:"+port+"/");
                listener.Start();

                while (true)
                {
                    Console.WriteLine("Listening..."); 
                    HttpListenerContext context = listener.GetContext();
                    exist(context,path);
                }
            }
            catch (WebException e)
            {
                Console.WriteLine(e.Status);
            }          
        }
        public void stop()
        {
            thread.Abort();
            listener.Stop();
        }
        private void exist(HttpListenerContext context,string path)
        {
            HttpListenerResponse response = context.Response;
            HttpListenerRequest request = context.Request;
            string responseString="";
            string fileName = context.Request.Url.AbsolutePath;
            fileName = fileName.Substring(1);
            fileName = Path.Combine(path, fileName);
            string[] fileList = Directory.GetFiles(path);

            if (File.Exists(fileName))
            {
                try
                {
                    foreach (string file_name in fileList)
                    {
                        if (file_name == fileName)
                        {
                            Console.WriteLine(file_name);
                            responseString = File.ReadAllText(fileName);
                            responseString += status(request); 
                            contentView(responseString, response);                           
                        }
                    }
                    context.Response.StatusCode = (int)HttpStatusCode.OK;
                }
                catch (Exception ex)
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    responseString = "500";
                    contentView(responseString, response);
                    Console.WriteLine(context.Response.StatusCode);
                }
            }
            else if(fileName == path)
            {
                Random rnd = new Random();
                int random = rnd.Next(1, 5);
                string[] tabString = null;
                Html obiekt1, obiekt2, obiekt3, obiekt4;
                obiekt1 = new Html(2, 2, 1, tabString);
                obiekt2 = new Html(2, 3, 2, tabString);
                obiekt3 = new Html(3, "lab2_3.csv");
                obiekt4 = new Html(2, 5, 4, tabString);

                if (random == 1)
                {
                    responseString = obiekt1.kodWynikowy();
                    obiekt1.zapisPliku(responseString,0);
                }
                else if (random == 2)
                {
                    responseString = obiekt2.kodWynikowy();
                    obiekt2.zapisPliku(responseString, 0);
                }
                else if (random == 3)
                {
                    responseString = obiekt3.kodWynikowy();
                    obiekt3.zapisPliku(responseString, 0);
                }
                else if (random == 4)
                {
                    responseString = obiekt4.kodWynikowy();
                    obiekt4.zapisPliku(responseString, 0);
                }
                Console.WriteLine(fileName+@"\index.html");
                responseString += status(request);
                contentView(responseString, response);
            }
            else
            {
                if (context.Request.Url.AbsolutePath.Substring(1) == "favicon.ico")
                {
                    return;
                }
                else
                {
                    context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    responseString = "404";
                    contentView(responseString, response);
                    Console.WriteLine(context.Response.StatusCode);
                }
            }
        }
        public string status(HttpListenerRequest request)
        {
            string str = "\n<br>";
            System.Collections.Specialized.NameValueCollection headers = request.Headers;
            foreach (string key in headers.AllKeys)
            {
                string[] values = headers.GetValues(key);
                if (values.Length > 0)
                {
                    str += key + ":"; 
                    foreach (string value in values)
                    {
                        str += " " + value + "\n<br>" ;
                    }
                }
                else
                    Console.WriteLine("There is no value associated with the header.");
            }
            return str;
        }
        public void contentView(string responseString, HttpListenerResponse response)
        {         
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
            response.ContentLength64 = buffer.Length;
            System.IO.Stream output = response.OutputStream;
            output.Write(buffer, 0, buffer.Length);
        }
    }
}
