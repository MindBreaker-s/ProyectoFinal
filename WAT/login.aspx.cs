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
using static WAT.site;
using System.Diagnostics.Metrics;

namespace WAT
{
    public partial class login : Page
    {
        private site master;
        string connectionStringWat_db = ConfigurationManager.ConnectionStrings["wat_db"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            master = (site)this.Master;
            if (!IsPostBack)
                hfFormState.Value = "login";
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "SetFormState", $"toggleForms('{hfFormState.Value}');", true);
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtLoginEmail.Text) || string.IsNullOrWhiteSpace(txtLoginPassword.Text))
            {
                master.ShowSuccess("Por favor, ingresa tu correo y contraseña.");
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
                        cmd.Parameters.AddWithValue("@p_rol_id", ddlRol.SelectedValue);
                        cmd.Parameters.AddWithValue("@p_email", txtLoginEmail.Text);
                        cmd.Parameters.AddWithValue("@p_contrasena_hash", hashedPassword);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read() && !reader.IsDBNull(0))
                            {
                                Session["UsuarioID"] = reader["user_id"];
                                Session["Nombre"] = reader["nombre_completo"];
                                Session["Rol"] = reader["rol_id"];

                                if (Session["Rol"].ToString() == "1")
                                    master.RedirectWithAlert("Pages/Perfil/PerfilTalento.aspx", "Inicio de sesión exitoso. Redirigiendo...", MessageType.success);
                                else if (Session["Rol"].ToString() == "2")
                                    master.RedirectWithAlert("Pages/Perfil/PerfilEmpresa.aspx", "Inicio de sesión exitoso. Redirigiendo...", MessageType.success);
                                else
                                    master.RedirectWithAlert("index.html", "Inicio de sesión exitoso. Redirigiendo...", MessageType.success);
                            }
                            else
                            {
                                master.ShowError("Correo o contraseña incorrectos.");
                            }
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                master.ShowError($"Error al conectar con la base de datos: {ex.Message}");
            }
            catch (Exception ex)
            {
                master.ShowError($"Ocurrió un error inesperado: {ex.Message}");
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

                master.ShowWarning("Por favor, completa todos los campos.");
                return;
            }

            if (txtPassword.Text != txtConfirmPassword.Text)
            {
                master.ShowWarning("Las contraseñas no coinciden.");
                return;
            }

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionStringWat_db))
                {
                    conn.Open();
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

                        master.ShowSuccess("¡Registro exitoso! Ahora puedes iniciar sesión.");
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "SetFormState", $"toggleForms('login');", true);
                    }
                }
            }
            catch (MySqlException ex)
            {
                if (ex.Number == 1062)
                {
                    master.ShowError($"El correo electrónico o número de identificación ya están registrados.");
                }
                else
                {
                    master.ShowError($"Ocurrió un error al registrar el usuario. Por favor, inténtalo de nuevo.");
                }
            }
            catch (Exception ex)
            {
                master.ShowError($"Ocurrió un error inesperado: {ex.Message}");
            }
        }
    }
}