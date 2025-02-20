﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics.Metrics;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using WAT.Recursos.utils;
using static WAT.site;

namespace WAT.Pages
{
    public partial class PerfilTalento : System.Web.UI.Page
    {
        private site master;
        string connectionStringWat_db = ConfigurationManager.ConnectionStrings["wat_db"].ConnectionString;
        private static List<Perfil> userProfiles = new List<Perfil>();
        protected void Page_Load(object sender, EventArgs e)
        {
            master = (site)this.Master;
            if (!IsPostBack && Session["UsuarioID"] != null)
            {
                using (MySqlConnection conn = new MySqlConnection(connectionStringWat_db))
                {
                    conn.Open();
                    var user_id = Session["UsuarioID"].ToString();
                    using (MySqlCommand cmd = new MySqlCommand("sp_LeerPerfilUsuario", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_user_id", user_id);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read() && !reader.IsDBNull(0))
                            {
                                Perfil perfil = new Perfil()
                                {
                                    PerfilId = reader["perfil_id"].ToString(),
                                    UsuarioId = user_id,
                                    Username = reader["nombre_perfil"].ToString(),
                                    Email = reader["email"].ToString(),
                                    Biografia = reader["biografia"].ToString(),
                                    TrabajoActual = "Desarrollador de Software",
                                    Ciudad = "Bogotá",
                                    Educacion = "Ingeniero de Sistemas",
                                    ProyectoActual = "Proyecto XYZ",
                                    FotoPerfil = "~/Recursos/FotoPerfil.jpg",
                                    BannerPerfil = "~/Recursos/paisaje-800x409.jpg"
                                };

                                userProfiles.Add(perfil);
                                DisplayProfile(perfil);
                            }
                            else
                            {
                                master.ShowError("Correo o contraseña incorrectos.");
                            }
                        }
                    }
                }
            }
        }

        private void DisplayProfile(Perfil user)
        {
            imgProfilePicture.ImageUrl = user.FotoPerfil ?? "~/Images/default-profile.png";
            imgBanner.ImageUrl = user.BannerPerfil ?? "~/Images/default-banner.jpg";
            lblUsername.Text = user.Username;
            lblDescription.Text = user.Biografia;
            lblTrabajoActual.Text = $"{user.TrabajoActual}";
            lblCiudad.Text = $"{user.Ciudad}";
            lblEducacion.Text = $"{user.Educacion}";
            lblProyectoActual.Text = $"{user.ProyectoActual}";
        }

        //protected void btnUploadProfilePicture_Click(object sender, EventArgs e)
        //{
        //    if (fuProfilePicture.HasFile)
        //    {
        //        string fileName = Path.GetFileName(fuProfilePicture.PostedFile.FileName);
        //        string filePath = "~/Images/" + fileName;
        //        fuProfilePicture.SaveAs(Server.MapPath(filePath));

        //        Perfil currentUser = userProfiles.Find(user => user.IdUsuario == "user123");
        //        if (currentUser != null)
        //        {
        //            currentUser.FotoPerfil = filePath;
        //            DisplayProfile(currentUser);
        //        }
        //    }
        //}

        //protected void btnUploadBanner_Click(object sender, EventArgs e)
        //{
        //    if (fuBannerImage.HasFile)
        //    {
        //        string fileName = Path.GetFileName(fuBannerImage.PostedFile.FileName);
        //        string filePath = "~/Images/" + fileName;
        //        fuBannerImage.SaveAs(Server.MapPath(filePath));

        //        Perfil currentUser = userProfiles.Find(user => user.IdUsuario == "user123");
        //        if (currentUser != null)
        //        {
        //            currentUser.BannerPerfil = filePath;
        //            DisplayProfile(currentUser);
        //        }
        //    }
        //}

        protected void btnSaveChanges_Click(object sender, EventArgs e)
        {
            Perfil currentUser = userProfiles.Find(user => user.UsuarioId == Session["UsuarioID"].ToString());
            if (currentUser != null)
            {
                currentUser.Biografia = txtDescripcion.Text;
                currentUser.Ciudad = txtCiudad.Text;
                currentUser.TrabajoActual = txtTrabajoActual.Text;
                currentUser.ProyectoActual = txtProyectoActual.Text;

                using (MySqlConnection conn = new MySqlConnection(connectionStringWat_db))
                {
                    conn.Open();

                    using (MySqlCommand cmd = new MySqlCommand("sp_ActualizarPerfilUsuario", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_perfil_id", currentUser.PerfilId);
                        cmd.Parameters.AddWithValue("@p_nombre_perfil", currentUser.Nombre);
                        cmd.Parameters.AddWithValue("@p_descripcion", currentUser.Biografia);
                        cmd.Parameters.AddWithValue("@p_foto_perfil", currentUser.FotoPerfil);
                        cmd.Parameters.AddWithValue("@p_direccion", currentUser.Ciudad);
                        cmd.Parameters.AddWithValue("@p_biografia", currentUser.Biografia);
                        cmd.Parameters.AddWithValue("@p_fecha_nacimiento", DateTime.Now);
                        cmd.ExecuteNonQuery();

                        master.ShowSuccess("¡Actualizacion exitosa!");
                    }
                }

                //if (fuProfilePicture.HasFile)
                //{
                //    string profileFileName = Path.GetFileName(fuProfilePicture.PostedFile.FileName);
                //    string profileFilePath = "~/Images/" + profileFileName;
                //    fuProfilePicture.SaveAs(Server.MapPath(profileFilePath));
                //    currentUser.FotoPerfil = profileFilePath;
                //}

                //if (fuBannerImage.HasFile)
                //{
                //    string bannerFileName = Path.GetFileName(fuBannerImage.PostedFile.FileName);
                //    string bannerFilePath = "~/Images/" + bannerFileName;
                //    fuBannerImage.SaveAs(Server.MapPath(bannerFilePath));
                //    currentUser.BannerPerfil = bannerFilePath;
                //}

                DisplayProfile(currentUser);
            }
        }
    }
}
