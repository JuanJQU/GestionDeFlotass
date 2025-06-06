using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GestiondeFlotas
{
    public class ViajeDAO
    {
        private string connDB = "Data Source=.\\SQLEXPRESS;Initial Catalog=GestionFlotas;Integrated Security=True;";
        public string ConnDB => connDB;

        public void AgregarViaje(Viajes viaje)
        {
            using (var conn = new SqlConnection(connDB))
            {
                conn.Open();
                var cmd = new SqlCommand(
                    "INSERT INTO Viajes (ViajeID, VehiculoID, ConductorID, Ruta, FechaSalida, FechaLlegada, Estado) " +
                    "VALUES (@ViajeID, @VehiculoID, @ConductorID, @Ruta, @FechaSalida, @FechaLlegada, @Estado)", conn
                );
                cmd.Parameters.AddWithValue("@ViajeID", viaje.ViajeID);
                cmd.Parameters.AddWithValue("@VehiculoID", viaje.VehiculoID);
                cmd.Parameters.AddWithValue("@ConductorID", viaje.ConductorID);
                cmd.Parameters.AddWithValue("@Ruta", viaje.Ruta);
                cmd.Parameters.AddWithValue("@FechaSalida", viaje.FechaSalida);
                cmd.Parameters.AddWithValue("@FechaLlegada", viaje.FechaLlegada);
                cmd.Parameters.AddWithValue("@Estado", viaje.Estado);

                cmd.ExecuteNonQuery();
            }
        }

        public void ActualizarViaje(Viajes viaje)
        {
            using (var conn = new SqlConnection(connDB))
            {
                conn.Open();
                var cmd = new SqlCommand(
                    "UPDATE Viajes SET VehiculoID = @VehiculoID, ConductorID = @ConductorID, Ruta = @Ruta, " +
                    "FechaSalida = @FechaSalida, FechaLlegada = @FechaLlegada, Estado = @Estado " +
                    "WHERE ViajeID = @ViajeID", conn
                );

                cmd.Parameters.AddWithValue("@ViajeID", viaje.ViajeID);
                cmd.Parameters.AddWithValue("@VehiculoID", viaje.VehiculoID);
                cmd.Parameters.AddWithValue("@ConductorID", viaje.ConductorID);
                cmd.Parameters.AddWithValue("@Ruta", viaje.Ruta);
                cmd.Parameters.AddWithValue("@FechaSalida", viaje.FechaSalida);
                cmd.Parameters.AddWithValue("@FechaLlegada", viaje.FechaLlegada);
                cmd.Parameters.AddWithValue("@Estado", viaje.Estado);

                cmd.ExecuteNonQuery();
            }
        }

        public bool EliminarViaje(int viajeId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connDB))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("DELETE FROM Viajes WHERE ViajeID = @ViajeID", conn);
                    cmd.Parameters.AddWithValue("@ViajeID", viajeId);
                    int rows = cmd.ExecuteNonQuery();

                    return rows > 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar el viaje: " + ex.Message, "Error");
                return false;
            }
        }

        public DataTable ObtenerDatosViajes()
        {
            using (SqlConnection conn = new SqlConnection(connDB))
            {
                conn.Open();
                string query = "SELECT ViajeID, VehiculoID, ConductorID, Ruta, FechaSalida, FechaLlegada, Estado FROM Viajes";
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                return dt;
            }
        }

        public DataTable BuscarViajeDataTablePorId(int viajeId)
        {
            using (SqlConnection conn = new SqlConnection(connDB))
            {
                string query = "SELECT * FROM Viajes WHERE ViajeID = @ViajeID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ViajeID", viajeId);
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        return dt;
                    }
                }
            }
        }

        public Viajes BuscarViajePorId(int viajeId)
        {
            using (SqlConnection conn = new SqlConnection(connDB))
            {
                conn.Open();
                string query = "SELECT * FROM Viajes WHERE ViajeID = @ViajeID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ViajeID", viajeId);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Viajes
                            {
                                ViajeID = (int)reader["ViajeID"],
                                VehiculoID = (int)reader["VehiculoID"],
                                ConductorID = (int)reader["ConductorID"],
                                Ruta = reader["Ruta"].ToString(),
                                FechaSalida = Convert.ToDateTime(reader["FechaSalida"]),
                                FechaLlegada = Convert.ToDateTime(reader["FechaLlegada"]),
                                Estado = reader["Estado"].ToString()
                            };
                        }
                    }
                }
            }
            return null;
        }

        public bool ViajeIdExistente(int id)
        {
            using (SqlConnection conn = new SqlConnection(connDB))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Viajes WHERE ViajeID = @id", conn);
                cmd.Parameters.AddWithValue("@id", id);
                int count = (int)cmd.ExecuteScalar();
                return count > 0;
            }
        }
    }
}
