using System;
using System.Net.Http;
using System.Threading.Tasks;
using CreditRatingService;
using Grpc.Net.Client;

namespace gRPCService.CreditRatingClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var httpHandler = new HttpClientHandler();
            httpHandler.ServerCertificateCustomValidationCallback = 
                HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            
            var channel = GrpcChannel.ForAddress("https://localhost:5001",new GrpcChannelOptions { HttpHandler = httpHandler });
            
            var client =  new CreditRatingCheck.CreditRatingCheckClient(channel);
            var creditRequest = new CreditRequest { CustomerId = "id0201", Credit = 7000};
            var reply = await client.CheckCreditRequestAsync(creditRequest);

            Console.WriteLine($"Credit for customer {creditRequest.CustomerId} {(reply.IsAccepted ? "approved" : "rejected")}!");
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
            
        }
        
    }
}
