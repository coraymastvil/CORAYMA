using Microsoft.Data.Sqlite;
using System.IO;

namespace FitLifeGym.Database
{
    public class DatabaseConfig
    {
        private const string DatabaseFile = "GymDB.db";
        public string ConnectionString => $"Data Source={DatabaseFile};";

        public void InitializeDatabase()
        {
            using var connection = new SqliteConnection(ConnectionString);
            connection.Open();

            string createMiembroTable = @"
                CREATE TABLE IF NOT EXISTS Miembro (
                    cedula TEXT PRIMARY KEY,
                    nombre_completo TEXT NOT NULL,
                    telefono TEXT
                );
            ";

            // The prompt asks only for Miembro implementation in the app part.
            // But the case study mentions the other tables as well. 
            // I'll at least create Miembro as per requirements of the second part.
            
            using var command = new SqliteCommand(createMiembroTable, connection);
            command.ExecuteNonQuery();
        }
    }
}
