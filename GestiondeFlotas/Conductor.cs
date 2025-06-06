using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestiondeFlotas
{
    internal class Conductor
    {
        public string Nombre { get; set; }
        public string ConductorID { get; set; }
        public string Licencia { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }

       
        public Conductor(string nombre, string documento, string licencia,string telefono, string email, byte[] passwordhash)
        {
            Nombre = nombre;
            ConductorID = documento;
            Licencia = licencia;
            Telefono = telefono;
            Email = email;
            PasswordHash = passwordhash;
        }

        public void AgregarConductor(Usuario usuario)
        {
            using(SqlConnection conn = new SqlConnection(usuario.ConnDB))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand
                (
                    "INSERT INTO Conductores (Nombre, ConductorID, Licencia, Telefono, Email, PasswordHash)" +
                    "VALUES (@Nombre, @ConductorID, @Licencia, @Telefono, @Email, @PasswordHash)",conn
                );
                    cmd.Parameters.AddWithValue("@Nombre", Nombre);
                    cmd.Parameters.AddWithValue("@ConductorID", ConductorID);
                    cmd.Parameters.AddWithValue("@Licencia", Licencia);
                    cmd.Parameters.AddWithValue("@Telefono", Telefono);
                    cmd.Parameters.AddWithValue("@Email", Email);
                    cmd.Parameters.AddWithValue("@PasswordHash", PasswordHash);
                    cmd.ExecuteNonQuery();
            }
        }

        
    }
}
