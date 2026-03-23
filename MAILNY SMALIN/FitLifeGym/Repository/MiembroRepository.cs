using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using FitLifeGym.Database;
using FitLifeGym.Models;

namespace FitLifeGym.Repository
{
    public interface IMiembroRepository
    {
        void RegistrarMiembro(Miembro miembro);
        List<Miembro> ListarTodos();
        Miembro? BuscarPorCedula(string cedula);
        void ActualizarTelefono(string cedula, string nuevoTelefono);
        void EliminarMiembro(string cedula);
    }

    public class MiembroRepository : IMiembroRepository
    {
        private readonly DatabaseConfig _databaseConfig;

        public MiembroRepository(DatabaseConfig databaseConfig)
        {
            _databaseConfig = databaseConfig;
        }

        public void RegistrarMiembro(Miembro miembro)
        {
            using var connection = new SqliteConnection(_databaseConfig.ConnectionString);
            connection.Open();
            var query = "INSERT INTO Miembro (cedula, nombre_completo, telefono) VALUES (@cedula, @nombre, @tel)";
            using var cmd = new SqliteCommand(query, connection);
            cmd.Parameters.AddWithValue("@cedula", miembro.Cedula);
            cmd.Parameters.AddWithValue("@nombre", miembro.NombreCompleto);
            cmd.Parameters.AddWithValue("@tel", miembro.Telefono);
            cmd.ExecuteNonQuery();
        }

        public List<Miembro> ListarTodos()
        {
            var miembros = new List<Miembro>();
            using var connection = new SqliteConnection(_databaseConfig.ConnectionString);
            connection.Open();
            var query = "SELECT cedula, nombre_completo, telefono FROM Miembro";
            using var cmd = new SqliteCommand(query, connection);
            using var reader = cmd.ExecuteReader(); 
            while (reader.Read())
            {
                miembros.Add(new Miembro
                {
                    Cedula = reader.GetString(0),
                    NombreCompleto = reader.GetString(1),
                    Telefono = reader.GetString(2)
                });
            }
            return miembros;
        }

        public Miembro? BuscarPorCedula(string cedula)
        {
            using var connection = new SqliteConnection(_databaseConfig.ConnectionString);
            connection.Open();
            var query = "SELECT cedula, nombre_completo, telefono FROM Miembro WHERE cedula = @cedula";
            using var cmd = new SqliteCommand(query, connection);
            cmd.Parameters.AddWithValue("@cedula", cedula);
            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new Miembro
                {
                    Cedula = reader.GetString(0),
                    NombreCompleto = reader.GetString(1),
                    Telefono = reader.GetString(2)
                };
            }
            return null;
        }

        public void ActualizarTelefono(string cedula, string nuevoTelefono)
        {
            using var connection = new SqliteConnection(_databaseConfig.ConnectionString);
            connection.Open();
            var query = "UPDATE Miembro SET telefono = @tel WHERE cedula = @cedula";
            using var cmd = new SqliteCommand(query, connection);
            cmd.Parameters.AddWithValue("@tel", nuevoTelefono);
            cmd.Parameters.AddWithValue("@cedula", cedula);
            cmd.ExecuteNonQuery();
        }

        public void EliminarMiembro(string cedula)
        {
            using var connection = new SqliteConnection(_databaseConfig.ConnectionString);
            connection.Open();
            var query = "DELETE FROM Miembro WHERE cedula = @cedula";
            using var cmd = new SqliteCommand(query, connection);
            cmd.Parameters.AddWithValue("@cedula", cedula);
            cmd.ExecuteNonQuery();
        }
    }
}
