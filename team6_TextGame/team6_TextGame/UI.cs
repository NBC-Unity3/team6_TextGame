using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using team6_TextGame.Items;

namespace team6_TextGame
{
    internal class UI
    {

        public int SelectList<T>(List<T> list, int cursor = -1)
        {
            int first, last, now;

            if (cursor == -1) cursor = Console.CursorTop;   //초기 위치를 입력받지 않을 시 현재커서위치로 설정
            Console.SetCursorPosition(0, cursor);

            first = Console.CursorTop;
            foreach (T item in list)
            {
                if (item is Quest)
                {
                    Quest q = new Quest();
                    q = item as Quest;
                    if (q.isClear == true)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("   (Clear!) ");
                        Console.ResetColor();
                    }
                    else if (q.isActive == true)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.Write("   (진행중) ");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write("   (New) ");
                        Console.ResetColor();
                    }
                    Console.Write($"{q.name}\n");
                }
                else if (item is EquipItem equipItem)
                {
                    WriteColoredNumbers($"   {equipItem.ToString()}\n");
                }
                else WriteColoredNumbers($"   {item}\n");
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
                        if (list.Count == 0) return -1;
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
