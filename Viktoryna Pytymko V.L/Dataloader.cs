// DataLoader.cs
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Viktoryna_Pytymko_V.L;

namespace ConsoleApp7
{
    public static class DataLoader
    {
        public static void LoadData()
        {
            try
            {
                if (File.Exists(Viktoryna.UsersFilePath))
                {
                    Viktoryna.Users = File.ReadAllLines(Viktoryna.UsersFilePath)
                        .Select(line => User.FromString(line))
                        .Where(user => user != null)
                        .ToList();
                }
                else
                {
                    Console.WriteLine($"Файл {Viktoryna.UsersFilePath} не знайдено. Створіть файл або зареєструйте нового користувача.");
                }

                if (File.Exists(Viktoryna.QuestionsFilePath))
                {
                    Viktoryna.Questions = File.ReadAllLines(Viktoryna.QuestionsFilePath)
                        .Select(line => Question.FromString(line))
                        .Where(question => question != null)
                        .ToList();
                }
                else
                {
                    Console.WriteLine($"Файл {Viktoryna.QuestionsFilePath} не знайдено. Створіть файл або додайте питання.");
                }

                if (File.Exists(Viktoryna.ResultsFilePath))
                {
                    Viktoryna.Results = File.ReadAllLines(Viktoryna.ResultsFilePath)
                        .Select(line => Result.FromString(line))
                        .Where(result => result != null)
                        .ToList();
                }
                else
                {
                    Viktoryna.Results = new List<Result>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Виникла помилка при читанні даних з файлу: {ex.Message}");
            }
        }
    }
}
