var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.LoteTablas_Grpc_Lottery_Service>("grpc-lottery")
    .AsHttp2Service();   

builder.add


//builder.AddProject<Projects.LoteTablas_Grpc_Card_Service>("grpc-card").AsHttp2Service();

builder.Build().Run();
