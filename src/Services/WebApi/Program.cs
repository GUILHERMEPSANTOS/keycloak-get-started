using WebApi.Configuration;
using WebApi.Core.User;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;


builder.Services.AddControllers();
builder.Services.AddCorsConfiguration();
builder.Services.AddScoped<IAspNetUser, AspNetUser>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerConfiguration();
builder.Services.AddAuthConfiguration(configuration, IsDevelopment: true, publicKeyJWT: "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAkNT6GycF0/A6a4ELr0YoYKsSVI87EM1p4OkoRkXeQRiZQvlkBxM9T4QvjSlqtDteZcPDI4Y5psa2W23fNJ+oygu+Ns9VQFNca+12GlCjfNP6DY/LxIlsxPCoHqYlIZZnzKvoB29juy3E+TwuaF2yUK44lSAtTuiqE3KquEjJQO3bjNnvZYG3bX+cEpXP7jxL/d2nJsj6UodYv7GMGXDJZHzd0Lb2Cygplq0NbwWidnC27Ty5YDclLY+bUYOXfXl904dFLUl7PVw6EIspylLrKk4VZtGUBADdD1uzKFP6rAjXuEs0BJJQRc1CA6XvAXi8Kfa8FXMjgrxMTjA1ah/UcQIDAQAB");

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSwaggerConfiguration();
app.UseHttpsRedirection();
app.UseCorsConfiguration();
app.UseAuthConfiguration();
app.MapControllers();
app.Run();

    