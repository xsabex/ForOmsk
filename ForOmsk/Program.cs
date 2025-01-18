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
            List<int> chips = BasicFunctions.GetChipsFromInput();

            int moves = 0;
            int chips_sum = chips.Sum();
            int average = chips_sum / chips.Count;

            bool ravnover_chips = Checkout.ravnomer_chips(chips);

            if (ravnover_chips)
                throw new Exception($"Невозможно равномерно распределить фишки");

            bool odinak_chips = Checkout.odinak_chips(chips, average);

            if (odinak_chips)
                throw new Exception($"Перемещения ненужны, у каждого места одинаковое количество фишек");

            #region govno-code, need change mind... 
            //вынести в отдельные функции
            while (chips.Any(x => x != average)) //перебираем фишки и сравниваем с средним количеством
            {
                moves += 1;
                int max = chips.Max();
                int indOfMax = chips.IndexOf(max);

                int movementIndex = indOfMax;
                int stepsToNext = 0; //шаги вперед
                int stepsToBack = 0; //шаги назад

                while (true)
                {
                    movementIndex += 1;
                    stepsToNext += 1;

                    if (movementIndex == chips.Count) //если достигли конца списка, переходим к началу
                        movementIndex = 0;

                    if (chips[movementIndex] < average) //если нашли место, где фишек меньше среднего - выходим из цикла
                        break;
                }

                movementIndex = indOfMax; //сбрасываем индекс перемещения к индексу макс.фишки

                while (true)
                {
                    movementIndex -= 1;
                    stepsToBack += 1;

                    if (movementIndex == -1) //если достигли начала списка, переходим к концу
                        movementIndex = chips.Count - 1;

                    if (chips[movementIndex] < average) //если нашли место, где фишек среднего - выходим из цикла
                        break;
                }

                //сравниваем кол-во шагов вперед и назад и выполняем перемещение
                if (stepsToNext < stepsToBack)
                {
                    int nextInd = indOfMax + 1;
                    if (nextInd == chips.Count)
                        nextInd = 0;

                    chips[indOfMax] -= 1;
                    chips[nextInd] += 1;
                }
                else if (stepsToBack <= stepsToNext)
                {
                    int nextInd = indOfMax - 1;
                    if (nextInd == -1)
                        nextInd = chips.Count - 1;

                    chips[indOfMax] -= 1;
                    chips[nextInd] += 1;
                }
            }
            #endregion

            BasicFunctions.LOG($"Минимальное количество перемещений: {moves}");
        }
        catch (Exception ex)
        {
            BasicFunctions.LOG($"Произошла ошибка: {ex.Message}");
        }
    }
}

public class BasicFunctions() 
{
    public static List<int> GetChipsFromInput()
    {
        Console.WriteLine("Введите количество фишек для каждого места через пробел:");
        string input = Console.ReadLine();
        List<int> chips = new List<int>();

        foreach (var temp in input.Split(' '))
        {
            if (int.TryParse(temp, out int chip))
                chips.Add(chip);
            else
                throw new FormatException($"{temp} является некорректным значением, введите целое число");
        }
        return chips;
    }
    public static void LOG(string text)
    {
        Console.WriteLine(text); 
    }
} 

public class Checkout()
{
    public static bool ravnomer_chips(List<int> chips)
    {
        bool ravnomer = false;
        if (chips.Sum() % chips.Count != 0)
            ravnomer = true;
        return ravnomer;
    }

    public static bool odinak_chips(List<int> chips, int average)
    {
        bool odinak = false;
        if (chips.All(x => x == average))
            odinak = true;
        return odinak;
    }
}