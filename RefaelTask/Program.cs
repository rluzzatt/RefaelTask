using RefaelTask.Interfaces;
using RefaelTask.Services;
using RefaelTask.Messages;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<MessageSubscriber<RequestTextMessage>>();
builder.Services.AddScoped<ISubscriber<RequestTextMessage>, MessageSubscriber<RequestTextMessage>>();
builder.Services.AddScoped<IPublisher<RequestTextMessage>, MessagePublisher<RequestTextMessage>>();

builder.Services.AddScoped<MessageSubscriber<ResponseTextMessage>>();
builder.Services.AddScoped<ISubscriber<ResponseTextMessage>, MessageSubscriber<ResponseTextMessage>>();
builder.Services.AddScoped<IPublisher<ResponseTextMessage>, MessagePublisher<ResponseTextMessage>>();

// Register Requester and Replier with the correct dependencies
builder.Services.AddScoped<IReplier<RequestTextMessage, ResponseTextMessage>, Replier>();
builder.Services.AddScoped<IRequester<RequestTextMessage, ResponseTextMessage>, Requester>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
