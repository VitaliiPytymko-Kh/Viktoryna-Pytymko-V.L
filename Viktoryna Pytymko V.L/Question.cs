using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Viktoryna_Pytymko_V.L
{
    public class Question
    {
        public string Category { get; set; }
        public string Text { get; set; }
        public Dictionary<string, bool> Options { get; set; }
        public Question(string category, string text, Dictionary<string, bool> options)

        {
            Category = category;
            Text = text;
            Options = options;
        }

        public static Question FromString(string data)
        {
            try
            {
                string[] parts = data.Split('/');

                if (parts.Length < 3)
                {
                    Console.WriteLine($"Invalid format: {data}");
                    return null;
                }

                string category = parts[0];
                string text = parts[1];

                Dictionary<string, bool> options = new Dictionary<string, bool>();

                string[] optionsPart = parts[2].Split(',');

                for (int i = 0; i < optionsPart.Length; i += 2)
                {
                    string optionText = optionsPart[i];
                    string optionValue = optionsPart[i + 1];
                    optionValue = optionValue.TrimEnd(';');
                    if (bool.TryParse(optionValue, out bool result))
                    {
                        options.Add(optionText, result);
                    }
                    else
                    {
                        Console.WriteLine($"Error parsing option data: {data}");
                        return null;
                    }
                }
                return new Question(category, text, options);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error parsing data: {ex.Message}");
                return null;
            }
        }

        public override string ToString()
        {
            string optionsString = string.Join(",", Options.Select(kv => $"{kv.Key},{kv.Value}"));
            return $"{Category};{Text};{optionsString}";
        }

    }
}

