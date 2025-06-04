using HandsOnEFCodeFirst.Entities;

namespace HandsOnEFCodeFirst
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddTransient<UworldDBContext>();
            builder.Services.AddControllers();
            builder.Services.AddCors(c =>
            {
                c.AddPolicy("AllowOrigin", options =>
       options.AllowAnyOrigin() //allows any client url
                .AllowAnyMethod() //allows all http methods like(get,post,put and delete)
                .AllowAnyHeader() //all any header like request,response and so on
                );
            });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            else if(app.Environment.IsProduction())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCors("AllowOrigin");
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}