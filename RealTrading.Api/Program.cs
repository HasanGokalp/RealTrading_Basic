using RealTrading.Api.BackGroundServices;
using RealTrading.Api.Hubs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddHostedService<TransmitterService>();

builder.Services.AddCors();

builder.Services.AddSwaggerGen();

builder.Services.AddSignalR();

var app = builder.Build();

if (app.Environment.IsDevelopment())
	{
	app.UseSwagger();
	app.UseSwaggerUI();
	}	

app.UseCors(opt => opt.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

app.UseHttpsRedirection();

app.MapHub<PriceHub>("pricehub");



app.Run();
