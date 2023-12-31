﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp7;

namespace Viktoryna_Pytymko_V.L
{
    public class Viktoryna:IDisposable
    {
        public static string UsersFilePath = "users.txt";
        public static string QuestionsFilePath = "question.txt";
        public static string ResultsFilePath = "results.txt";

            public static List<User> Users;
            public static List<Question> Questions;
            public static List<Result> Results;
            public User currentUser;

            public Viktoryna()
            {
            DataLoader.LoadData();

           
            AppDomain.CurrentDomain.ProcessExit += (sender, args) => SaveData();
            }

            public void Dispose()
            {
                // Збереження даних при виході з програми
                SaveData();
            }

           
            public void SaveData()
            {

                try
                {
                    File.WriteAllLines(UsersFilePath, Users.Select(u => u.ToString()));
                    File.WriteAllLines(ResultsFilePath, Results.Select(r => r.ToString()));
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Виникла помилка при записі даних у файл: {ex.Message}");

                }
            }

            public void Run()
            {
                Console.WriteLine("Ласкаво просимо до вікторини!");

                while (true)
                {
                    Console.WriteLine("1. Log in");
                    Console.WriteLine("2. Sign up");
                    Console.WriteLine("3. Exit");

                    int choice = GetChoice(1, 3);
                    switch (choice)
                    {
                        case 1:
                            Console.Clear(); // Очистка консолі перед викликом методу Login()
                            Login();
                            break;
                        case 2:
                            Console.Clear(); // Очистка консолі перед викликом методу Register()
                            Register();
                            break;
                        case 3:
                            SaveData();
                            Console.Clear(); // Очистка консолі перед виведенням прощального повідомлення
                            Console.WriteLine("Дякуємо за гру. До побачення!");
                            return;
                    }
                }
            }

            private void Login()
        {
            Console.WriteLine("Введіть логін: ");
            string login = Console.ReadLine();
            Console.WriteLine("Введіть пароль: ");
            string password = Console.ReadLine();
            User user = Users.Find(u => u.Login == login && u.Password == password);
            if (user != null)
            {
                currentUser = user;
                Console.WriteLine($"З поверненням! {currentUser.Login}");
                MainMenu();
            }
            else { Console.WriteLine("Неправильний логін чи пароль."); };
        }

            private void MainMenu()
            {
                while (true)
                {

                    Console.WriteLine("\n Головне меню:");
                    Console.WriteLine("1.Почату вікторину ");
                    Console.WriteLine("2.Перелянути результати ");
                    Console.WriteLine("3.Топ 20 ");
                    Console.WriteLine("4.Змінити налаштування ");
                    Console.WriteLine("5.Вийти ");

                    int choise = GetChoice(1, 5);

                    switch (choise)
                    {
                        case 1:
                        Console.Clear();
                        StartVictoryna();
                            break;
                        case 2:
                        Console.Clear();
                        VievResults();
                            break;
                        case 3:
                            Console.Clear();
                            VievTop20();
                            break;
                        case 4:
                            Console.Clear();
                            ChangeSettings();
                            break;
                        case 5:
                            Console.Clear();
                            SaveData();
                            Console.WriteLine("Дякуємо за гру. До побачення!");
                            return;
                    }
                }
            }

        private void VievTop20()
        {

            Console.WriteLine("\n Оберіть розділ вікторини:");

            List<string> uniqueCategories = Questions.Select(q => q.Category).Distinct().ToList();

            for (int i = 0; i < uniqueCategories.Count; i++)
            {
                Console.WriteLine($"{i + 1}.{uniqueCategories[i]}");
            }

            int categoryChoise = GetChoice(1, Questions.Count);
            var topResults = Results.Where(r => r.Category == Questions[categoryChoise - 1].Category)
                .OrderByDescending(r => r.CorrectAnswersCount)
                .Take(20);
            Console.WriteLine($"\nТоп-20 гравців у розділі '{Questions[categoryChoise - 1].Category}':");
            int position = 1;
            foreach (var result in topResults)
            {
                Console.WriteLine($"{position}.{result.UserName},Вірних відповідей: {result.CorrectAnswersCount} ");
                position++;

                result.CorrectAnswersCount++;

            }
        }


        private void VievResults()
        {
            Console.WriteLine("\nРезультати вікторини");

            var userResults = Results.Where(r => r.UserName == currentUser.Login);

            foreach (var uniqueCategory in userResults.Select(r => r.Category).Distinct())
            {
                var resultsInCategory = userResults.Where(r => r.Category == uniqueCategory);

                int totalQuestionsInCategory = resultsInCategory.First().TotalQuestions;
                int correctAnswersCountInCategory = resultsInCategory.Sum(r => r.CorrectAnswersCount);

                Console.WriteLine($" Розділ :{uniqueCategory}, вірних відповідей : {correctAnswersCountInCategory} з {totalQuestionsInCategory}");
            }
        }

        private static readonly Random random = new Random();



        private void StartVictoryna()
        {
            Console.WriteLine("\n Оберіть розділ вікторини");

            List<string> uniqueCategories = Questions.Select(q => q.Category).Distinct().ToList();
            int totalQuestions = Questions.Count;

            for (int i = 0; i < uniqueCategories.Count; i++)
            {
                Console.WriteLine($"{i + 1}.{uniqueCategories[i]}");
            }

            int categoryChoice = GetChoice(1, uniqueCategories.Count);
            string selectedCategory = uniqueCategories[categoryChoice - 1];

            var selectedQuestions = Questions.Where(q => q.Category == selectedCategory).ToList();

            do { 
                  var randomQuestion = selectedQuestions[random.Next(selectedQuestions.Count)];
                 
                  Console.WriteLine($"\n Початок вікторини з розділу '{selectedCategory}'...");
                  Console.WriteLine($"\n{randomQuestion.Text}");
                 
                 
                 
                  foreach (var option in randomQuestion.Options)
                  {
                      Console.WriteLine($"{option.Key}");
                  }
                 
                  Console.WriteLine("Ваш вибір (A, B, C): ");
                  string userResponse = Console.ReadLine()?.ToUpper();
                  List<string> userAnswers = new List<string> { userResponse };
                 
                  int correctAnswersCount = CalculaterCorrectAnswers(randomQuestion, userAnswers);
                   Console.WriteLine($"\n Ви відповіли вірно на {correctAnswersCount} з 1 питання");
                 
                      Console.WriteLine("\n1. Наступне питання");
                      Console.WriteLine("2. Вийти до Головного Меню");
                 
                      int menuChoice = GetChoice(1, 2);
                 
                      switch(menuChoice)
                      {
                          case 1:
                              // Продовжити виконання циклу для наступного питання
                              break;
                         
                          case 2:
                              // Вийти до Головного Меню
                              Console.Clear(); // Очистити консоль перед виведенням нового меню
                              MainMenu();
                              return;
                      }
                Results.Add(new Result(currentUser.Login, selectedCategory, correctAnswersCount, totalQuestions));
                SaveData();
            } while (true) ;

           

        }
        

        private int CalculaterCorrectAnswers(Question question, List<string> userAnswers)
        {
            int correctAnswerCount = 0;

            var correctAnswers = question.Options.Where(option => option.Value == true).Select(option => option.Key.Split(':')[0].Trim());

            foreach (var userAnswer in userAnswers)
            {
                if (correctAnswers.Any(a => string.Equals(a, userAnswer.Trim(), StringComparison.OrdinalIgnoreCase)))
                {
                    correctAnswerCount++;
                    break;
                }
            }

            return correctAnswerCount;
        }

        private void Register()
            {
                Console.WriteLine("Введіть логін: ");
                string login = Console.ReadLine();
                if (Users.Any(u => u.Login == login))
                {
                    Console.WriteLine("Цей логін вже існує. Виберіть інший.");
                    return;
                }

                Console.WriteLine("Введіть пароль: ");
                string password = Console.ReadLine();
                Console.WriteLine("Введіть дату народження (рррр-мм-дд): ");
                string birthDate = Console.ReadLine();

                User newUser = new User(login, password, birthDate);
                Users.Add(newUser);
                currentUser = newUser;

                Console.WriteLine($"Реєстрація пройшла успішно,{currentUser.Login}");

            }

            private void ChangeSettings()
            {
                Console.WriteLine("\nЗміна налаштувань :");
                Console.WriteLine("1.Змінити пароль");
                Console.WriteLine("2.Змінити дату народження");
                Console.WriteLine("3.Повернутися");
                int choise = GetChoice(1, 3);
                switch (choise)
                {
                    case 1:
                        Console.Write("Введить новий пароль: ");
                        string newPassword = Console.ReadLine();
                        currentUser.Password = newPassword;
                        Console.WriteLine("Пароль змінено успішно.");
                        break;
                    case 2:
                        Console.Write("Введіть нову дату народження (ррр-мм-дд):");
                        string newBirthDate = Console.ReadLine();
                        currentUser.BirthDate = newBirthDate;
                        Console.WriteLine("Дату народження змінено успішно.");
                        break;
                }
            }

            private int GetChoice(int minValue, int maxValue)
            {
                int choice;
                while (true)
                {
                    Console.Write($"Будь ласка, введіть число від {minValue} до {maxValue}: ");
                    string input = Console.ReadLine();

                    if (int.TryParse(input, out choice))
                    {
                        if (choice >= minValue && choice <= maxValue)
                        {
                            break; 
                        }
                        else
                        {
                            Console.WriteLine($"Будь ласка, введіть число від {minValue} до {maxValue}.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Будь ласка, введіть коректне число.");
                    }
                }
                return choice;
            }

          
    }
}
