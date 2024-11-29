using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WAT
{
    public partial class login : System.Web.UI.Page
    {
        String cadenaConexion = ConfigurationManager.ConnectionStrings['conexion'].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            using (MySqlConnection conexion = new MySqlConnection(cadenaConexion)) { }
        }
    }
}