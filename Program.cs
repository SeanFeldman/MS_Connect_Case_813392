using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Remoting.Messaging;
using System.Threading.Tasks;

namespace MS_Connect_Case_813392
{
    public static class Program
    {
        static void Main(string[] args)
        {
            // PASS not quoted without semicolon
            Execute("https://github.com/maxmind/geoip-api-csharp2/archive/master.zip");

            // PASS quoted without semicolon
            Execute("https://androidnetworktester.googlecode.com/files/10mb.txt");
            
            // FAIL quoted with semicolon
            Execute("http://freedownloads.last.fm/download/499536933/Lord%2Bof%2Bthe%2BGame%2B%2528ft.%2BMexican%2BGirl%2529.mp3");
            
            Console.ReadLine();
        }

        static void Execute(string uri)
        {
            Console.WriteLine("-----------------------------------------------------------------------------");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(uri);
            Console.ResetColor();
            Console.WriteLine("-----------------------------------------------------------------------------");
            var headers = Call(uri).Result;
            Console.WriteLine(headers.ToString());
            Console.WriteLine(string.Format("headers.ContentDisposition != null  : {0}", headers.ContentDisposition != null));
            Console.WriteLine(string.Format("headers.ContentDisposition.FileName : {0}", headers.ContentDisposition != null ? headers.ContentDisposition.FileName : "null"));
            Console.WriteLine();
        }

        static async Task<HttpContentHeaders> Call(string uri)
        {
            using (var httpClient = new HttpClient())
            {
                var responseMessage = await httpClient.GetAsync(uri, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(continueOnCapturedContext: false);
                return responseMessage.Content.Headers;
            }
        }
    }
}
