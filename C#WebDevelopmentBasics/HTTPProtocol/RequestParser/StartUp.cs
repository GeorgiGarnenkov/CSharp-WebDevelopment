using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace RequestParser
{
    public class StartUp
    {
        public static void Main()
        {
            // / register / get
            // / register / post
            // END
            // GET / register HTTP / 1.1

            // / login / get
            // / register / get
            // / login / post
            // END
            // POST / register HTTP / 1.1

            var endPointsByHttpMethod = new Dictionary<string, HashSet<string>>();

            string input;

            while ((input = Console.ReadLine()) != "END")
            {
                var splitInput = input
                    .ToLower()
                    .Split("/", StringSplitOptions.RemoveEmptyEntries);

                var httpMethod = splitInput[1];
                var endPoint = splitInput[0];

                if (!endPointsByHttpMethod.ContainsKey(httpMethod))
                {
                    endPointsByHttpMethod.Add(httpMethod, new HashSet<string>());
                }

                endPointsByHttpMethod[httpMethod].Add(endPoint);

            }

            var request = Console.ReadLine()
                .Split(" ", StringSplitOptions.RemoveEmptyEntries);

            var requestHttpMethod = request[0].ToLower();
            var requestEndPoint = request[1].ToLower();
            var requestProtocol = request[2];

            if (endPointsByHttpMethod.ContainsKey(requestHttpMethod))
            {
                var endPoint = endPointsByHttpMethod[requestHttpMethod]
                    .FirstOrDefault(e => "/"+ e == requestEndPoint);

                if (endPoint != null)
                {
                    var httpResponseString = 
                        $"{requestProtocol} {(int)HttpStatusCode.OK} {HttpStatusCode.OK}" + Environment.NewLine +
                                             $"Content-Length: {HttpStatusCode.OK.ToString().Length}" + Environment.NewLine +
                                             "Content-Type: text/plain" + Environment.NewLine +
                                             Environment.NewLine +
                                             $"{HttpStatusCode.OK}";

                    Console.WriteLine(httpResponseString);
                    return;
                }
            }

            var errorResponseString =
                $"{requestProtocol} {(int)HttpStatusCode.NotFound} {HttpStatusCode.NotFound}" + Environment.NewLine +
                $"Content-Length: {HttpStatusCode.NotFound.ToString().Length}" + Environment.NewLine +
                "Content-Type: text/plain" + Environment.NewLine +
                Environment.NewLine +
                $"{HttpStatusCode.NotFound}";

            Console.WriteLine(errorResponseString);
        }
    }
}
