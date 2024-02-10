using TestsWebApplication.Context;
using TestsWebApplication.Repository;
using TestsWebApplication.Repository.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddSingleton<DapperContext>();
builder.Services.AddScoped<IDisciplinaAlunoRepository, DisciplinaAlunoRepository>();

builder.Services.AddTransient<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddTransient<ITipoEnsinoRepository, TipoEnsinoRepository>();
builder.Services.AddTransient<IMatriculaRepository, MatriculaRepository>();
builder.Services.AddTransient<IRematriculaRepository, RematriculaRepository>();
builder.Services.AddTransient<IAreaRepository, AreaRepository>();
builder.Services.AddTransient<IDisciplinaRepository, DisciplinaRepository>();
builder.Services.AddTransient<ITipoAtendimentoRepository, TipoAtendimentoRepository>();
builder.Services.AddTransient<IAlunoRepository, AlunoRepository>();
builder.Services.AddTransient<IEnderecoRepository, EnderecoRepository>();

//TODO: Implementar classe para adcionar serviços de repositorios. Verificar Classes de serviço que utilizam repositorios 

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
