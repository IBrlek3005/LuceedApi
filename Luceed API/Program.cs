using Luceed_API.Options;
using Luceed_API.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
const string ORIGIN_NAME = "_myAllowSpecificOrigins";
var origin = builder.Configuration["AllowedOrigin"];
if (!string.IsNullOrEmpty(origin))
    builder.Services.AddCors(options => options.AddPolicy(ORIGIN_NAME,
        builder => builder.WithOrigins(origin)
            .SetIsOriginAllowed(origin => true)
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials()
            .WithExposedHeaders("Content-Disposition")));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<LuceedOptions>(builder.Configuration.GetSection("LuceedAPI"));
builder.Services.AddScoped<LuceedService>();
builder.Services.AddHttpClient();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(ORIGIN_NAME);
app.UseAuthorization();

app.MapControllers();

app.Run();
