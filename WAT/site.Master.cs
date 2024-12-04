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
            if (!IsPostBack)
            {
                if (Session["Usuario"] == null && !Request.Url.ToString().Contains("login.aspx"))
                {
                    Response.Redirect("login.aspx");
                }
            }

            //if (Session["Rol"].ToString() == "1")
            //{

            //}

            using (MySqlConnection conexion = new MySqlConnection(connectionStringWat_db))            
            { 
            
            }
        }

        public void RedirectWithAlert(string url, string message, MessageType type, int delayMilliseconds = 5000)
        {
            string alertType = type.ToString().ToLower();

            string script = $@"
                showAlert('{message}', '{alertType}');
                setTimeout(function() {{
                    window.location.href = '{url}';
                }}, {delayMilliseconds});
            ";

            ScriptManager.RegisterStartupScript(this, GetType(), "redirectWithAlert", script, true);
        }

        public void ShowError(string message)
        {
            Show(MessageType.danger, message);
        }

        public void ShowInfo(string message)
        {
            Show(MessageType.info, message);
        }

        public void ShowSuccess(string message)
        {
            Show(MessageType.success, message);
        }

        public void ShowWarning(string message)
        {
            Show(MessageType.warning, message);
        }

        private void Show(MessageType messageType, string message)
        {
            string script = $"showAlert('{message}', '{messageType}');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowAlert", script, true);
        }

        public enum MessageType
        {
            danger = 1,
            info = 2,
            success = 3,
            warning = 4
        }

    }
}