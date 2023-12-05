using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Viktoryna_Pytymko_V.L
{
    public class Result
    {
        public string UserName { get; set; }
        public string Category { get; set; }
        public int CorrectAnswersCount { get; set; }

        public Result(string userName, string category, int correnAnserCount)
        {
            UserName = userName;
            Category = category;
            CorrectAnswersCount = correnAnserCount;
        }
        //public static Result FromString(string data)
        //{
        //    string[] parts = data.Split(',');
        //    return new Result(parts[0], parts[1], int.Parse(parts[2]));
        //}

        public static Result FromString(string data)
        {
            string[] parts = data.Split(',');
            if (parts.Length == 3 && int.TryParse(parts[2], out int correctAnswersCount))
            {
                return new Result(parts[0], parts[1], correctAnswersCount);
            }
            else
            {
                // Обробка помилки, можливо, повернення null або іншої логіки за вашим вибором.
                return null;
            }
        }

        public override string ToString()
        {
            return $"{UserName},{Category},{CorrectAnswersCount}";
        }
    }
}
