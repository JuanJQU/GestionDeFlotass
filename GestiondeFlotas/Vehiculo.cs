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
    class Vehiculo
    {
        public int VehiculoID { get; set; }
        public string Matricula { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string tipo { get; set; }
        public int Capacidad { get; set; }
        public string Estado { get; set; }

        public Vehiculo() { }
        public Vehiculo(int vehiculoID, string matricula, string marca, string modelo, string tipo, int capacidad, string estado)
        {
            VehiculoID = vehiculoID;
            Matricula = matricula;
            Marca = marca;
            Modelo = modelo;
            this.tipo = tipo;
            Capacidad = capacidad;
            Estado = estado;
        }
        private string connDB = "Data Source=.\\SQLEXPRESS;Initial Catalog=GestionFlotas;Integrated Security=True;";
        public string ConnDB => connDB;

        public void AgregarVehiculo(Vehiculo vehiculo)
        {
            using (var conn = new System.Data.SqlClient.SqlConnection(vehiculo.ConnDB))
            {
                conn.Open();
                var cmd = new System.Data.SqlClient.SqlCommand
                (
                    "INSERT INTO Vehiculos (VehiculoID,Matricula, Marca, Modelo, Tipo, Capacidad, Estado) " +
                    "VALUES (@VehiculoID,@Matricula, @Marca, @Modelo, @Tipo, @Capacidad, @Estado)", conn
                );
                cmd.Parameters.AddWithValue("@VehiculoID", vehiculo.VehiculoID);
                cmd.Parameters.AddWithValue("@Matricula", vehiculo.Matricula);
                cmd.Parameters.AddWithValue("@Marca", vehiculo.Marca);
                cmd.Parameters.AddWithValue("@Modelo", vehiculo.Modelo);
                cmd.Parameters.AddWithValue("@Tipo", vehiculo.tipo);
                cmd.Parameters.AddWithValue("@Capacidad", vehiculo.Capacidad);
                cmd.Parameters.AddWithValue("@Estado", vehiculo.Estado);

                cmd.ExecuteNonQuery();
            }
        }

        public void ActualizarVehiculo(Vehiculo vehiculo, string connDB)
        {
            using (var conn = new SqlConnection(connDB))
            {
                conn.Open();
                var cmd = new SqlCommand(
                    "UPDATE Vehiculos SET Matricula = @Matricula, Marca = @Marca, Modelo = @Modelo, Tipo = @Tipo, Capacidad = @Capacidad, Estado = @Estado WHERE VehiculoID = @VehiculoID",
                    conn
                );
                cmd.Parameters.AddWithValue("@VehiculoID", vehiculo.VehiculoID);
                cmd.Parameters.AddWithValue("@Matricula", vehiculo.Matricula);
                cmd.Parameters.AddWithValue("@Marca", vehiculo.Marca);
                cmd.Parameters.AddWithValue("@Modelo", vehiculo.Modelo);
                cmd.Parameters.AddWithValue("@Tipo", vehiculo.tipo);
                cmd.Parameters.AddWithValue("@Capacidad", vehiculo.Capacidad);
                cmd.Parameters.AddWithValue("@Estado", vehiculo.Estado);
                cmd.ExecuteNonQuery();
            }
        }

        public bool EliminarVehiculo(int vehiculoId)
        {
            try
            {
                string connDB = "Data Source=.\\SQLEXPRESS;Initial Catalog=GestionFlotas;Integrated Security=True;";

                using (SqlConnection conn = new SqlConnection(connDB))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("DELETE FROM Vehiculos WHERE VehiculoID = @VehiculoID", conn);
                    cmd.Parameters.AddWithValue("@VehiculoID", vehiculoId);
                    int rows = cmd.ExecuteNonQuery();

                    return rows > 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar el vehículo: " + ex.Message, "Error");
                return false;
            }
        }


        public void ListarVehiculos(Vehiculo vehiculo)
        {
            using (var conn = new System.Data.SqlClient.SqlConnection(vehiculo.ConnDB))
            {
                conn.Open();
                var cmd = new System.Data.SqlClient.SqlCommand
                (
                    "SELECT * FROM Vehiculos", conn
                );
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"ID: {reader["VehiculoID"]}, Matricula: {reader["Matricula"]}, Marca: {reader["Marca"]}, Modelo: {reader["Modelo"]}, Tipo: {reader["Tipo"]}, Capacidad: {reader["Capacidad"]}, Estado: {reader["Estado"]}");
                    }
                }
            }
        }

        public void BuscarVehiculoPorID(Vehiculo vehiculo, int vehiculoID)
        {
            using (var conn = new System.Data.SqlClient.SqlConnection(vehiculo.ConnDB))
            {
                conn.Open();
                var cmd = new System.Data.SqlClient.SqlCommand
                (
                    "SELECT * FROM Vehiculos WHERE VehiculoID = @VehiculoID", conn
                );
                cmd.Parameters.AddWithValue("@VehiculoID", vehiculoID);
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Console.WriteLine($"ID: {reader["VehiculoID"]}, Matricula: {reader["Matricula"]}, Marca: {reader["Marca"]}, Modelo: {reader["Modelo"]}, Tipo: {reader["Tipo"]}, Capacidad: {reader["Capacidad"]}, Estado: {reader["Estado"]}");
                    }
                    else
                    {
                        Console.WriteLine("Vehículo no encontrado.");
                    }
                }
            }

        }

        public bool VehiculoIdExistente(int id)
        {
            using (SqlConnection conn = new SqlConnection(ConnDB))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Vehiculos WHERE VehiculoID = @id", conn);
                cmd.Parameters.AddWithValue("@id", id);
                int count = (int)cmd.ExecuteScalar();
                return count > 0;
            }
        }

        internal object GetAllVehiculos()
        {
            using (SqlConnection conn = new SqlConnection(connDB))
            {
                conn.Open();
                // Selecciona solo las columnas necesarias (ajusta según tu modelo)
                string query = "SELECT VehiculoID, Matricula,Marca, Modelo, Tipo, Capacidad,Estado FROM Vehiculos";
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                return dt;
            }
        }

        //Obtener vehiculo por VehiculoID
        public Vehiculo BuscarVehiculoPorId(int vehiculoId)
        {
            using (SqlConnection conn = new SqlConnection(connDB))
            {
                conn.Open();
                string query = "SELECT * FROM Vehiculos WHERE VehiculoID = @VehiculoID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@VehiculoID", vehiculoId);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Vehiculo
                            {
                                VehiculoID = (int)reader["VehiculoID"],
                                Matricula = reader["Matricula"].ToString(),
                                Marca = reader["Marca"].ToString(),
                                Modelo = reader["Modelo"].ToString(),
                                tipo = reader["Tipo"].ToString(),
                                Capacidad = (int)reader["Capacidad"],
                                Estado = reader["Estado"].ToString()
                            };
                        }
                    }
                }
            }
            return null; // Si no se encuentra el vehículo

        }

        public DataTable BuscarVehiculoDataTablePorId(int vehiculoId)
        {
            using (SqlConnection conn = new SqlConnection(connDB))
            {
                string query = "SELECT * FROM Vehiculos WHERE VehiculoID = @VehiculoID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@VehiculoID", vehiculoId);
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        return dt;
                    }
                }
            }
        }


        //obtener datos de vehiculos
        public DataTable ObtenerDatosVehiculos()
        {
            using (SqlConnection conn = new SqlConnection(connDB))
            {
                conn.Open();
                string query = "SELECT VehiculoID, Matricula, Marca, Modelo, Tipo, Capacidad, Estado FROM Vehiculos";
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                return dt;
            }
        }
    }
}

