using TodoApi;

internal class Program
{
    private static void Main(string[] args)
    {
        // create app builder
        var builder = WebApplication.CreateBuilder(args);
        ConfigureServices(builder, builder.Services);

        // build, configure app
        var app = builder.Build();
        ConfigurApp(app);

        // run app
        // redirect to swagger
        if (app.Environment.IsDevelopment())
        {
            app.Run(async context =>
            {
                if (context.Request.Path == "/")
                {
                    context.Response.Redirect("/swagger");
                }
            });
        }

        app.Run();
    }

    private static void ConfigureServices(WebApplicationBuilder builder, IServiceCollection services)
    {
        // databases
        services.Configure<TodoDatabaseSettings>(
            builder.Configuration.GetSection("TodoDatabase"));

        // api
        services.AddSingleton<TodoService>();

        // controllers
        services.AddControllers();

        // swagger
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
    }

    private static void ConfigurApp(WebApplication app)
    {
        // set as development
        if (app.Environment.IsDevelopment())
        {
            // enable swagger
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        // map controllers
        app.MapControllers();
    }
}