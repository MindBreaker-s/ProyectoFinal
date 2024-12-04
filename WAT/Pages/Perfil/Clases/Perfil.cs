using System;
using System.Collections.Generic;
using WAT.Pages;

public class Perfil
{
    public string PerfilId { get; set; }
    public string UsuarioId { get; set; }
    public string Username { get; set; }
    public string Nombre { get; set; }
    public string Apellido { get; set; }
    public string Email { get; set; }
    public string Biografia { get; set; }
    public string TrabajoActual { get; set; }
    public string Ciudad { get; set; }
    public string Educacion { get; set; }
    public string ProyectoActual { get; set; }
    public string FotoPerfil { get; set; }
    public string BannerPerfil { get; set; }
    public List<Post> Posts { get; set; }

    // Constructor para inicializar los campos
    public Perfil()
    {
        Posts = new List<Post>();
    }

    // Método para añadir un post al perfil
    public void AñadirPost(Post post)
    {
        Posts.Add(post);
    }
}
