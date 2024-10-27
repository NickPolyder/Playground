var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.SampleApp_SetCookiesOnResponse>("sampleapp-setcookiesonresponse");

builder.AddProject<Projects.SampleApp_SetCookiesOnResponse_FrontEnd>("sampleapp-setcookiesonresponse-frontend");

builder.Build().Run();
