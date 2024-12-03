using MySql.Data.MySqlClient;
using Org.BouncyCastle.Crypto.Generators;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Security.Policy;
using WAT.Recursos.utils;

namespace WAT
{
    public partial class login : System.Web.UI.Page
    {
        private site master;
        string connectionStringWat_db = System.Configuration.ConfigurationManager.ConnectionStrings["wat_db"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            master = (site)this.Master;
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtLoginEmail.Text) || string.IsNullOrWhiteSpace(txtLoginPassword.Text))
            {
                master?.ShowErrorMessage("Por favor, ingresa tu correo y contraseña.");
                return;
            }

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionStringWat_db))
                {
                    conn.Open();
                    string hashedPassword = PasswordHasher.HashPassword(txtLoginPassword.Text);

                    using (MySqlCommand cmd = new MySqlCommand("sp_AutenticarUsuario", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_email", txtLoginEmail.Text);
                        cmd.Parameters.AddWithValue("@p_hashed_password", hashedPassword);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read() && !reader.IsDBNull(0))
                            {
                                // Usuario autenticado
                                Session["UsuarioID"] = reader["user_id"];
                                Session["Nombre"] = reader["nombre"];
                                Session["Rol"] = reader["rol_id"];

                                master?.ShowSuccessMessage("Inicio de sesión exitoso. Redirigiendo...");
                                Response.Redirect("Perfil.aspx");
                            }
                            else
                            {
                                // Credenciales incorrectas
                                master?.ShowErrorMessage("Correo o contraseña incorrectos.");
                            }
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                master?.ShowErrorMessage($"Error al conectar con la base de datos: {ex.Message}");
            }
            catch (Exception ex)
            {
                master?.ShowErrorMessage($"Ocurrió un error inesperado: {ex.Message}");
            }
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFullName.Text) ||
                string.IsNullOrWhiteSpace(txtEmail.Text) ||
                string.IsNullOrWhiteSpace(txtIdNumber.Text) ||
                string.IsNullOrWhiteSpace(txtPhone.Text) ||
                string.IsNullOrWhiteSpace(txtPassword.Text) ||
                string.IsNullOrWhiteSpace(txtConfirmPassword.Text) ||
                ddlRegisterRol.SelectedValue == "")
            {

                master?.ShowErrorMessage("Por favor, completa todos los campos.");
                return;
            }

            if (txtPassword.Text != txtConfirmPassword.Text)
            {
                master?.ShowErrorMessage("Las contraseñas no coinciden.");
                return;
            }

            try
            {
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;

                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    // Generar el hash de la contraseña
                    string hashedPassword = PasswordHasher.HashPassword(txtPassword.Text);

                    using (MySqlCommand cmd = new MySqlCommand("sp_RegistrarUsuario", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_nombre_completo", txtFullName.Text);
                        cmd.Parameters.AddWithValue("@p_email", txtEmail.Text);
                        cmd.Parameters.AddWithValue("@p_numero_identificacion", txtIdNumber.Text);
                        cmd.Parameters.AddWithValue("@p_telefono", txtPhone.Text);
                        cmd.Parameters.AddWithValue("@p_rol_id", ddlRegisterRol.SelectedValue);
                        cmd.Parameters.AddWithValue("@p_contrasena_hash", hashedPassword);
                        cmd.ExecuteNonQuery();

                        master?.ShowSuccessMessage("¡Registro exitoso! Ahora puedes iniciar sesión.");
                    }
                }
            }
            catch (MySqlException ex)
            {
                if (ex.Number == 1062) // Código de error para duplicados en MySQL
                {
                    master?.ShowErrorMessage($"El correo electrónico o número de identificación ya están registrados.");
                }
                else
                {
                    master?.ShowErrorMessage($"Ocurrió un error al registrar el usuario. Por favor, inténtalo de nuevo.");
                }
            }
            catch (Exception ex)
            {
                master?.ShowErrorMessage($"Ocurrió un error inesperado: {ex.Message}");
            }
        }
    }
}