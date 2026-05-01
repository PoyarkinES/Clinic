using Service;
using Grpc.Core;

namespace Service.Services
{
    public class MongoService : Greeter.GreeterBase
    {
        private readonly ILogger<MongoService> _logger;
        public MongoService(ILogger<MongoService> logger)
        {
            _logger = logger;
        }

        public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            return Task.FromResult(new HelloReply
            {
                Message = "Hello " + request.Name
            });
        }
    }
}
