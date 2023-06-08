using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MyProtocolsAPI_AlllanD.Models;

namespace MyProtocolsAPI_AlllanD
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            //vamos a leer la etiqueta CNNSTR de appsettings.json para configurar la conexión 
            //a la base de datos. 
            var CnnStrBuilder = new SqlConnectionStringBuilder(builder.Configuration.GetConnectionString("CNNSTR"));

            //eliminamos del la CNNSTR el dato del password ya que sería muy sencillo obtener la info de conexión
            //del usuario de SQL Server del archivo de config appsettings.json.
            CnnStrBuilder.Password = "123456";
            
            //CnnStrBuilder es un objeto que permite la construcción de cadenas de conexión a bases de datos. 
            //se pueden modificar cada parte de la misma, pero el final debemos extraer un string con la info final
            string cnnStr = CnnStrBuilder.ConnectionString;

            //ahora conectamos el proyecto a la base de datos usando cnnStr
            builder.Services.AddDbContext<MyProtocolsDBContext>(options => options.UseSqlServer(cnnStr));


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

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}