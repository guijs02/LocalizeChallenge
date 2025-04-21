using LocalizeApi.Context;
using LocalizeApi.Services;
using LocalizeApi.Services.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Localize.TestE2E
{
    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // Remover a configuração real do banco
                var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<LocalizeDbContext>));
                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }

                // Adicionar um banco em memória para testes
                services.AddDbContext<LocalizeDbContext>(options =>
                {
                    options.UseInMemoryDatabase("TestDb");
                });

                // Remover o IHttpClientFactory para evitar chamadas reais
                services.AddSingleton<IReceitaWsService, FakeReceitaWsApi>();


                // Criar o banco e aplicar migrations se necessário
                var sp = services.BuildServiceProvider();
                using var scope = sp.CreateScope();
                var db = scope.ServiceProvider.GetRequiredService<LocalizeDbContext>();
                db.Database.EnsureCreated();
            });
        }
    }
}
