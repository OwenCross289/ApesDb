using ApesDb.Common;
using ApesDb.Domain;
using ApesDb.Worker;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddApesDbCommon();
builder.Services.AddApesDbDomain(builder.Configuration);
builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
