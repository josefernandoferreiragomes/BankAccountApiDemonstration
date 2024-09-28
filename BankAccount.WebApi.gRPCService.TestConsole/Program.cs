// See https://aka.ms/new-console-template for more information
using BankAccount.WebApi.gRPCService;
using Grpc.Net.Client;
using System.Threading.Tasks;
using BankAccount.GrpcGreeterClient;

Console.WriteLine("Hello, World! ... waiting for service to start...");

Task.Delay(1000).Wait();

Console.WriteLine("Press any key to continue...");
Console.ReadKey();

// The port number must match the port of the gRPC server.
using var channel = GrpcChannel.ForAddress("https://localhost:32769");
var client = new Greeter.GreeterClient(channel);
var reply = await client.SayHelloAsync(
                  new HelloRequest { Name = "GreeterClient" });
Console.WriteLine("Greeting: " + reply.Message);

var bankingClient = new BankService.BankServiceClient(channel);
var bankingReply = await bankingClient.GetCustomerAsync(new CustomerRequest() { CustomerId = 1} );
                  
Console.WriteLine("banking customer: " + bankingReply.FirstName);
Console.WriteLine("Press any key to exit...");
Console.ReadKey();
