﻿using WebApi2._0.models.biblioteca;
using Microsoft.Data.SqlClient;

namespace WebApi2._0.Data.bibioteca_data
{

    public class Prestamos_data
    {
        private static readonly string baseDatos = "master";

        // Insertar Prestamo
        public static bool InsertarPrestamo(Prestamo_models prestamo)
        {
            using SqlConnection conexion = Conn_general.ObtenerConexion(baseDatos);
            conexion.Open();
            string query = "INSERT INTO Prestamos (Numero, FechaPrestamo, Descripcion) VALUES (@numero, @fecha, @descripcion)";
            SqlCommand comando = new(query, conexion);
            comando.Parameters.AddWithValue("@numero", prestamo.Numero);
            comando.Parameters.AddWithValue("@fecha", prestamo.FechaPrestamo);
            comando.Parameters.AddWithValue("@descripcion", prestamo.Descripcion);

            try
            {
                comando.ExecuteNonQuery();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error al insertar Prestamo: {e.Message}");
                return false;
            }
        }

        // Consultar todos los Prestamos
        public static List<Prestamo_models> ConsultarPrestamos()
        {
            using SqlConnection conexion = Conn_general.ObtenerConexion(baseDatos);
            conexion.Open();
            string query = "SELECT * FROM Prestamos";
            SqlCommand comando = new(query, conexion);
            SqlDataReader reader = comando.ExecuteReader();
            List<Prestamo_models> lista = new();
            while (reader.Read())
            {
                Prestamo_models prestamo = new()
                {
                    Numero = reader.GetInt32(0),
                    FechaPrestamo = reader.GetDateTime(1),
                    Descripcion = reader.GetString(2)
                };
                lista.Add(prestamo);
            }
            return lista;
        }

        // Consultar Prestamo por Numero
        public static Prestamo_models ConsultarPrestamo(int numero)
        {
            using SqlConnection conexion = Conn_general.ObtenerConexion(baseDatos);
            conexion.Open();
            string query = "SELECT * FROM Prestamos WHERE Numero = @numero";
            SqlCommand comando = new(query, conexion);
            comando.Parameters.AddWithValue("@numero", numero);
            SqlDataReader reader = comando.ExecuteReader();
            if (reader.Read())
            {
                return new Prestamo_models()
                {
                    Numero = reader.GetInt32(0),
                    FechaPrestamo = reader.GetDateTime(1),
                    Descripcion = reader.GetString(2)
                };
            }
            return null;
        }

        // Actualizar Prestamo
        public static bool ActualizarPrestamo(int numero, Prestamo_models prestamo)
        {
            using SqlConnection conexion = Conn_general.ObtenerConexion(baseDatos);
            conexion.Open();
            string query = "UPDATE Prestamos SET FechaPrestamo = @fecha, Descripcion = @descripcion WHERE Numero = @numero";
            SqlCommand comando = new(query, conexion);
            comando.Parameters.AddWithValue("@numero", numero);
            comando.Parameters.AddWithValue("@fecha", prestamo.FechaPrestamo);
            comando.Parameters.AddWithValue("@descripcion", prestamo.Descripcion);

            try
            {
                int rowsAffected = comando.ExecuteNonQuery();
                return rowsAffected > 0;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error al actualizar Prestamo: {e.Message}");
                return false;
            }
        }

        // Eliminar Prestamo
        public static bool EliminarPrestamo(int numero)
        {
            using SqlConnection conexion = Conn_general.ObtenerConexion(baseDatos);
            conexion.Open();
            string query = "DELETE FROM Prestamos WHERE Numero = @numero";
            SqlCommand comando = new(query, conexion);
            comando.Parameters.AddWithValue("@numero", numero);

            try
            {
                int rowsAffected = comando.ExecuteNonQuery();
                return rowsAffected > 0;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error al eliminar Prestamo: {e.Message}");
                return false;
            }
        }
    }

}
