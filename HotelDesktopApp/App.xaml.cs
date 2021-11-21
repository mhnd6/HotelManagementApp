using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using DataAcessLibrary.Data;
using DataAcessLibrary.Databases;

namespace HotelDesktopApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static ServiceProvider serviceProvider;
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var services = new ServiceCollection();
            //Transient means we always gonna get new instance
            services.AddTransient<MainWindow>();
            services.AddTransient<CheckIn>();
            services.AddTransient<ISqlDataAccess, SqlDataAccess>();
            services.AddTransient<ISqliteDataAccess, SqliteDataAccess>();

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            IConfiguration config = builder.Build();

            services.AddSingleton(config);

            string dbCoice = config.GetValue<string>("DatabaseChoice").ToLower();

            if (dbCoice == "sql")
            {
                //Here we mapped the interface to SqlData type and Sql data asked for
                //ISqlDataAccess so upove we mapped ISqlDataAccess to SqlDataAccess type
                //We tell them what you will get

                services.AddTransient<IDataBaseData, SqlData>();
            }
            else if (dbCoice == "sqlite")
            {
                services.AddTransient<IDataBaseData, SqliteData>();
            }
            else
            {
                services.AddTransient<IDataBaseData, SqlData>();
            }

            serviceProvider = services.BuildServiceProvider();
            var mainWindow = serviceProvider.GetService<MainWindow>();

            mainWindow.Show();
        }
    }
}
