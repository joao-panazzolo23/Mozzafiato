using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Mozzafiato.Areas.Admin.Services;
using Mozzafiato.Context;
using Mozzafiato.Models;
using Mozzafiato.Repositories;
using Mozzafiato.Repositories.Interfaces;
using Mozzafiato.Services;
using ReflectionIT.Mvc.Paging;

internal class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Conexão com o banco de dados
        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
                   .UseSnakeCaseNamingConvention());

        // Adicionando serviços
        builder.Services.AddControllersWithViews();
        builder.Services.AddIdentity<IdentityUser, IdentityRole>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        // Repositórios e outros serviços
        builder.Services.AddTransient<InterLancheRepository, LancheRepository>();
        builder.Services.AddTransient<InterCategoriaRepository, CategoriaRepository>();
        builder.Services.AddTransient<InterPedidoRepository, PedidoRepository>();
        builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        builder.Services.AddScoped(sp => CarrinhoCompra.GetCarrinho(sp));
        builder.Services.AddScoped<RelatorioVendasService>();

        builder.Services.AddScoped<GraficosVendaService>();
        builder.Services.AddPaging(options =>
        {
            options.ViewName = "Bootstrap4";
            options.PageParameterName = "pageIndex";
        });

        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
        });

        builder.Services.Configure<IdentityOptions>(options =>
        {
            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequiredLength = 0;
        });

        builder.Services.AddScoped<InterSeedRoleInitial, SeedUserRoleInitial>();
        builder.Services.AddMemoryCache();
        builder.Services.AddSession();
        builder.Services.Configure<ConfigureImagens>(
     builder.Configuration.GetSection("ConfigurationPastaImagens")
 );
        var app = builder.Build();

        // Configuração do pipeline HTTP
        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        // Cria os perfis e usuários
        using (var scope = app.Services.CreateScope())
        {
            var seedUserRoleInitial = scope.ServiceProvider.GetRequiredService<InterSeedRoleInitial>();
            seedUserRoleInitial.SeedRoles();
            seedUserRoleInitial.SeedUsers();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseSession();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllerRoute(
            name: "areas",
            pattern: "{area:exists}/{controller=Admin}/{action=Index}");

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller}/{action}/{id?}",
            defaults: new { controller = "Home", Action = "Index" });

        app.MapControllerRoute(
            name: "categoriaFiltro",
            pattern: "Lanche/{action}/{categoria?}",
            defaults: new { controller = "Lanche", Action = "List" });

        app.Run();
    }
    void CriarPerfisUsuario(WebApplication app)
    {
        var service = app.Services.GetService<InterSeedRoleInitial>();
        service.SeedRoles();
        service.SeedUsers();
     
    }

}
