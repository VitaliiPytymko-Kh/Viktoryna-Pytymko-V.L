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
        public static Result FromString(string data)
        {
            string[] parts = data.Split(',');
            return new Result(parts[0], parts[1], int.Parse(parts[2]));
        }
        public override string ToString()
        {
            return $"{UserName},{Category},{CorrectAnswersCount}";
        }
    }
}
