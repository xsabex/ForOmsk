/*
description: Тестовое задание, желательно выполнить на C#, результат выложите на GitHub, будут вопросы - пишите.
Так же напишите ориентировочный срок сколько вам потребуется на выполнение задания.

task: Есть круглый стол для игры в покер для каждого из сидящих за столом было одинаковое количество фишек. 
Но кто-то переставил все фишки так, что они перестали быть равномерно распределенными! 
Теперь нужно перераспределить фишки так, чтобы у каждого места был одинаковое количество
Но чтобы не потерять ни одной фишки в процессе передвигать фишки можно только между соседними местами. 
Более того, надо передвигать фишки только по одной за раз. Каково минимальное количество ходов фишек 
Что нужно будет сделать, чтобы вернуть равное количество?

[Test #1]
Input chips: [1, 5, 9, 10, 5]
Expected Output: 12

[Test #2]
Input chips: [1, 2, 3]
Expected Output: 1

[Test #3]
Input chips: [0, 1, 1, 1, 1, 1, 1, 1, 1, 2]
Expected Output: 1

 */

using System;
using System.Linq.Expressions;
using System.Runtime.InteropServices;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            /*int places = GetValueForPlacesAndChips($"Введите количество мест за столом: ");
            int[] chips = GetChips(places);*/
            Console.WriteLine("Введите количество фишек для каждого места через пробел:");
            string input = Console.ReadLine();
            int[] chips = Array.ConvertAll(input.Split(' '), int.Parse);

            int moves = CalculateMinValues(chips);
            Console.WriteLine($"Минимальное количество перемещений: {moves}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Произошла ошибка: {ex.Message}");
        }
    }

    #region был создан механизм лучше
    /*    static int GetValueForPlacesAndChips(string message)
        {
            Console.WriteLine(message);
            if (!int.TryParse(Console.ReadLine(), out int result) || result < 0)
                throw new ArgumentException($"Введите положительное целое число");
            return result;
        }

        static int[] GetChips(int count_place)
        {
            int[] chips = new int[count_place];
            for (int i = 0; i < count_place; i++)
            {
                chips[i] = GetValueForPlacesAndChips($"Введите количество фишек для места {i + 1}:");
            }
            return chips;
        }*/
    #endregion

    static int CalculateMinValues(int[] chips)
    {
        int chips_length = chips.Length;
        int chips_sum = chips.Sum();
        int target = chips_sum / chips_length;
        int moves = 0;

        //проверка на распределение
        if (chips_sum % chips_length != 0)
            throw new Exception($"Невозможно равномерно распределить");

        //проверка на одинаковые числа
        bool allEqual = true;
        for (int i = 0; i < chips_length; i++)
        {
            if (chips[i] != target)
            {
                allEqual = false;
                break;
            }
        }

        if (allEqual) //все фишки имеются однаковое кол-во фишек
            return 0;

        for (int i = 0; i < chips_length; i++)
        {
            // Если текущее место больше places_count
            if (chips[i] > chips_length)
            {
                int excess = chips[i] - target;
                chips[i] -= excess; // Уменьшаем текущее место до places_count
                if (i < chips_length - 1)
                {
                    chips[i + 1] += excess; // Передаем излишки на next chip
                    moves += excess; // Увеличиваем count ходов на count transferred chips
                }
            }
            // Если текущее место меньше places_count
            else if (chips[i] < target)
            {
                int deficit = target - chips[i];
                chips[i] += deficit; // Увеличиваем текущее место до places_count
                if (i > 0)
                {
                    chips[i - 1] -= deficit; // Получаем недостающие фишки из предыдущего места
                    moves += deficit; // Увеличиваем количество ходов на количество полученных фишек
                }
            }
        }
        return moves;
    }
}