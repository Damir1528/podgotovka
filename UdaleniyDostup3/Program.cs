

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tr_zad
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Введите количество строк в таблице: ");

            double[][] massiv = new double[4][]; // Инициализация двумерного массива
            massiv = new double[4][]; // Пример инициализации пустого массива

            Simplex(massiv.GetLength(0), massiv, massiv.GetLength(1)); // Вызов метода Simplex для обработки массива
        }

        public static void Simplex(int a, double[][] massiv, int b)
        {
            Console.Write("Выберите метод решения задачи\n1. Задача стремится к MAX\n2. Задача стремится к MIN\a ");
            int flag = int.Parse(Console.ReadLine()); // Получение выбора метода

            int end = 0; // Переменная для контроля завершения алгоритма

            switch (flag)
            {
                case 1: // Решение для задачи стремится к MAX
                    Console.WriteLine("Таблица:\a");

                    // Вывод исходной таблицы
                    for (int i = 0; i < massiv.Length; i++)
                    {
                        for (int j = 0; j < massiv[i].Length; j++)
                        {
                            Console.Write($"   {massiv[i][j]}");
                        }
                        Console.WriteLine();
                    }

                    while (true) // Бесконечный цикл для вычислений
                    {
                        double leading_col = massiv[a - 1][0];
                        int id_leading_col = 0;

                        // Поиск ведущего столбца
                        for (int i = 0; i < massiv.Length; i++)
                        {
                            for (int j = 0; j < b - a; j++)
                            {
                                if (massiv[a - 1][j] < leading_col)
                                {
                                    leading_col = massiv[a - 1][j];
                                    id_leading_col = j;
                                }
                            }
                        }
                        Console.WriteLine($"\nВедущий столбец: {id_leading_col}\a");

                        Console.WriteLine($"Результат после деления L(X)");

                        double[] LX = new double[massiv.Length - 1];

                        // Вычисление значений L(X)
                        for (int i = 0; i < massiv.Length - 1; i++)
                        {
                            LX[i] = massiv[i][b - 1] / massiv[i][id_leading_col];
                            Console.WriteLine($"Столбец {i}: {LX[i]}");
                        }

                        double leading_row = LX[0];
                        int id_leading_row = 0;

                        // Поиск ведущей строки
                        for (int i = 0; i < LX.Length; i++)
                        {
                            if (LX[i] <= leading_row && massiv[i][id_leading_col] != 0 && LX[i] >= 0)
                            {
                                leading_row = LX[i];
                                id_leading_row = i;
                            }
                        }
                        Console.WriteLine($"\nВедущая строка: {id_leading_row}\a");

                        double[] additional_array = new double[massiv.Length];

                        // Процесс зануления
                        for (int i = 0; i < massiv.Length; i++)
                        {
                            for (int j = 0; j < b; j++)
                            {
                                if (i == id_leading_row)
                                {
                                    continue;
                                }
                                else if (j == id_leading_col)
                                {
                                    additional_array[i] = -massiv[i][id_leading_col] / massiv[id_leading_row][id_leading_col];
                                    massiv[i][j] -= massiv[id_leading_row][id_leading_col] / massiv[id_leading_row][id_leading_col] * massiv[i][j];
                                }
                                else if (i != id_leading_row && j != id_leading_col)
                                {
                                    massiv[i][j] += additional_array[i] * massiv[id_leading_row][j];
                                }
                                else
                                    continue;
                            }
                        }

                        for (int i = 0; i < massiv.Length; i++)
                        {
                            for (int j = 0; j < id_leading_col; j++)
                                massiv[i][j] += additional_array[i] * massiv[id_leading_row][j];
                        }
                        Console.WriteLine("\nТаблица после зануления\a");

                        // Вывод таблицы после обнуления
                        for (int i = 0; i < massiv.Length; i++)
                        {
                            for (int j = 0; j < massiv[i].Length; j++)
                            {
                                Console.Write($" {Math.Round(massiv[i][j], 2)}");
                            }
                            Console.WriteLine();
                        }

                        end = 0; // Сброс счетчика положительных элементов

                        for (int i = 0; i < b; i++)
                            if (massiv[a - 1][i] > 0)
                                end++;

                        // Проверка оптимальности плана
                        if (end == 0)
                        {
                            Console.WriteLine($"\nОпорный план оптимальный; \nL(X) = {massiv[a - 1][b - 1]}");
                            break;
                        }
                    }
                    break;

                case 2:
                    Console.WriteLine("Таблица\a");
                    for (int i = 0; i < massiv.Length; i++)
                    {
                        for (int j = 0; j < massiv[i].Length; j++)
                        {
                            Console.Write($"{massiv[i][j]}   ");
                        }
                        Console.WriteLine();
                    }
                    while (true)
                    {
                        double ved_stl = massiv[a - 1][0];
                        int id_ved_stl = 0;
                        for (int i = 0; i < massiv.Length; i++)
                        {
                            for (int j = 0; j < b - a; j++)
                            {
                                if (massiv[a - 1][j] > ved_stl)
                                {
                                    ved_stl = massiv[a - 1][j];
                                    id_ved_stl = j;
                                }
                            }
                        }
                        Console.WriteLine($"\nВедущий столбец: {id_ved_stl}\a");

                        Console.WriteLine($"Результат после деления L(X) ");
                        double[] LX = new double[massiv.Length - 1];
                        for (int i = 0; i < massiv.Length - 1; i++)
                        {
                            LX[i] = massiv[i][b - 1] / massiv[i][id_ved_stl];
                            Console.WriteLine($"Столбец {i}: {LX[i]}");
                        }
                        double ved_str = LX.Max();
                        int id_ved_str = 0;
                        for (int i = 0; i < LX.Length; i++)
                        {
                            if (LX[i] <= ved_str && LX[i] >= 0)
                            {
                                ved_str = LX[i];
                                id_ved_str = i;
                            }
                        }
                        Console.WriteLine($"\nВедущая строка: {id_ved_str}\a");
                        double[] dop_mas = new double[massiv.Length];
                        for (int i = 0; i < massiv.Length; i++)
                        {
                            for (int j = 0; j < b; j++)
                            {
                                if (i == id_ved_str)
                                {
                                    continue;
                                }
                                else if (j == id_ved_stl)
                                {
                                    dop_mas[i] = -massiv[i][id_ved_stl] / massiv[id_ved_str][id_ved_stl];
                                    massiv[i][j] -= massiv[id_ved_str][id_ved_stl] / massiv[id_ved_str][id_ved_stl] * massiv[i][j];
                                }
                                else if (i != id_ved_str && j != id_ved_stl)
                                {
                                    massiv[i][j] += dop_mas[i] * massiv[id_ved_str][j];
                                }
                                else
                                    continue;
                            }
                        }
                        for (int i = 0; i < massiv.Length; i++)
                            for (int j = 0; j < id_ved_stl; j++)
                                massiv[i][j] += dop_mas[i] * massiv[id_ved_str][j];

                        Console.WriteLine("\nТаблица после зануления\a");
                        for (int i = 0; i < massiv.Length; i++)
                        {
                            for (int j = 0; j < massiv[i].Length; j++)
                            {
                                Console.Write($" {Math.Round(massiv[i][j], 2)}");
                            }
                            Console.WriteLine();
                        }
                        end = 0;
                        for (int i = 0; i < b; i++)
                            if (massiv[a - 1][i] > 0)
                                end++;
                        if (end == 0)
                        {
                            Console.WriteLine($"\nОпорный план оптимальный; \nL(X) = {massiv[a - 1][b - 1]}");
                            break;
                        }
                    }
                    break;
                default:
                    Console.WriteLine($"Нет такого варианта {flag}.(1 или 2)");
                    break;
            }
        }
    }
}