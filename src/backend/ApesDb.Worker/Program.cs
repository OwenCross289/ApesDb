using ApesDb.Common;
using ApesDb.Worker;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddApesDbCommon();
builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
