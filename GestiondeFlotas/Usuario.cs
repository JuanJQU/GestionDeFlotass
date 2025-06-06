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
    class Usuario
    {
        public int UsuarioID { get; set; }
        public string Rol { get; set; }
        public string Nombre { get; set; }          // Solo para conductores
        public string Licencia { get; set; }        // Solo para conductores
        public string Telefono { get; set; }        // Solo para conductores
        public string Email { get; set; }           // Para ambos
        public byte[] PasswordHash { get; set; }


        private string connDB = "Data Source=.\\SQLEXPRESS;Initial Catalog=GestionFlotas;Integrated Security=True;";
        public string ConnDB => connDB;

        internal Usuario ObtenerDatosUsuario(int usuarioId)
        {
            using (SqlConnection conn = new SqlConnection(connDB))
            {
                conn.Open();
                string query = @"
            SELECT 
                u.UsuarioID,
                u.Rol,
                u.PasswordHash,
                c.Nombre,
                c.Licencia,
                c.Telefono,
                c.Email
            FROM Usuarios u
            LEFT JOIN Conductores c ON u.UsuarioID = c.ConductorID
            WHERE u.UsuarioID = @usuarioId";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@usuarioId", usuarioId);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Usuario
                            {
                                UsuarioID = (int)reader["UsuarioID"],
                                Rol = reader["Rol"].ToString(),
                                PasswordHash = (byte[])reader["PasswordHash"],
                                Nombre = reader["Nombre"] as string,
                                Licencia = reader["Licencia"] as string,
                                Telefono = reader["Telefono"] as string,
                                Email = reader["Email"] as string
                            };
                        }
                    }
                }
            }

            return null;
        }

        public bool UsuarioExistente(int usuarioId)
        {
            bool existe = false;
            using (SqlConnection conn = new SqlConnection(connDB))
            {
                string query = "SELECT COUNT(*) FROM Usuarios WHERE UsuarioID = @usuarioId";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@usuarioId", usuarioId);
                    conn.Open();
                    int count = (int)cmd.ExecuteScalar();
                    existe = count > 0;
                }
                return existe;
            }
        }

        public long GetAccountIdByUserId(int ConductorID)
        {
            using (SqlConnection conn = new SqlConnection(connDB))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("SELECT ConductorID FROM Conductores WHERE ConductorID = @ConductorID", conn);
                cmd.Parameters.AddWithValue("@ConductorID", ConductorID);
                object result = cmd.ExecuteScalar();
                if (result != null)
                {
                    return (long)result;
                }
                else
                {
                    throw new Exception("No se encontró una cuenta para este usuario.");
                }
            }
        }

        internal object GetAllUsers()
        {
            using (SqlConnection conn = new SqlConnection(connDB))
            {
                conn.Open();
                // Selecciona solo las columnas necesarias (ajusta según tu modelo)
                string query = "SELECT ConductorID, Nombre, Licencia, Email, Telefono,PasswordHash FROM Conductores";
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                return dt;
            }
        }

        public DataTable BuscarUsuarioPorId(int ConductorID)
        {
            using (SqlConnection conn = new SqlConnection(connDB))
            {
                conn.Open();
                string query = "SELECT ConductorID, Nombre, Email, Telefono, PasswordHash, Licencia FROM Conductores WHERE ConductorID = @ConductorID";
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                adapter.SelectCommand.Parameters.AddWithValue("@ConductorID", ConductorID);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                return dt;
            }
        }

        public bool EliminarConductor(int ConductorID)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connDB))
                {
                    conn.Open();

                    //// Elimina primero las cuentas asociadas (si existen)
                    //SqlCommand cmdAccounts = new SqlCommand("DELETE FROM Accounts WHERE userId = @userId", conn);
                    //cmdAccounts.Parameters.AddWithValue("@userId", userId);
                    //cmdAccounts.ExecuteNonQuery();

                    // Luego elimina el usuario
                    SqlCommand cmdUsuario = new SqlCommand("DELETE FROM Conductores WHERE ConductorID = @ConductorID", conn);
                    cmdUsuario.Parameters.AddWithValue("@ConductorID", ConductorID);
                    int rows = cmdUsuario.ExecuteNonQuery();

                    return rows > 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar el usuario: " + ex.Message, "Error");
                return false;
            }
        }
        public bool DatosUsuarioDuplicados(int CondcutorID, string Email, string Telefono, string Licencia, byte[] PasswordHash)
        {
            using (SqlConnection conn = new SqlConnection(connDB))
            {
                conn.Open();
                string query = @"SELECT COUNT(*) FROM Conductores 
                         WHERE ConductorID <> @ConductorID
                         AND (Email = @Email OR Telefono = @Telefono OR Licencia = @Licencia OR PasswordHash = @PasswordHash)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ConductorID", CondcutorID);
                    cmd.Parameters.AddWithValue("@Email", Email);
                    cmd.Parameters.AddWithValue("@Telefono", Telefono);
                    cmd.Parameters.AddWithValue("@Licencia", Licencia);
                    cmd.Parameters.AddWithValue("@PasswordHash", PasswordHash);
                    int count = (int)cmd.ExecuteScalar();
                    return count > 0;
                }
            }
        }
        public bool ActualizarConductor(Usuario usuario)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connDB))
                {
                    conn.Open();

                    // Si la contraseña está vacía, no la actualices
                    if (usuario.PasswordHash == null || usuario.PasswordHash.Length == 0)
                    {
                        string query = @"UPDATE Conductores 
                                 SET Nombre = @Nombre, 
                                     Email = @Email, 
                                     Telefono = @Telefono, 
                                     Licencia = @Licencia
                                 WHERE ConductorID = @ConductorID";
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                            cmd.Parameters.AddWithValue("@Email", usuario.Email);
                            cmd.Parameters.AddWithValue("@Telefono", usuario.Telefono);
                            cmd.Parameters.AddWithValue("@Licencia", usuario.Licencia);
                            cmd.Parameters.AddWithValue("@ConductorID", usuario.UsuarioID);

                            int rows = cmd.ExecuteNonQuery();
                            return rows > 0;
                        }
                    }
                    else
                    {
                        // Si la contraseña NO está vacía, actualízala (hasheada)
                        string query = @"UPDATE Conductores 
                                 SET Nombre = @Nombre, 
                                     Email = @Email, 
                                     Telefono = @Telefono, 
                                     Licencia = @Licencia,
                                     PasswordHash = @PasswordHash
                                 WHERE ConductorID = @ConductorID";
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                            cmd.Parameters.AddWithValue("@Email", usuario.Email);
                            cmd.Parameters.AddWithValue("@Telefono", usuario.Telefono);
                            cmd.Parameters.AddWithValue("@Licencia", usuario.Licencia);
                            cmd.Parameters.AddWithValue("@PasswordHash", usuario.PasswordHash);
                            cmd.Parameters.AddWithValue("@ConductorID", usuario.UsuarioID);

                            int rows = cmd.ExecuteNonQuery();
                            return rows > 0;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar los datos: " + ex.Message, "Error");
                return false;
            }
        }

    }
}
