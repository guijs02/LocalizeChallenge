using LocalizeApi.Common;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddAuthentication();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddContext(builder.Configuration);
builder.Services.AddSwaggerGen();
builder.Services.AddIdentityRole();
builder.Services.ConfigJwtBearer(builder.Configuration);
builder.Services.AddDependencies();
builder.Services.AddSwaggerDocumentation();
builder.Services.ConfigIdentityOptions();
builder.Services.AddHttpClient("ReceitaWs", c =>
{
    c.BaseAddress = new Uri("https://www.receitaws.com.br/v1/");
    c.DefaultRequestHeaders.Accept.Clear();
    c.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
   app.AddSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }