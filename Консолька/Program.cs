using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;

namespace GameConsole
{
    class Program
    {
        static List<char[]> maze = new List<char[]>();
        static int LineWidth = 117;
        static char hero = '@';
        public static int score = 0;
        public static int lives = 3;
        public static char death = '9';
        public static char lava = '▒';
        public static char zero = '0';
        public static int sum;//+
        static void Main(string[] args)
        {
            bool status = true;
            reading();
            fix();
            show();
            while (status)
            {
                var keyInfo = Console.ReadKey();
                move(keyInfo);
                show();
            }
            Console.ReadKey();
        }
        static void reading()
        {
            string[] file = File.ReadAllLines("lab.txt");
            foreach (string el in file)
            {
                maze.Add(el.ToCharArray());
            }
            for (int i = 0; i < maze.Count; i++)
                for (int j = 0; j < maze[i].Length; j++)
                {
                    if (maze[i][j] == '+') sum += 1;
                }
        }
        static void ShowText(int chis)
        {
            string text_1="", text_2="",text_3="";
            if (chis == 1) {  text_1 = "Самый бесполезный лабиринт на земле"; text_2 = "Но факт есть фактом XIAOMI ЛУЧШИЙ ЗА СВОИ ДЕНЬГИ!!!";}
            else if (chis == 2) { Console.Clear(); text_1 = "Да ладно ты смог выйграть в эту тупую игру"; text_2 = "Мои поздравления даунич";text_3 = "\n\t\t\t\t\t\t Пошел нахер отсюдого\n\n\n"; }
            else if (chis == 3) { Console.Clear(); text_1 = "Ты проиграл даунич"; text_2 = "Выйди от сюдого убожество"; }
            Console.SetCursorPosition(0, 0);
            HeaderLine("╔═╗");
            HeaderLine("║ ║");
            HeaderLine("║║", text_1);
            HeaderLine("║║", text_2);
            HeaderLine("║ ║");
            HeaderLine("╚═╝");
            Console.WriteLine(text_3);
            if (chis == 3 || chis == 2) { Console.ReadKey(); System.Environment.Exit(0); }
        }
        static void show()
        {
            if (lives <= 0) ShowText(3);
            ShowText(1);
            for (int i = 0; i < maze.Count; i++)
            {
                for (int j = 0; j < maze[i].Length; j++)
                {

                    if (maze[i][j] == '9')
                    {
                        maze[i][j] = lava;
                        death = maze[i][j];
                    }
                    if (maze[i][j] == '0')
                    {
                        maze[i][j] = '║';
                        zero = maze[i][j];
                    }
                    if (maze[i][j] == hero)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                    if (maze[i][j] == lava)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                    if (maze[i][j] == '=')
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                    }
                    Console.Write(maze[i][j]);
                    Console.ForegroundColor = ConsoleColor.White;

                }
                Console.WriteLine();
            }
            string str = "";
            if (score == 0)
            {
                str = "Ты будешь хоть что то собирать в этой игре?";
            }
            else if (score == 1)
            {
                str = "Ну что поздравляю ты хоть чего то добился в этой жизни";
            }
            else if (score <= 4)
            {
                str = "Просто лучший я верю в бога но не в тебя";
            }
            else if (score <= 17)
            {
                str = "Да ладно ты реально их собираешь(все равно скоро потратишь все )";
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Score: {score}/{sum}\tLive: {lives}/3\tСдаться: ESQ\t{str}");
            Console.ForegroundColor = ConsoleColor.White;

        }
        static void HeaderLine(string charect)
        {
            Console.Write(charect[0]);
            Console.Write(new String(charect[1], LineWidth - 2));
            Console.WriteLine(charect[2]);
        }
        static void HeaderLine(string charect, string text)
        {
            Console.Write(charect[0]);
            var textheader = (LineWidth - text.Length - 2) / 2;
            Console.Write(new string(' ', textheader));
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(text);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(new String(' ', (LineWidth - textheader - text.Length) - 2));

            Console.WriteLine(charect[1]);
        }
        static void fix()
        {
            for (int i = 1; i < maze.Count - 1; i++) { 
                for (int j = 1; j < maze[i].Length - 1; j++) { 
                    if (maze[i][j] == '0' && maze[i + 1][j] == '0' && maze[i - 1][j] == '0'
                        && maze[i][j - 1] == '0' && maze[i][j + 1] == '0') maze[i][j] = '╬';
                    if (maze[i][j] == '0' && (maze[i - 1][j] == '0' || maze[i - 1][j] != ' ') && (maze[i + 1][j] == '0'
                    || maze[i + 1][j] != ' ') && (maze[i][j + 1] == '0' || maze[i][j + 1] != ' ')) maze[i][j] = '╠';
                    if (maze[i][j] == '0' && (maze[i - 1][j] == '0' || maze[i - 1][j] != ' ') && (maze[i + 1][j] == '0'
                    || maze[i + 1][j] != ' ') && (maze[i][j - 1] == '0' || maze[i][j - 1] != ' ')) maze[i][j] = '╣';
                    if (maze[i][j] == '0' && (maze[i + 1][j] == '0' || maze[i + 1][j] != ' ') && (maze[i][j - 1] == '0'
                        || maze[i][j - 1] != ' ') && (maze[i][j + 1] == '0' || maze[i][j + 1] != ' ')) maze[i][j] = '╦';
                    if (maze[i][j] == '0' && (maze[i - 1][j] == '0' || maze[i - 1][j] != ' ') && (maze[i][j - 1] == '0'
                        || maze[i][j - 1] != ' ') && (maze[i][j + 1] == '0' || maze[i][j + 1] != ' ')) maze[i][j] = '╩';
                    if (maze[i][j] == '0' && (maze[i - 1][j] == '0' || maze[i - 1][j] != ' ')
                        && (maze[i][j - 1] == '0' || maze[i][j - 1] != ' ')) maze[i][j] = '╝';
                    if (maze[i][j] == '0' && (maze[i - 1][j] == '0' || maze[i - 1][j] != ' ')
                        && (maze[i][j + 1] == '0' || maze[i][j + 1] != ' ')) maze[i][j] = '╚';
                    if (maze[i][j] == '0' && (maze[i + 1][j] == '0' || maze[i + 1][j] != ' ')
                        && (maze[i][j + 1] == '0' || maze[i][j + 1] != ' ')) maze[i][j] = '╔';
                    if (maze[i][j] == '0' && (maze[i + 1][j] == '0' || maze[i + 1][j] != ' ')
                        && (maze[i][j - 1] == '0' || maze[i][j - 1] != ' ')) maze[i][j] = '╗';
                    if (maze[i][j] == '0' && (maze[i][j + 1] == '0' || maze[i][j - 1] == '0'
                        || maze[i][j + 1] != ' ' || maze[i][j - 1] != ' ')) maze[i][j] = '═';
                    if (maze[i][j] == '0' && (maze[i + 1][j] == '0' || maze[i - 1][j] == '0'
                        || maze[i + 1][j] != ' ' || maze[i - 1][j] != ' ')) maze[i][j] = '║';
                }
            }
        }
        static void run_i(int i, int j, int sign, bool check,bool updown)
        {
            int s;
            if (check) s = -1; else s = 1;
            maze[i][j] = ' ';
            if (!updown) maze[i - (sign * 1)*s][j] = hero; else maze[i][j-(sign * 1)*s] = hero;
            show();
            if (check) { lives--; if (!updown) checKed(i, j, sign);  else checKeds(i, j, sign); show(); }
        }
        static void checKed(int i, int j, int sign)
        {
            if (maze[i - (sign * 1)][j] != zero && maze[i - (sign * 1)][j] != death && maze[i - (sign * 1)][j] != '╬' && maze[i - (sign * 1)][j] != '╠' && maze[i - (sign * 1)][j] != '╣' && maze[i - (sign * 1)][j] != '╦' && maze[i - (sign * 1)][j] != '╩' && maze[i - (sign * 1)][j] != '╝'
                && maze[i - (sign * 1)][j] != '╚' && maze[i - (sign * 1)][j] != '╔' && maze[i - (sign * 1)][j] != '╗' && maze[i - (sign * 1)][j] != '═' && maze[i - (sign * 1)][j] != '║')
            {
                maze[i - (sign * (-1))][j] = death;
                maze[i - (sign * 1)][j] = hero;
            }
            else
            {
                maze[i - (sign * (-1))][j] = death;
                maze[i][j] = hero;
            }
        }
        static void checKeds(int i, int j, int sign)
        {
            if (maze[i][j - (sign * 1)] != zero && maze[i][j - (sign * 1)] != death && maze[i][j - (sign * 1)] != '╬' && maze[i][j - (sign * 1)] != '╠' && maze[i][j - (sign * 1)] != '╣' && maze[i][j - (sign * 1)] != '╦' && maze[i][j - (sign * 1)] != '╩' && maze[i][j - (sign * 1)] != '╝'
                && maze[i][j - (sign * 1)] != '╚' && maze[i][j - (sign * 1)] != '╔' && maze[i][j - (sign * 1)] != '╗' && maze[i][j - (sign * 1)] != '═' && maze[i][j - (sign * 1)] != '║')
            {
                maze[i][j - (sign * (-1))] = death;
                maze[i][j - (sign * 1)] = hero;
            }
            else
            {
                maze[i][j - (sign * (-1))] = death;
                maze[i][j] = hero;
            }
        }
        static void ConsoleUpDownRightLeft(int x, int y, int sign, bool check)
        {
            int a, z;
            bool ch = false;
            int mark_1, mark_2;
            if (check)
            {
                a = (sign * (-1));
                z = 0;
                ch = false;

            }
            else 
            {
                a = 0;
                z = (sign * (-1));
                ch = true;
            }
            if (sign == 1) { mark_1 = 1; mark_2 = -1; } else { mark_1 = -1; mark_2 = 1; };
            if (maze[x - a][y - z] == ' ')
            {
                run_i(x, y, mark_2, false, ch);
                show();
            }
            else if (maze[x - a][y - z] == death)
            {
                run_i(x, y, mark_1, true, ch);
                show();
            }
            else if (maze[x - a][y - z] == '+')
            {
                score++;
                run_i(x, y, mark_2, false, ch);
                show();
            }
            else if (maze[x - a][y - z] == '-')
            {
                if (score <= 0)
                {
                    score = 0;
                    show();
                }
                else
                {
                    run_i(x, y, mark_2, false, ch);
                    score--;
                    show();
                }
            }
            else if (maze[x - a][y - z] == '=')
            {
                ShowText(2);
                show();
            }
                show();
        }
        static void move(ConsoleKeyInfo key)
        {
            int x = 0, y = 0;
            for (int i = 0; i < maze.Count; i++)
                for (int j = 0; j < maze[i].Length; j++)
                {
                    if (maze[i][j] == hero)
                    {
                        x = i;
                        y = j;
                    }
                }
            switch (key.Key)
            {
                case ConsoleKey.Escape:
                    ShowText(3);
                    break;
                case ConsoleKey.UpArrow:
                    ConsoleUpDownRightLeft(x, y, -1,true);
                    break;
                case ConsoleKey.DownArrow:
                    ConsoleUpDownRightLeft(x, y, 1, true);
                    break;
                case ConsoleKey.LeftArrow:
                    ConsoleUpDownRightLeft(x, y, -1, false);
                    break;
                case ConsoleKey.RightArrow:
                    ConsoleUpDownRightLeft(x, y, 1, false);
                    break;
            }
        }
    }
}


