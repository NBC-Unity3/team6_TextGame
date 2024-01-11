using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace team6_TextGame
{
    internal class UI
    {

        public int SelectList<T>(List<T> list)
        {
            int first, last, now;


            first = Console.CursorTop;
            foreach (T item in list)
            {
                Console.WriteLine($"   {item}");
            }
            last = Console.CursorTop - 1;

            now = first;

            WriteAt("=>", now);

            while(true)
            {
                var key = Console.ReadKey(true).Key;
                switch(key)
                {
                    case ConsoleKey.DownArrow:
                        if(Console.CursorTop < last)
                        {
                            WriteAt("  ", now++);
                            WriteAt("=>", now);
                        }
                        continue;
                    case ConsoleKey.UpArrow:
                        if (Console.CursorTop > first)
                        {
                            WriteAt("  ", now--);
                            WriteAt("=>", now);
                        }
                        continue;
                    case ConsoleKey.Enter:
                        Console.SetCursorPosition(0, last + 2);
                        return now - first;
                    case ConsoleKey.Escape:
                        return -1;
                    default:
                        continue;
                }
            }


        }

        public void WriteAt(string s, int y)
        {
            Console.SetCursorPosition(0, y);
            Console.Write(s);
        }

        public void WriteAt(string s, int x, int y)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(s);
        }

        public void DrawLine()
        {
            Console.WriteLine("-----------------------------------------------");
        }


        public void TextColor(string text, ConsoleColor clr)
        {
            Console.ForegroundColor = clr;
            Console.WriteLine(text);
            Console.ResetColor();
        }

        // 텍스트 내의 숫자만 다른 색으로 출력 (텍스트, 숫자 색, 텍스트 색)
        public static void WriteColoredNumbers(string text, ConsoleColor numberColor = ConsoleColor.Magenta, ConsoleColor textColor = ConsoleColor.White)
        {
            Console.ForegroundColor = textColor;

            foreach (char c in text)
            {
                if (Char.IsDigit(c))
                {
                    Console.ForegroundColor = numberColor;
                    Console.Write(c);
                    Console.ForegroundColor = textColor;
                }
                else
                {
                    Console.Write(c);
                }
            }
            Console.ResetColor();
        }


        public void Alert()
        {
            Console.Clear();

            /* TODO: UI
            
            #############################################
            #                                           #
            #                                           #
            #                   ALERT!                  #
            #                                           #
            #                                           #
            #                                           #
            #               Press Any Key               #
            #                                           #
            #############################################

             */
        }


    }
}
