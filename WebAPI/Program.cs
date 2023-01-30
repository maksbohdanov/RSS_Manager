using DAL;
using DAL.Interfaces;
using DAL.Repositories;
using AutoMapper;
using BLL.Mapping;



using Microsoft.EntityFrameworkCore;

namespace WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<RSSManagerDbContext>(options =>
                options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddScoped<IFeedRepository, FeedRepository>();
            builder.Services.AddScoped<INewsRepository, NewsRepository>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            var mapperConfig = new MapperConfiguration(mc =>
                 mc.AddProfile(new AutomapperProfile()));
            var mapper = mapperConfig.CreateMapper();
            builder.Services.AddSingleton(mapper);

           

            builder.Services.AddControllers();

            builder.Services.AddRouting(options => options.LowercaseUrls = true);

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}