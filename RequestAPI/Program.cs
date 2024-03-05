using Azure.Storage.Queues;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddScoped<QueueClient>((service) => new QueueClient(service.GetService<IConfiguration>()?.GetConnectionString("REQUEST_QUEUE"), "knightmoverequests"));


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAzureAppConfiguration();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
