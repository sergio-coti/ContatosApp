var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//definindo a politica de permiss�o do CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("DefaultPolicy",
        builder =>
        {
            builder.AllowAnyOrigin() //qualquer projeto poder� acessar a API
                   .AllowAnyMethod() //permiss�o para qualquer m�todo POST, PUT, DELETE, GET
                   .AllowAnyHeader(); //permiss�o para envio de dados no cabe�alho das requisi��es
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

//adicionando a politica
app.UseCors("DefaultPolicy");

app.MapControllers();

app.Run();
