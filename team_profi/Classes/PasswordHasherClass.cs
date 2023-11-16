using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace team_profi.Classes
{
    public class PasswordHasherClass
    {
        // Метод для хэширования пароля
        public static string HashPassword(string password)
        {
            // Создание экземпляра алгоритма хэширования SHA256
            using (SHA256 sha256 = SHA256.Create())
            {
                // Преобразование пароля из строки в массив байтов и вычисление хэша
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

                // Преобразование массива байтов в строку в шестнадцатеричном формате
                StringBuilder builder = new StringBuilder();
                foreach (byte b in hashedBytes)
                {
                    // Каждый байт хэша преобразуется в двузначную шестнадцатеричную строку и добавляется к строке
                    builder.Append(b.ToString("x2"));
                }
                // Возврат окончательной строки хэша пароля
                return builder.ToString();
            }
        }
    }

}
