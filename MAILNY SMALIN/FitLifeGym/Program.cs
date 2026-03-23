using FitLifeGym.Database;
using FitLifeGym.Repository;
using FitLifeGym.Services;
using FitLifeGym.Screens;
using System;

namespace FitLifeGym
{
    class Program
    {
        static void Main(string[] args)
        {
            // Initializing Database
            var dbConfig = new DatabaseConfig();
            dbConfig.InitializeDatabase();

            // Set up DI manually (Constructor Injection)
            IMiembroRepository repo = new MiembroRepository(dbConfig);
            IMiembroService service = new MiembroService(repo);
            MemberScreen screen = new MemberScreen(service);

            // Run the app
            screen.ShowMenu();
        }
    }
}
