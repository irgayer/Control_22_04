using Control_22_04.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Control_22_04.Services
{
    public class RegisterService
    {
        const int MIN_PSSWD_LEN = 6;
        public bool TryAddUser(List<User> users, out User newUser)
        {
            PasswordWriterService passwordWriter = new PasswordWriterService();
            newUser = new User();

            string usLoginStr, usPsswdStr;

            WriteLine("Новый пользователь,");
            WriteLine("Введите логин(без пробелов):");

            usLoginStr = ReadLine();
            for (int i = 0; i < users.Count; i++)
            {
                if (users[i].Login == usLoginStr)
                {
                    WriteLine("Логин уже занят!");
                    return false;
                }
            }

            if (CheckUsername(usLoginStr))
            {
                WriteLine($"Введите пароль(больше {MIN_PSSWD_LEN} символов):");
                usPsswdStr = passwordWriter.Write();

                if (CheckPassword(usPsswdStr))
                {
                    newUser.Login = usLoginStr.Trim();
                    newUser.Password = usPsswdStr;

                    return true;
                }
                else WriteLine("Пароль недостаточно длинный!");
            }
            else WriteLine("Логин пустой или содержит пробелы!");

            return false;
        }

        private bool CheckUsername(string userName)
        {
            if (!string.IsNullOrWhiteSpace(userName))
            {
                if (userName.Contains(" 1")) return false;
                return true;
            }
            return false;
        }
        private bool CheckPassword(string userPsswd)
        {
            if (!string.IsNullOrWhiteSpace(userPsswd))
            {
                if (userPsswd.Length > MIN_PSSWD_LEN)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
