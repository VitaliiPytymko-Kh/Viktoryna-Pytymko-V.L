using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Viktoryna_Pytymko_V.L
{
    public class Question
    {
        public string Category { get; }
        public string Text { get; }
        public Dictionary<string, bool> Options { get; }

        private Question(string category, string text, Dictionary<string, bool> options)
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
                    Console.WriteLine($"Invalid format: {data}. Insufficient parts.");
                    return null;
                }

                string category = parts[0];
                string text = parts[1];

                Dictionary<string, bool> options = ParseOptions(parts[2]);

                if (options == null)
                {
                    Console.WriteLine($"Error parsing options for data: {data}");
                    return null;
                }

                return new Question(category, text, options);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error parsing data: {ex.Message}");
                return null;
            }
        }

        private static Dictionary<string, bool> ParseOptions(string optionsPart)
        {
            Dictionary<string, bool> options = new Dictionary<string, bool>();
            string[] optionParts = optionsPart.Split(',');

            for (int i = 0; i < optionParts.Length; i += 2)
            {
                string optionText = optionParts[i];
                string optionValue = optionParts[i + 1].TrimEnd(';');

                if (bool.TryParse(optionValue, out bool result))
                {
                    options.Add(optionText, result);
                }
                else
                {
                    return null; 
                }
            }

            return options;
        }

        public override string ToString()
        {
            string optionsString = string.Join(",", Options.Select(kv => $"{kv.Key},{kv.Value}"));
            return $"{Category};{Text};{optionsString}";
        }
    }
}

