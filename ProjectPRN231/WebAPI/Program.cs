using WebAPI.DataAccess;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMvc();
builder.Services.AddCors();
builder.Services.AddDbContext<PRN231_DBContext>();
builder.Services.AddControllers
    (options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true).AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);
var app = builder.Build();
app.UseCors();
app.MapControllers();

app.MapGet("/", () => "Hello World!");

app.Run();
