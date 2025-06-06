using System;
using System.Data;
using System.Data.SqlClient;

namespace GestiondeFlotas
{
    public class Mantenimiento
    {
        public int MantenimientoID { get; set; }
        public int VehiculoID { get; set; }
        public DateTime FechaMantenimiento { get; set; }
        public string TipoMantenimiento { get; set; }
        public string Descripcion { get; set; }

        private readonly string connDB =    "Data Source=.\\SQLEXPRESS;Initial Catalog=GestionFlotas;Integrated Security=True;";

        // Agregar un nuevo registro de mantenimiento
        public bool Agregar(int mantenimientoId, int vehiculoId, DateTime fecha, string tipo, string descripcion)
        {
            using (SqlConnection conn = new SqlConnection(connDB))
            {
                string query = "INSERT INTO Mantenimiento (MantenimientoID, VehiculoID, FechaMantenimiento, TipoMantenimiento, Descripcion) " +
                               "VALUES (@MantenimientoID, @VehiculoID, @FechaMantenimiento, @TipoMantenimiento, @Descripcion)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MantenimientoID", mantenimientoId);
                cmd.Parameters.AddWithValue("@VehiculoID", vehiculoId);
                cmd.Parameters.AddWithValue("@FechaMantenimiento", fecha);
                cmd.Parameters.AddWithValue("@TipoMantenimiento", tipo);
                cmd.Parameters.AddWithValue("@Descripcion", descripcion ?? (object)DBNull.Value);

                conn.Open();
                int result = cmd.ExecuteNonQuery();
                return result > 0;
            }
        }

        // Buscar todos los registros de mantenimiento
        public DataTable Buscar()
        {
            using (SqlConnection conn = new SqlConnection(connDB))
            {
                string query = "SELECT * FROM Mantenimiento";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        // Buscar registros de mantenimiento por MantenimientoID
        public DataTable BuscarPorMantenimientoId(int mantenimientoId)
        {
            using (var connection = new SqlConnection(connDB))
            {
                string query = "SELECT * FROM Mantenimiento WHERE MantenimientoID = @MantenimientoID";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MantenimientoID", mantenimientoId);
                    using (var adapter = new SqlDataAdapter(command))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        return dt;
                    }
                }
            }
        }

        // Buscar registros de mantenimiento por VehiculoID
        public DataTable BuscarPorVehiculoId(int vehiculoId)
        {
            using (var connection = new SqlConnection(connDB))
            {
                string query = "SELECT * FROM Mantenimiento WHERE VehiculoID = @VehiculoID";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@VehiculoID", vehiculoId);
                    using (var adapter = new SqlDataAdapter(command))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        return dt;
                    }
                }
            }
        }

        // Buscar registros de mantenimiento por MantenimientoID y VehiculoID
        public DataTable BuscarPorIds(int mantenimientoId, int vehiculoId)
        {
            using (var connection = new SqlConnection(connDB))
            {
                string query = "SELECT * FROM Mantenimiento WHERE MantenimientoID = @MantenimientoID AND VehiculoID = @VehiculoID";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MantenimientoID", mantenimientoId);
                    command.Parameters.AddWithValue("@VehiculoID", vehiculoId);
                    using (var adapter = new SqlDataAdapter(command))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        return dt;
                    }
                }
            }
        }

        // Editar un registro de mantenimiento existente
        public bool Editar(int mantenimientoId, int vehiculoId, DateTime fecha, string tipo, string descripcion)
        {
            using (SqlConnection conn = new SqlConnection(connDB))
            {
                string query = "UPDATE Mantenimiento SET VehiculoID = @VehiculoID, FechaMantenimiento = @FechaMantenimiento, " +
                               "TipoMantenimiento = @TipoMantenimiento, Descripcion = @Descripcion " +
                               "WHERE MantenimientoID = @MantenimientoID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MantenimientoID", mantenimientoId);
                cmd.Parameters.AddWithValue("@VehiculoID", vehiculoId);
                cmd.Parameters.AddWithValue("@FechaMantenimiento", fecha);
                cmd.Parameters.AddWithValue("@TipoMantenimiento", tipo);
                cmd.Parameters.AddWithValue("@Descripcion", descripcion ?? (object)DBNull.Value);

                conn.Open();
                int result = cmd.ExecuteNonQuery();
                return result > 0;
            }
        }

        // Eliminar un registro de mantenimiento existente
        public bool Eliminar(int mantenimientoId)
        {
            try
            {
                using (var connection = new SqlConnection(connDB))
                {
                    connection.Open();
                    string query = "DELETE FROM Mantenimiento WHERE MantenimientoID = @MantenimientoID";
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@MantenimientoID", mantenimientoId);
                        int rowsAffected = command.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}