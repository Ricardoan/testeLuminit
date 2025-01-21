using Api.Configurations;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Add AutoMapper Obs: Sem o AutoMapper a injeção de dependencia não funciona
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Add your dependency injection here
//Metodos na classe ConfigureService
builder.Services.AddAplications(); 
builder.Services.AddAdapters();
builder.Services.AddBusiness();


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

app.UseAuthorization();

app.MapControllers();
app.UseHttpsRedirection();

app.Run();

