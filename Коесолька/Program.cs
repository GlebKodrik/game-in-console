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
        static void show()
        {
            if (lives <= 0) you_lose();
            Console.SetCursorPosition(0, 0);
            HeaderLine("╔═╗");
            HeaderLine("║ ║");
            HeaderLine("║║", "Самый бесполезный лабиринт на земле");
            HeaderLine("║║", "Но факт есть фактом XIAOMI ЛУЧШИЙ ЗА СВОИ ДЕНЬГИ!!!");
            HeaderLine("║ ║");
            HeaderLine("╚═╝");
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
        static void you_win()
        {
            Console.Clear();
            HeaderLine("╔═╗");
            HeaderLine("║ ║");
            HeaderLine("║║", "Да ладно ты смог выйграть в эту тупую игру");
            HeaderLine("║║", "Мои поздравления даунич");
            HeaderLine("║ ║");
            HeaderLine("╚═╝");
            Console.WriteLine("\n\t\t\t\t\t\t Пошел нахер отсюдого");
            Console.WriteLine("\n\n\n");
            Console.ReadKey();
            System.Environment.Exit(0);
        }
        static void you_lose()
        {
            Console.Clear();
            HeaderLine("╔═╗");
            HeaderLine("║ ║");
            HeaderLine("║║", "Ты проиграл даунич");
            HeaderLine("║║", "Выйди от сюдого убожество");
            HeaderLine("║ ║");
            HeaderLine("╚═╝");
            Console.ReadKey();
            System.Environment.Exit(0);
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
            for (int i = 1; i < maze.Count - 1; i++)
                for (int j = 1; j < maze[i].Length - 1; j++)
                    if (maze[i][j] == '0' && maze[i + 1][j] == '0' && maze[i - 1][j] == '0'
                        && maze[i][j - 1] == '0' && maze[i][j + 1] == '0') maze[i][j] = '╬';

            for (int i = 1; i < maze.Count - 1; i++)
                for (int j = 1; j < maze[i].Length - 1; j++)
                    if (maze[i][j] == '0' && (maze[i - 1][j] == '0' || maze[i - 1][j] != ' ') && (maze[i + 1][j] == '0'
                        || maze[i + 1][j] != ' ') && (maze[i][j + 1] == '0' || maze[i][j + 1] != ' ')) maze[i][j] = '╠';

            for (int i = 1; i < maze.Count - 1; i++)
                for (int j = 1; j < maze[i].Length - 1; j++)
                    if (maze[i][j] == '0' && (maze[i - 1][j] == '0' || maze[i - 1][j] != ' ') && (maze[i + 1][j] == '0'
                        || maze[i + 1][j] != ' ') && (maze[i][j - 1] == '0' || maze[i][j - 1] != ' ')) maze[i][j] = '╣';

            for (int i = 1; i < maze.Count - 1; i++)
                for (int j = 1; j < maze[i].Length - 1; j++)
                    if (maze[i][j] == '0' && (maze[i + 1][j] == '0' || maze[i + 1][j] != ' ') && (maze[i][j - 1] == '0'
                        || maze[i][j - 1] != ' ') && (maze[i][j + 1] == '0' || maze[i][j + 1] != ' ')) maze[i][j] = '╦';

            for (int i = 1; i < maze.Count - 1; i++)
                for (int j = 1; j < maze[i].Length - 1; j++)
                    if (maze[i][j] == '0' && (maze[i - 1][j] == '0' || maze[i - 1][j] != ' ') && (maze[i][j - 1] == '0'
                        || maze[i][j - 1] != ' ') && (maze[i][j + 1] == '0' || maze[i][j + 1] != ' ')) maze[i][j] = '╩';

            for (int i = 1; i < maze.Count - 1; i++)
                for (int j = 1; j < maze[i].Length - 1; j++)
                    if (maze[i][j] == '0' && (maze[i - 1][j] == '0' || maze[i - 1][j] != ' ')
                        && (maze[i][j - 1] == '0' || maze[i][j - 1] != ' ')) maze[i][j] = '╝';

            for (int i = 1; i < maze.Count - 1; i++)
                for (int j = 1; j < maze[i].Length - 1; j++)
                    if (maze[i][j] == '0' && (maze[i - 1][j] == '0' || maze[i - 1][j] != ' ')
                        && (maze[i][j + 1] == '0' || maze[i][j + 1] != ' ')) maze[i][j] = '╚';

            for (int i = 1; i < maze.Count - 1; i++)
                for (int j = 1; j < maze[i].Length - 1; j++)
                    if (maze[i][j] == '0' && (maze[i + 1][j] == '0' || maze[i + 1][j] != ' ')
                        && (maze[i][j + 1] == '0' || maze[i][j + 1] != ' ')) maze[i][j] = '╔';

            for (int i = 1; i < maze.Count - 1; i++)
                for (int j = 1; j < maze[i].Length - 1; j++)
                    if (maze[i][j] == '0' && (maze[i + 1][j] == '0' || maze[i + 1][j] != ' ')
                        && (maze[i][j - 1] == '0' || maze[i][j - 1] != ' ')) maze[i][j] = '╗';

            for (int i = 1; i < maze.Count - 1; i++)
                for (int j = 1; j < maze[i].Length - 1; j++)
                    if (maze[i][j] == '0' && (maze[i][j + 1] == '0' || maze[i][j - 1] == '0'
                        || maze[i][j + 1] != ' ' || maze[i][j - 1] != ' ')) maze[i][j] = '═';

            for (int i = 1; i < maze.Count - 1; i++)
                for (int j = 1; j < maze[i].Length - 1; j++)
                    if (maze[i][j] == '0' && (maze[i + 1][j] == '0' || maze[i - 1][j] == '0'
                        || maze[i + 1][j] != ' ' || maze[i - 1][j] != ' ')) maze[i][j] = '║';
        }
        static void run_i(int i, int j, int sign, bool check)
        {
            int s;
            if (check) s = -1; else s = 1;
            maze[i][j] = ' ';
            maze[i - (sign * 1)*s][j] = hero;
            show();
            if (check) { lives--; checKed(i, j, sign); show(); }
        }
        static void run_j(int i, int j, int sign, bool check)
        {
            int s;
            if (check) s = -1; else s = 1;
            maze[i][j] = ' ';
            maze[i][j-(sign * 1)*s] = hero;
            show();
            if (check) { lives--; checKeds(i, j, sign); show(); }
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

        static void ConsoleUpDown(int x, int y, int sign)
        {
            int mark_1, mark_2;
            if (sign == 1) { mark_1 = 1; mark_2 = -1; } else { mark_1 = -1; mark_2 = 1; };
            if (maze[x - (sign * (-1))][y] == ' ')
            {
                run_i(x, y, mark_2, false);
                show();
            }
            else if (maze[x - (sign * (-1))][y] == death)
            {
                run_i(x, y, mark_1, true);
                show();
            }
            else if (maze[x - (sign * (-1))][y] == '+')
            {
                score++;
                Console.Clear();
                run_i(x, y, mark_2, false);
                show();
            }
            else if (maze[x - (sign * (-1))][y] == '-')
            {
                if (score <= 0)
                {
                    score = 0;
                    show();
                }
                else
                {
                    run_i(x, y, mark_2, false);
                    score--;
                    show();
                }
                Console.Clear();
                show();
            }
        }
        static void ConsoleRightLeft(int x, int y, int sign)
        {
            int mark_1, mark_2;
            if (sign == 1) { mark_1 = 1; mark_2 = -1; } else { mark_1 = -1; mark_2 = 1; };
            if (maze[x][y - (sign * (-1))] == ' ')
            {
                run_j(x, y, mark_2, false);
                show();
            }
            else if (maze[x][y - (sign * (-1))] == death)
            {
                run_j(x, y, mark_1, true);
                show();
            }
            else if (maze[x][y - (sign * (-1))] == '+')
            {
                score++;
                run_j(x, y, mark_2, false);
                Console.Clear();
                show();
            }
            else if (maze[x][y - (sign * (-1))] == '-')
            {
                if (score <= 0)
                {
                    score = 0;
                    show();
                }
                else
                {
                    run_j(x, y, mark_2, false);
                    score--;
                    show();
                }
                Console.Clear();
                show();
            }
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
                    you_lose();
                    break;
                case ConsoleKey.UpArrow:
                    ConsoleUpDown(x, y, -1);
                    break;
                case ConsoleKey.DownArrow:
                    ConsoleUpDown(x, y, 1);
                    break;
                case ConsoleKey.LeftArrow:
                    ConsoleRightLeft(x, y, -1);
                    break;
                case ConsoleKey.RightArrow:
                    ConsoleRightLeft(x, y, 1);
                    break;
            }
        }
    }
}


