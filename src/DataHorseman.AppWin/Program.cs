using DataHorseman.Domain.Interfaces;
using DataHorseman.Infrastructure.Persistencia.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace DataHorseman.AppWin;

internal static class Program
{
    public static IServiceProvider ServiceProvider { get; private set; }
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        var services = new ServiceCollection();

        // Registra os servi�os da camada Application
        services.AddScoped<Application.Interfaces.IService, Application.Services.Service>();
        services.AddScoped<IRepository, Repository>();
        services.AddAutoMapper(typeof(Application.Profiles.AtivoProfile).Assembly);
        // Registra o formul�rio
        services.AddTransient<frmUpload>();

        // Construir o ServiceProvider a partir do ServiceCollection
        ServiceProvider = services.BuildServiceProvider();

        // Inicializar a configura��o do aplicativo (High DPI, estilo, etc.)
        ApplicationConfiguration.Initialize();

        // Obter a inst�ncia de frmUpload a partir do ServiceProvider
        var mainForm = ServiceProvider.GetRequiredService<frmUpload>();

        // Iniciar a aplica��o com o formul�rio principal
        System.Windows.Forms.Application.Run(mainForm);
    }
}