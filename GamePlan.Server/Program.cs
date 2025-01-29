using GamePlan.Api;
using GamePlan.Application.DependencyInjection;
using GamePlan.DAL.DependencyInjection;
using GamePlan.Domain.Settings;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection(JwtSettings.DefaultSections));

builder.Services.AddControllers();
builder.Services.AddAuthenticationAndAuthorization(builder);
builder.Services.AddSwagger();

builder.Services.AddDataAccessLayer(builder.Configuration);
builder.Services.AddApplication();

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI(c =>
	{
		c.SwaggerEndpoint("/swagger/v1/swagger.json", "GamePlan Swagger v1.0");
		//c.SwaggerEndpoint("/swagger/v2/swagger.json", "GamePlan Swagger v2.0");
	});
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
