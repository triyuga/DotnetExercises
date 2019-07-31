using System;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace NumberWonder
{
    class Program
    {
        static void Main(string[] args)
        {
            log("");
            
            bool hasArg = args.ElementAtOrDefault(0) != null;
            if (!hasArg) {
                error("Must pass exactly one arg.");
            }

            char[] numbers = args[0].ToCharArray();

            // Validate
            foreach (char c in numbers) 
            {
                List<char> allowedChars = new List<char>(numberMap.Keys);
                bool isValid = allowedChars.Contains(c);
                if (!isValid) {
                    error(string.Format("Invalid char \"{0}\" in arg: \"{1}\". Only numbers 1-9 are accepted.", c, args[0]));
                }
            }

            // Print
            // string output = "";
            // foreach (char c in args[0].ToCharArray()) 
            // {
            //     output += numberMap[c];
            // }
            // log(output);
            // log("numbers", numbers);
            // log("numbers.Count()", numbers.Count());

            // Print colourfully.
            int colorKey = 0;
            for(int i = 0; i < numbers.Count(); i++)
            {
                colorKey++;
                colorKey = (colorKey+1 > Colors.Count()) ? 0 : colorKey;
                Console.ForegroundColor = (ConsoleColor) Enum.Parse(typeof(ConsoleColor), Colors[colorKey]);
                System.Console.Write(numberMap[numbers[i]]);
            }
            Console.ForegroundColor = ConsoleColor.White;
            log("");

            log("");
            log("finished...");
            log("");
        }

        static Dictionary<char, string> numberMap = new Dictionary<char, string>() {
            {'0', "Zero"},
            {'1', "One"},
            {'2', "Two"},
            {'3', "Three"},
            {'4', "Four"},
            {'5', "Five"},
            {'6', "Six"},
            {'7', "Seven"},
            {'8', "Eight"},
            {'9', "Nine"},
        };

        static string[] Colors = new string[] { 
            "Red", 
            "Green", 
            "Yellow", 
            "Blue",
            "Magenta",
            "Cyan",
        };

        static void log(object msg, object obj = null) {
            if (isString(msg) && obj != null) {
                Console.WriteLine("{0}:", msg);
                 if (isString(obj)) {
                    Console.WriteLine(obj);
                } else {
                    Console.WriteLine(jsonStringify(obj));
                }
            } else if (isString(msg)) {
                Console.WriteLine(msg);
            } else {
                Console.WriteLine(jsonStringify(msg));
            }
        }

        static void error(object obj, bool exit = true) {
            Console.ForegroundColor = ConsoleColor.Red;
            log("ERROR", obj);
            Console.ForegroundColor = ConsoleColor.White;
            if (exit) {
                log("");
                System.Environment.Exit(1);
            }
        }

        static bool isString(object obj) {
            return obj.GetType().ToString() == "System.String" || obj.GetType().ToString() == "System.Char";
        }

        static string jsonStringify(object obj) {
            return JsonConvert.SerializeObject(obj, Formatting.Indented);
        }
    }
}
