using RoboGuidanceSystem.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
RegisterServices(builder.Services);
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.MapControllers();
app.Run();

return 0;



void RegisterServices(IServiceCollection serviceCollection)
{
    serviceCollection.AddSingleton<RobotCommunicationService>();
}
