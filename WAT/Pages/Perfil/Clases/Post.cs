using System;

namespace WAT.Pages
{
    public class Post
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime DatePosted { get; set; }
        public string UserId { get; set; }
        public Post(int id, string content, string userId)
        {
            this.Id = id;
            Content = content;
            DatePosted = DateTime.Now; // Establece la fecha actual como la fecha de publicación
            UserId = userId;
        }

        // Constructor vacío opcional
        public Post() { }
    }


        // Constructor para inicializar los campos
        
    }

