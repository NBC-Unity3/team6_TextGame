using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace team6_TextGame
{
    internal class UI
    {

        public int ShowList<T>(List<T> list)
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
                            break;
                        }
                        continue;
                    case ConsoleKey.UpArrow:
                        if (Console.CursorTop > first)
                        {
                            WriteAt("  ", now--);
                            WriteAt("=>", now);
                            break;
                        }
                        continue;
                    case ConsoleKey.Enter:
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


        public void TextColor(string text, ConsoleColor clr)
        {
            Console.ForegroundColor = clr;
            Console.WriteLine(text);
            Console.ResetColor();
        }


        public void Hwakin()
        {
            //Console.WriteLine(" 확인 Enter 뒤로가기 ESC");
        }


    }
}
