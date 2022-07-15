// See https://aka.ms/new-console-template for more information
using Grpc.Core;
using Grpc.Net.Client;
using GrpcServer;

// var input = new HelloRequest { Name = "UserName" };
var clientRequested = new CustomerLookupModel { UserId = 2 };

var channel = GrpcChannel.ForAddress("https://localhost:7230");
//var client = new Greeter.GreeterClient(channel);

//var reply = await client.SayHelloAsync(input);

//Console.WriteLine(reply.Message);

var customerClient = new Customer.CustomerClient(channel);

var customer = await customerClient.GetCustomerInfoAsync(clientRequested);

Console.WriteLine($"{customer.FirstName} {customer.LastName}");
Console.WriteLine("New Customer List:");

using (var call = customerClient.GetNewCustomers(new NewCustomerRequest()))
{
    while (await call.ResponseStream.MoveNext())
    {
        var currentCustomer = call.ResponseStream.Current;
        Console.WriteLine($"{currentCustomer.FirstName} {currentCustomer.LastName} {currentCustomer.EmailAddress}");
    }
}

Console.ReadKey();
