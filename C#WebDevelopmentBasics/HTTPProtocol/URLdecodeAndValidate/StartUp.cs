using System;
using System.Net;
using System.Text;

namespace URLdecodeAndValidate
{
    public class StartUp
    {
        public static void Main()
        {
            string url = Console.ReadLine();

            string urlDecode = WebUtility.UrlDecode(url);


            Uri baseUrl = new Uri(urlDecode);

            if (string.IsNullOrWhiteSpace(baseUrl.Scheme) ||
                string.IsNullOrWhiteSpace(baseUrl.Host) ||
                string.IsNullOrWhiteSpace(baseUrl.LocalPath) ||
                !baseUrl.IsDefaultPort)
            {
                Console.WriteLine("Invalid URL");
                return;
            }

            var builder = new StringBuilder();
            builder
                .AppendLine($"Protocol: {baseUrl.Scheme}")
                .AppendLine($"Host: {baseUrl.Host}")
                .AppendLine($"Port: {baseUrl.Port}")
                .AppendLine($"Path: {baseUrl.LocalPath}");

            if (!string.IsNullOrWhiteSpace(baseUrl.Query))
            {
                builder.AppendLine($"Query: {baseUrl.Query.Substring(1)}");
            }

            if (!string.IsNullOrWhiteSpace(baseUrl.Fragment))
            {
                builder.AppendLine($"Fragment: {baseUrl.Fragment.Substring(1)}");
            }

            Console.WriteLine(builder.ToString().Trim());

        }
    }
}
