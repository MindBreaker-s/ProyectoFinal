using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WAT
{
    public partial class site : System.Web.UI.MasterPage
    {
        string connectionStringWat_db = System.Configuration.ConfigurationManager.ConnectionStrings["wat_db"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Usuario"] != null)
            {
                Response.Redirect("Perfil.aspx");
            }

            //if (Session["Rol"].ToString() == "1")
            //{

            //}

            using (MySqlConnection conexion = new MySqlConnection(connectionStringWat_db))            
            { 
            
            }
        }

        /// <summary>
        /// Muestra un mensaje de confirmación o éxito.
        /// </summary>
        /// <param name="message">El mensaje a mostrar.</param>
        public void ShowSuccessMessage(string message)
        {
            MessagePanel.CssClass = "alert alert-success"; // Clase CSS para mensajes de éxito
            MessageText.Text = message;
            MessagePanel.Visible = true;
        }

        /// <summary>
        /// Muestra un mensaje de error.
        /// </summary>
        /// <param name="message">El mensaje a mostrar.</param>
        public void ShowErrorMessage(string message)
        {
            MessagePanel.CssClass = "alert alert-danger"; // Clase CSS para mensajes de error
            MessageText.Text = message;
            MessagePanel.Visible = true;
        }

        /// <summary>
        /// Oculta cualquier mensaje actualmente visible.
        /// </summary>
        public void HideMessage()
        {
            MessagePanel.Visible = false;
            MessageText.Text = string.Empty;
        }
    }
}