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
using System.Runtime.InteropServices;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            int places = GetValueForPlacesAndChips($"Введите количество мест за столом: ");
            int[] chips = GetChips(places);
            int moves = CalculateMinValues(chips);
            Console.WriteLine($"Минимальное количество перемещений: {moves}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Произошла ошибка: {ex.Message}");
        }
    }

    static int GetValueForPlacesAndChips(string message)
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
    }

    static int CalculateMinValues(int[] chips)
    {
        int totalChips = chips.Sum();
        int places_count = chips.Length;
        if (totalChips % places_count != 0)
            throw new InvalidOperationException($"Невозможно равномерно распределить имеющиеся фишки");

        int target = totalChips / places_count;
        int moves = 0;
        foreach (var chip in chips)
        {
            if (chip > target)
                moves += chip - target;
        }
        return moves;
    }
}