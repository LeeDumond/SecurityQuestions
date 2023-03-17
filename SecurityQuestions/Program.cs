using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SecurityQuestions.Data;
using SecurityQuestions.Services;

namespace SecurityQuestions
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using IHost host = Host.CreateDefaultBuilder(args).ConfigureServices(services =>
            {
                services.AddScoped<IFlowService, FlowService>();
                services.AddScoped<ISecurityQuestionService, SecurityQuestionService>();
                services.AddScoped<IDataRepository, JsonFileRepository>();
            }).Build();

            IFlowService flowService = host.Services.GetRequiredService<IFlowService>();

            flowService.RunInitialFlow();
        }
    }
}