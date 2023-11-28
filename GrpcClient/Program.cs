using Grpc.Core;
using Grpc.Net.Client;
using GrpcServer;

var channel = GrpcChannel.ForAddress("http://localhost:5292");

//var input = new HelloRequest { Name = "Yuliia" };
//var client = new Greeter.GreeterClient(channel);

//var reply = await client.SayHelloAsync(input);
//Console.WriteLine(reply.Message);

var customerClient = new Customer.CustomerClient(channel);

var clientRequest = new CustomerLookupModel { UserId = 2 };

var customer = await customerClient.GetCustomerInfoAsync(clientRequest);

Console.WriteLine($"{customer.FirstName} {customer.LastName}");
Console.WriteLine("New customers list");

using (var call = customerClient.GetNewCustomers(new NewCustomerRequest()))
{
    while (await call.ResponseStream.MoveNext())
    {
        var currentCustomer = call.ResponseStream.Current;
        Console.WriteLine($"{currentCustomer.FirstName} {currentCustomer.LastName}: {currentCustomer.EmailAddress}");
    }
}
Console.ReadLine();
