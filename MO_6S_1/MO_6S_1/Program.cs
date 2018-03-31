using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MO_6S_1
{
    class Program
    {
        static int NUM_X = 5;
        static int rkA = 3, NUM_U = 3;
        static int LINE = -1, INDEX = -1;
        static Pair[] pointC;// = new Pair[] { 1, 3, -3, 0, 0 };
        public class Pair
        {
            public Pair(int numerator = 0, int denominator = 1)
            {
                this.numerator = numerator;
                this.denominator = denominator;
            }
            public Pair (int number)
            {
                this.numerator = number;
                this.denominator = 1;
            }
            // числитель и знаменатель
            int numerator, denominator;
            // методы
            public int getNumerator()
            {
                return numerator;
            }
            public int getDenominator()
            {
                return denominator;
            }
            private int gcd_1(int a, int b)
            {
                if (b == 0)
                    return a;
                return gcd_1(b, a % b);
            }
            public void check()
            {
                int _gcd = gcd_1(numerator, denominator);
                numerator /= _gcd;
                denominator /= _gcd;
                if (denominator < 0)
                {
                    denominator = -denominator;
                    numerator = -numerator;
                }
            }
            // переопределение 
            public override string ToString()
            {
                if (denominator != 0)
                {
                    if ( numerator % denominator == 0)
                    {
                        return (numerator / denominator).ToString();
                    }
                    else
                    {
                        return numerator + "/" + denominator;
                    }   
                }
                else return "NaN";
            }
            public double ToNumber()
            {
                if (denominator != 0)
                {
                    return (double)numerator / (double)denominator;
                }
                else return double.NaN;
            }
            // перегрузка операторов 
            public static Pair operator +(Pair pair1, Pair pair2)
            {
                //if (pair1 == null)
                //{
                //    pair1 = 0;
                //}
                //if (pair2 == null)
                //{
                //    pair2 = 0;
                //}
                Pair pair = new Pair();
                pair.numerator = pair1.numerator * pair2.denominator 
                                 + pair2.numerator * pair1.denominator;
                pair.denominator = pair1.denominator * pair2.denominator;
                pair.check();
                return pair;
            }
            public static Pair operator -(Pair pair1, Pair pair2)
            {
                Pair pair = new Pair();
                pair.numerator = pair1.numerator * pair2.denominator
                                 - pair2.numerator * pair1.denominator;
                pair.denominator = pair1.denominator * pair2.denominator;
                pair.check();
                return pair;
            }
            public static Pair operator *(Pair pair1, Pair pair2)
            {
                Pair pair = new Pair();
                pair.numerator = pair1.numerator * pair2.numerator;
                pair.denominator = pair1.denominator * pair2.denominator;
                pair.check();
                return pair;
            }
            public static Pair operator /(Pair pair1, Pair pair2)
            {
                Pair pair = new Pair();
                pair.numerator = pair1.numerator * pair2.denominator;
                pair.denominator = pair1.denominator * pair2.numerator;
                pair.check();
                return pair;
            }
            public static Pair operator -(Pair pair1)
            {
                Pair pair = new Pair();
                pair.numerator = -pair1.numerator;
                pair.denominator = pair1.denominator;
                pair.check();
                return pair;
            }
            // операторы преобразования
            public static implicit operator Pair (int num)
            {
                return new Pair(num);
            }
        }
        public class Line
        {
            public Line(string identity)
            {
                this.identity = identity;
            }
            public void changeIdentity (string identity)
            {
                this.identity = identity;
            }
            public Pair[] x = new Pair[NUM_X];
            public Pair[] u = new Pair[NUM_U];
            public Pair v = 0;
            public string identity = "";
            // методы
            public void change (Pair divider)
            {
                int i = 0;
                foreach (Pair X in x)
                {
                    x[i] = X * divider;
                    i++;
                }
                i = 0;
                foreach (Pair U in u)
                {
                    u[i] = U * divider;
                    i++;
                }
                v *= divider;
            }
            // перегрузка операторов
            public static Line operator +(Line line1, Line line2)
            {
                Line line = new Line(line1.identity);
                line.x = new Pair[NUM_X];
                line.u = new Pair[NUM_U];
                line.v = line1.v + line2.v;
                for (int i = 0; i < line1.u.Length; i++)
                {
                    if (line1.u[i] == null || line2.u[i] == null)
                    {
                        continue;
                    }
                    line.u[i] = line1.u[i] + line2.u[i];
                }
                for (int i = 0; i < line1.x.Length; i++)
                {
                    line.x[i] = line1.x[i] + line2.x[i];
                }
                return line;
            }
            public static Line operator *(Line line1, Pair p)
            {
                Line line = new Line(line1.identity);
                line.x = new Pair[NUM_X];
                line.u = new Pair[NUM_U];
                for (int i = 0; i < line1.u.Length; i++)
                {
                    line.u[i] = line1.u[i] * p;
                }
                for (int i = 0; i < line1.x.Length; i++)
                {
                    line.x[i] = line1.x[i] * p;
                }
                line.v = line1.v * p;
                return line;
            }
            // переопределение 
            public override string ToString()
            {
                string nums = "";
                //foreach (Pair U in u)
                //{
                //    if (U == null)
                //    {
                //        continue;
                //    }
                //    nums += U.ToString() + " ";
                //}
                foreach (Pair X in x)
                {
                    nums += X.ToString() + " ";
                }
                return identity + " | " + v + " | " + nums;
            }
            public string ToStringWithU()
            {
                string nums = "";
                foreach (Pair U in u)
                {
                    if (U == null)
                    {
                        continue;
                    }
                    nums += U.ToString() + " ";
                }
                foreach (Pair X in x)
                {
                    nums += X.ToString() + " ";
                }
                return identity + " | " + v + " | " + nums;
            }
        }
        public static void funcU(ref Line[] L)
        {
            for (int i = 0; i < L.Length - 1; i++)
            {
                L[L.Length - 1].v += L[i].v;
                for (int k = 0; k < L[i].u.Length; k++)
                {
                    L[L.Length - 1].u[k] = 0;
                }
                for (int k = 0; k < L[i].x.Length; k++)
                {
                    L[L.Length - 1].x[k] += L[i].x[k];
                }
            }
        }
        public static void funcX(ref Line[] L)
        {
            L[L.Length - 1] = new Line(L[L.Length - 1].identity);
            for (int i = 0; i < L.Length - 1; i++)
            {
                int index = int.Parse(L[i].identity.Substring(1).ToString()) - 1;
                if (L[L.Length - 1].v == null)
                {
                    L[L.Length - 1].v = 0;
                }
                L[L.Length - 1].v += pointC[index] * L[i].v;
                for (int k = 0; k < L[i].x.Length; k++)
                {
                    if (L[L.Length - 1].x[k] == null)
                    {
                        L[L.Length - 1].x[k] = 0;
                    }
                    L[L.Length - 1].x[k] += pointC[index] * L[i].x[k];
                    if (i == L.Length - 2)
                    {
                        L[L.Length - 1].x[k] += -pointC[k];
                    }
                }
            }
            
        }
        public static void changeLines(ref Line[] L, int line, int index)
        {
            L[line].identity = "x" + (index + 1);
            for (int i = 0; i < L.Length; i++)
            {
                if (i != line)
                {
                    Pair D1 = -L[i].x[index];
                    Pair D2 = L[line].x[index];
                    Pair currDivider = (-L[i].x[index] / L[line].x[index]);
                    L[i] += L[line] * (-L[i].x[index] / L[line].x[index]);

                    // если отрицательный корень !

                    //if (L[i].v.ToNumber() < 0)
                    //{
                    //    L[i].change(-1);
                    //}
                }
                else
                {
                    Pair currDivider = L[i].x[index];
                    L[i] *= 1 / L[i].x[index];
                }
            }
        }
        // составление симплекс-таблицы
        public static void SimplexTable (ref Line[] L)
        {
            // для каждого столбца
            for (int i = 0; i < L[L.Length - 1].x.Length; i++)
            {
                if (L[L.Length - 1].x[i].ToNumber() > 0)
                {
                    int line = -1;
                    Pair d = new Pair(0);
                    // для каждой строки
                    for (int k = 0; k < L.Length - 1; k++)
                    {
                        if (L[L.Length - 1].x[i].ToNumber() == 0)
                        {
                            continue;
                        }
                        // если столбец ещё не выбран и частное от деления больше 0
                        if (line == -1 && (L[k].v / L[k].x[i]).ToNumber() > 0)
                        {
                            d = L[k].v / L[k].x[i];
                            line = k;
                        }
                        // если уже есть выбранный столбец и новое значение меньше текущего
                        if ((L[k].v / L[k].x[i]).ToNumber() > 0 
                            && (L[k].v / L[k].x[i]).ToNumber() < d.ToNumber())
                        {
                            d = L[k].v / L[k].x[i];
                            line = k;
                        }
                    }
                    INDEX = i;
                    LINE = line;
                    if (L[L.Length - 1].u[0] == null)
                    {
                        drawTable(L, LINE, INDEX);
                    }
                    else
                    {
                        drawTable(L, LINE, INDEX, true);
                    }
                    //Console.WriteLine("(" + (i + 1) + "; " + (line + 1) + ")");
                    changeLines(ref L, line, i);
                    break;
                }
            }
        }
        // проверка на первый случай
        public static int isFirstCase (Line L) // 0 - нет точек, 
                                               // 1 - первый случай, 
                                               // 2 - второй или третий случай
        {
            int check = 0;
            foreach (Pair X in L.x)
            {
                if (X.ToNumber() <= 0)
                {
                    check = 1;
                }
                else
                {
                    return 2;
                }
            }
            if (L.v.ToNumber() != 0)
            {
                return 0;
            }
            return check;
        }
        // проверка на второй и третий случаи 
        public static bool isSecondCase (Line[] L)
        {
            // проверка столбцов x
            for (int i = 0; i < L[L.Length - 1].x.Length; i++)
            {
                if (L[L.Length - 1].x[i].ToNumber() > 0)
                {
                    // по строкам
                    foreach (Line l in L)
                    {
                        if (l.identity == "d ")
                        {
                            continue;
                        }
                        if (l.x[i].ToNumber() > 0)
                        {
                            return false;
                        }
                    }
                }
                
            }
            return true;
        }
        // проверка случая
        public static int checkCase(Line[] L)
        {
            switch (isFirstCase(L[L.Length - 1]))
            {
                case 0:
                    {
                        if (L[L.Length - 1].u[0] == null)
                        {
                            return 1;
                        }
                        return -1;
                    }
                case 1:
                    {
                        return 1;
                    }
                case 2:
                    {
                        if (isSecondCase(L))
                        {
                            return 2;
                        }
                        else
                        {
                            return 3;
                        }
                    }
                default:
                    {
                        return 0;
                    }
            }
        }
        // отрисовка таблицы
        public static void drawTable(Line[] L, int line = -1, int index = -1, bool isU = false)
        {
            // размер внутреннего отступа
            int padding = 3;
            string pad = new string(' ', padding);
            string title = "    |" + pad + "v" + pad + "|" + pad;
            string[] strs = new string[rkA + 1];
            // вывод u-коэффициентов
            if (isU)
            {
                for (int i = 0; i < L[1].u.Length; i++)
                {
                    title += "u" + (i + 1) + pad + "|" + pad;
                }
            }
            // отрисовка заголовка
            for (int i = 0; i < L[1].x.Length; i++)
            {
                title += "x" + (i + 1) + pad + "|" + pad;
            }
            // орисовка строк
            for (int i = 0; i < L.Length; i++)
            {
                // обозначение строки с разрешающим элементом
                strs[i] = " " + L[i].identity + " ";
                int pad1 = (int)Math.Floor((decimal)(L[i].v.ToString().Length / 2));
                int pad2 = 0;
                if (L[i].v.ToString().Length % 2 != 0)
                {
                    pad2 = 1;
                }
                // столбец с v
                strs[i] += "|" + new string(' ', padding - pad1)
                    + L[i].v.ToString() 
                    + new string(' ', padding - pad1 + 1 - pad2) 
                    + "|";
                // столбец с u
                if (isU)
                {
                    for (int k = 0; k < L[1].u.Length; k++)
                    {
                        //if(L[i].u[k] == null)
                        //{
                        //    L[i].u[k] = 0;
                        //}
                        pad1 = (int)Math.Floor((decimal)(L[i].u[k].ToString().Length / 2));
                        pad2 = 0;
                        if (L[i].u[k].ToString().Length % 2 != 0)
                        {
                            pad2 = 1;
                        }
                        strs[i] += new string(' ', padding - pad1 + 1)
                            + L[i].u[k].ToString() 
                            + new string(' ', padding + 1 - pad1 - pad2)
                            + "|";
                    }
                }
                // столбец с x
                for (int k = 0; k < L[1].x.Length; k++)
                {
                    pad1 = (int)Math.Floor((decimal)(L[i].x[k].ToString().Length / 2));
                    pad2 = 0;
                    if (L[i].x[k].ToString().Length % 2 != 0)
                    {
                        pad2 = 1;
                    }
                    strs[i] += new string(' ', padding - pad1 + 1)
                        + L[i].x[k].ToString() + new string(' ', padding +1 - pad1 - pad2)
                        + "|";
                }

            }
            if (line > -1)
            {
                strs[line] += " <-";
            }
            //int t = title.Length;
            string upTitle = "\n" + new string('-', title.Length) + "\n";
            string footer = "";
            if (index > -1)
            {
                if (isU)
                {
                    footer = new string('-', 7 + (3 + 2 * L[1].u.Length + 2 * index)
                                            * padding + (L[1].u.Length + index) * 3)
                                + "^^^" + new string('-', 2 * (L[1].x.Length - index)
                                            * padding + 3 * (L[1].x.Length - index) - 3);
                }
                else
                {
                    footer = new string('-', 7 + (3 + 2 * index)
                                            * padding + index * 3)
                                + "^^^" + new string('-', 2 * (L[1].x.Length - index)
                                            * padding + 3 * (L[1].x.Length - index) - 3);
                }
                
            }
            else
            {
                footer = new string('-', title.Length) + "\n";
            }
            string rows = "";
            foreach (string str in strs)
            {
                rows += str + "\n";
            }
            Console.WriteLine(upTitle + title + upTitle + rows + footer);

        }
        // симплекс
        private static bool isCanonical = false;
        public static void Simplex(ref Line[] L)
        {
            SimplexTable(ref L);
            if (isCanonical)
            {
                funcX(ref L);
                drawTable(L, LINE, INDEX);
            }
            switch (checkCase(L))
            {
                case -1:
                    {
                        Console.WriteLine("Нет точек");
                        break;
                    }
                case 1:
                    {
                        if (!isCanonical)
                        {
                            drawTable(L, -1, -1, true);
                        }
                        if (isCanonical)
                        {
                            drawTable(L);
                            Pair[] res = new Pair[NUM_X];
                            string result = "f* = ";
                            foreach(Line l in L)
                            {
                                if (l.identity == "d ")
                                {
                                    result += L[L.Length - 1].v.ToString() + "\nx* = (";
                                }
                                else
                                {
                                    res[int.Parse(l.identity.Substring(1).ToString())-1] = l.v;
                                }
                            }
                            foreach(Pair r in res)
                            {
                                if (r == null)
                                {
                                    result += 0 + ", ";
                                }
                                else
                                {
                                    result += r.ToString() + ", ";
                                }
                            }
                            result += ")";
                            Console.WriteLine(result);
                            break;
                        }
                        string strPoints = "";
                        Pair[] points = new Pair[NUM_X];
                        for (int i = 0; i < L.Length - 1; i++)
                        {
                            points[int.Parse(L[i].identity.ToArray()[1].ToString())-1] = L[i].v;
                        }
                        for (int i = 0; i < points.Length; i++)
                        {
                            if (points[i] == null)
                            {
                                points[i] = 0;
                            }
                            strPoints += points[i].ToString();
                            if (i != points.Length - 1)
                            {
                                strPoints += ", ";
                            }
                        }
                        Console.WriteLine("Начальная угловая точка: (" + 
                                          strPoints + ")");
                        isCanonical = true;
                        //Simplex(ref L);
                        funcX(ref L);
                        //drawTable(L);
                        Simplex(ref L);
                        break;
                    }
                case 2:
                    {
                        Console.WriteLine("f* = -infinity");
                        break;
                    }
                case 3:
                    {
                        //flag = true;
                        Simplex(ref L);
                        break;
                    }
            }
        }
        public static void error()
        {
            Console.WriteLine("Строка имела неправильный формат");
        }
        public static Line[] enterValues()
        {
            bool lockflag = true;

            while (lockflag)
            {
                Console.Write("Количество переменных x: ");
                if (int.TryParse(Console.ReadLine(), out NUM_X))
                {
                    lockflag = false;
                }
                else
                {
                    error();
                }
            }
            lockflag = true;

            while (lockflag)
            {
                Console.Write("\nРанг матрицы (количество u): ");
                if (int.TryParse(Console.ReadLine(), out NUM_U))
                {
                    rkA = NUM_U;
                    lockflag = false;
                }
                else
                {
                    error();
                }
            }

            Line[] L = new Line[rkA + 1];
            for (int i = 0; i < rkA; i++)
            {
                L[i] = new Line("u" + (i + 1));
            }

            Console.WriteLine("\nВведите построчно матрицу A");
            for (int i = 0; i < NUM_U; i++)
            {
                lockflag = true;
                while (lockflag)
                {
                    string[] arrayInput = new string[NUM_X];
                    Console.Write("Строка " + (i + 1) + ": ");
                    arrayInput = Console.ReadLine().Split(' ');
                    if (arrayInput.Length == NUM_X)
                    {
                        L[i].x = new Pair[NUM_X];
                        for (int k = 0; k < NUM_X; k++)
                        {
                            int num = 0;
                            if (int.TryParse(arrayInput[k], out num))
                            {
                                L[i].x[k] = num;
                                if (k == NUM_X - 1)
                                {
                                    lockflag = false;
                                }
                            }
                            else
                            {
                                error();
                                break;
                            }
                        }
                    }
                    else
                    {
                        error();
                    }
                }
            }

            Console.WriteLine("\nВведите матрицу b");
            for (int i = 0; i < NUM_U; i++)
            {
                lockflag = true;
                while (lockflag)
                {
                    string input;
                    int num;
                    Console.Write("Значение " + (i + 1) + ": ");
                    input = Console.ReadLine();
                    if (int.TryParse(input, out num))
                    {
                        L[i].v = num;
                        lockflag = false;
                    }
                    else
                    {
                        error();
                    }
                }
            }
            for (int i = 0; i < NUM_U; i++)
            {
                for (int k = 0; k < NUM_U; k++)
                {
                    if (i == k)
                    {
                        L[i].u[k] = 1;
                    }
                    else
                    {
                        L[i].u[k] = 0;
                    }
                }
            }

            lockflag = true;
            while (lockflag)
            {
                pointC = new Pair[NUM_X];
                string[] arrayInput = new string[NUM_X];
                int num;
                Console.Write("\nВведите коэффициенты канонического уравнения: ");
                arrayInput = Console.ReadLine().Split(' ');
                if (arrayInput.Length <= NUM_X)
                {
                    for (int k = 0; k < NUM_X; k++)
                    {
                        if (k < arrayInput.Length)
                        {
                            if (int.TryParse(arrayInput[k], out num))
                            {
                                pointC[k] = num;
                                if (k == NUM_X - 1)
                                {
                                    lockflag = false;
                                }
                            }
                            else
                            {
                                error();
                            }
                        }
                        else
                        {
                            pointC[k] = 0;
                            if (k == NUM_X - 1)
                            {
                                lockflag = false;
                            }
                        }
                    }
                }
                else
                {
                    error();
                }
            }

            L[L.Length - 1] = new Line("d ");
            L[L.Length - 1].x = new Pair[NUM_X];

            for (int i = 0; i < L[L.Length - 1].x.Length; i++)
            {
                L[L.Length - 1].x[i] = 0;
            }

            L[L.Length - 1].u = new Pair[NUM_U];

            for (int i = 0; i < L[L.Length - 1].u.Length; i++)
            {
                L[L.Length - 1].u[i] = 0;
            }

            return L;
        }
        static void Main(string[] args)
        {
            Console.Title = "Симплекс-метод"; 
            Line[] L = enterValues();

            funcU(ref L);
            Simplex(ref L);

            Console.ReadLine();

            Main(args);
        }
    }
}
