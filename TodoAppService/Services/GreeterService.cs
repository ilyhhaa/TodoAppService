using Grpc.Core;
using TodoAppService;



namespace TodoAppService.Services
{
    public class GreeterService : Greeter.GreeterBase
    {
        private readonly ILogger<GreeterService> _logger;
        public GreeterService(ILogger<GreeterService> logger)
        {
            _logger = logger;
        }

        private List<Todo> todos = new List<Todo>();

       public override Task<Todo> CreateTodo(Todo request, ServerCallContext context)
        {
            int newid = todos.Count + 1;

            request.Id = newid;
            todos.Add(request);

            return Task.FromResult(request);
        }

        public override Task<Todo> ReadTodo(TodoId request, ServerCallContext context)
        {
            Todo todo = todos.FirstOrDefault(x => x.Id == request.Id);

            if (todo != null) 
            {
                return Task.FromResult(todo);
            }
            else
            {
                throw new RpcException(new Status(StatusCode.NotFound, "Todo not Found"));
            }

        }

        public override Task<Todo> UpdateTodo(Todo request, ServerCallContext context)
        {
            Todo todo = todos.FirstOrDefault(x => x.Id == request.Id);

            if (todo != null)
            {
               todo.Discription = request.Discription;
                return Task.FromResult(todo);
            }
            else
            {
                throw new RpcException(new Status(StatusCode.NotFound, "Todo not Found"));
            }

        }

        public override Task<Google.Protobuf.WellKnownTypes.Empty> DeleteTodo(TodoId request, ServerCallContext context)
        {
            Todo todo = todos.FirstOrDefault(x => x.Id == request.Id);

            if (todo != null)
            {
                todos.Remove(todo);
                return Task.FromResult(new Google.Protobuf.WellKnownTypes.Empty());
            }
            else
            {
                throw new RpcException(new Status(StatusCode.NotFound, "Todo not Found"));
            }

        }
    }
}