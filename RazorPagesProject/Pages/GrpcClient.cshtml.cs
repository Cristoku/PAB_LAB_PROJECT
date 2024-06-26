using Grpc.Net.Client;
using GrpcProject;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

public class GrpcClientModel : PageModel
{
    public string Message { get; private set; }

    public async Task OnGetAsync()
    {
        using var channel = GrpcChannel.ForAddress("https://localhost:5001");
        var client = new Greeter.GreeterClient(channel);
        var reply = await client.SayHelloAsync(new HelloRequest { Name = "World" });
        Message = reply.Message;
    }
}
