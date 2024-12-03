using System;
using System.Security.Cryptography;
using System.Text;

namespace WAT.Recursos.utils
{
    public class PasswordHasher
    {
        /// <summary>
        /// Genera un hash seguro utilizando SHA-512 y sal.
        /// </summary>
        /// <param name="password">La contraseña a hashear.</param>
        /// <returns>Un string en Base64 que contiene el hash y la sal.</returns>
        public static string HashPassword(string password)
        {
            // Generar una sal segura de 16 bytes
            byte[] salt = new byte[16];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            // Combinar la contraseña con la sal y generar el hash
            using (var sha512 = SHA512.Create())
            {
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                byte[] passwordWithSalt = new byte[salt.Length + passwordBytes.Length];

                Buffer.BlockCopy(salt, 0, passwordWithSalt, 0, salt.Length);
                Buffer.BlockCopy(passwordBytes, 0, passwordWithSalt, salt.Length, passwordBytes.Length);

                byte[] hashBytes = sha512.ComputeHash(passwordWithSalt);

                // Combinar la sal y el hash en un solo array
                byte[] hashWithSaltBytes = new byte[salt.Length + hashBytes.Length];
                Buffer.BlockCopy(salt, 0, hashWithSaltBytes, 0, salt.Length);
                Buffer.BlockCopy(hashBytes, 0, hashWithSaltBytes, salt.Length, hashBytes.Length);

                // Convertir a Base64 para almacenamiento
                return Convert.ToBase64String(hashWithSaltBytes);
            }
        }

        /// <summary>
        /// Verifica si una contraseña coincide con un hash almacenado.
        /// </summary>
        /// <param name="password">La contraseña a verificar.</param>
        /// <param name="storedHash">El hash almacenado que incluye la sal.</param>
        /// <returns>True si la contraseña coincide; de lo contrario, False.</returns>
        public static bool VerifyPassword(string password, string storedHash)
        {
            // Convertir el hash almacenado de Base64 a bytes
            byte[] hashWithSaltBytes = Convert.FromBase64String(storedHash);

            // Extraer la sal (primeros 16 bytes)
            byte[] salt = new byte[16];
            Buffer.BlockCopy(hashWithSaltBytes, 0, salt, 0, salt.Length);

            // Extraer el hash (restantes bytes)
            byte[] storedHashBytes = new byte[hashWithSaltBytes.Length - salt.Length];
            Buffer.BlockCopy(hashWithSaltBytes, salt.Length, storedHashBytes, 0, storedHashBytes.Length);

            // Hashear la contraseña ingresada con la sal almacenada
            using (var sha512 = SHA512.Create())
            {
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                byte[] passwordWithSalt = new byte[salt.Length + passwordBytes.Length];

                Buffer.BlockCopy(salt, 0, passwordWithSalt, 0, salt.Length);
                Buffer.BlockCopy(passwordBytes, 0, passwordWithSalt, salt.Length, passwordBytes.Length);

                byte[] hashBytes = sha512.ComputeHash(passwordWithSalt);

                // Comparar los hashes byte por byte
                for (int i = 0; i < storedHashBytes.Length; i++)
                {
                    if (storedHashBytes[i] != hashBytes[i])
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}