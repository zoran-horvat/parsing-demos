using ParsingInterfaces;
using System;
using System.Collections.Generic;
using TextInput;

namespace TextInputDemo
{
    class Program
    {

        static void Print(IEnumerable<char> text)
        {

            int pos = 0;

            foreach (char character in text)
            {

                string toPrint = ToPrintableText(character);

                Console.Write("{0,3}", toPrint);

                pos++;
                if (pos % 20 == 0)
                    Console.WriteLine();

            }

            if (pos % 20 != 0)
                Console.WriteLine();

        }

        static string ToPrintableText(char character)
        {

            string toPrint = character.ToString();

            switch (character)
            {
                case '\r':
                    toPrint = "\\r";
                    break;
                case '\n':
                    toPrint = "\\n";
                    break;
                case '\t':
                    toPrint = "\\t";
                    break;
            }

            return toPrint;

        }

        static void Main(string[] args)
        {
            ITextInput textInput = new ConsoleLineInput("Enter some text (single line): ");
            Print(textInput.LookAhead);
            Console.WriteLine();

            textInput = new ConsoleBlockInput("Enter multiline text (end with line -):\n", "-");
            Print(textInput.LookAhead);
            Console.WriteLine();

            Console.Write("Press ENTER to exit... ");
            Console.ReadLine();

        }
    }
}
