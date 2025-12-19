var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.PLL_API_ODS>("pll-api-ods");

builder.AddProject<Projects.PLL_MVC_ODS>("pll-mvc-ods");

builder.Build().Run();
