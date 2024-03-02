using Grpc.Core;
using Grpc.Net.Client;
using TodoAppService;



namespace GrpcClient;
internal class Program
{
     static async Task Main(string[] args)
    {
        const string ServerAddress = "http://localhost:5103";

        var channel = GrpcChannel.ForAddress(ServerAddress);

        var client = new Greeter.GreeterClient(channel);

        try
        {
            var newTodo = await client.CreateTodoAsync(new Todo { Discription = "Complete assigment" });
            
            Console.WriteLine($"Created todo with ID: {newTodo.Id}");
            
            var readTodo = await client.ReadTodoAsync(new TodoId { Id = newTodo.Id });
            
            Console.WriteLine($"Read todo: id = {readTodo.Id} ,  Description = {readTodo.Discription}");

            var updateTodo = await client.UpdateTodoAsync(newTodo);
            await Console.Out.WriteLineAsync($"Updated Todo : ID ={updateTodo.Id} ");

            await client.DeleteTodoAsync(new TodoId
            {
                Id = updateTodo.Id
            });
            await Console.Out.WriteLineAsync("Todo Deleted");
        }
        catch (RpcException ex) 
        {
            await Console.Out.WriteLineAsync(ex.Message);

        }
       
        
        await channel.ShutdownAsync();

        await Console.Out.WriteLineAsync("Press any key to exit ");

       


    }
}