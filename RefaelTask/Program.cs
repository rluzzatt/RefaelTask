using RefaelTask.Interfaces;
using RefaelTask.Services;
using RefaelTask.Messages;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<MessageSubscriber<RequestTextMessage>>();
builder.Services.AddScoped<MessageSubscriber<ResponseTextMessage>>();

builder.Services.AddScoped(sp =>
{
    var subscriber = sp.GetRequiredService<MessageSubscriber<RequestTextMessage>>();
    return new MessagePublisher<RequestTextMessage>(subscriber);
});

builder.Services.AddScoped(sp =>
{
    var subscriber = sp.GetRequiredService<MessageSubscriber<ResponseTextMessage>>();
    return new MessagePublisher<ResponseTextMessage>(subscriber);
});

// Register Requester and Replier
builder.Services.AddScoped<IRequester<RequestTextMessage, ResponseTextMessage>>(sp =>
{
    var requestPublisher = sp.GetRequiredService<MessagePublisher<RequestTextMessage>>();
    var responseSubscriber = sp.GetRequiredService<MessageSubscriber<ResponseTextMessage>>();
    return new Requester(requestPublisher, responseSubscriber);
});

builder.Services.AddScoped<IReplier<RequestTextMessage, ResponseTextMessage>>(sp =>
{
    var responsePublisher = sp.GetRequiredService<MessagePublisher<ResponseTextMessage>>();
    var requestSubscriber = sp.GetRequiredService<MessageSubscriber<RequestTextMessage>>();
    return new Replier(responsePublisher, requestSubscriber);
});

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
