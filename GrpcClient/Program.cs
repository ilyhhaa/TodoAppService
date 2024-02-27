using Grpc.Net.Client;
using TodoAppService;


namespace GrpcClient;
internal class Program
{
    private static async void Main(string[] args)
    {
        const string ServerAddress = "";

        var channel = GrpcChannel.ForAddress(ServerAddress);

        var client = new Greeter.GreeterClient(channel);

        try
        {
            var newTodo = await client.CreateTodoAsync(new Todo { Discription = "Complete assigment" });
            Console.WriteLine($"Created todo with ID: {newTodo.Id}");
            var readTodo = await client.ReadTodoAsync(new TodoId { Id = newTodo.Id });
            Console.WriteLine($"Read todo: id = {readTodo.Id} ,  Description = {readTodo.Discription}");
        }
        catch (Exception ex) { } //Dodelat !!!!

		
    }
}