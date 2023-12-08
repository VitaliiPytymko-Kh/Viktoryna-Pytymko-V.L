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
        public int TotalQuestions { get; set; }

        public Result(string userName, string category, int correnAnserCount, int totalQuestions)
        {
            UserName = userName;
            Category = category;
            CorrectAnswersCount = correnAnserCount;
            TotalQuestions=totalQuestions;
        }


        public static Result FromString(string data)
        {
            string[] parts = data.Split(',');

            if (parts.Length == 4 &&
                int.TryParse(parts[2], out int correctAnswersCount) &&
                int.TryParse(parts[3], out int totalQuestions))
            {
                return new Result(parts[0], parts[1], correctAnswersCount, totalQuestions);
            }
            else
            {
                return null;
            }
        }

        public override string ToString()
        {
            return $"{UserName},{Category},{CorrectAnswersCount},{TotalQuestions}";
        }
    }
}

