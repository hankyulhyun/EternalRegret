using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Runtime.InteropServices;
using EternalRegret.State;

namespace EternalRegret
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddSingleton<ICurrent, Current>();

            builder.Services.AddTransient(
                sp => new HttpClient
                {
                    BaseAddress = new Uri("https://api.eternal-regret.hankyul.io/v1/")
                });

            await builder.Build().RunAsync();
        }
    }
}
