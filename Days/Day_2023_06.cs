using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Day_2023_06 : DayScript2023
{
    protected override string part_1()
    {
        List<int> times = _input.Split('\n')[0].Substring(5).Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToList();
        List<int> distances = _input.Split('\n')[1].Substring(9).Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToList();

        double result = 1;
        for (int i = 0; i < times.Count; i++)
        {
            int delta = times[i] * times[i] - 4 * distances[i];
            if (delta < 0)
            {
                Debug.LogError("no solution for race " + i + ":   " + times[i] + " | " + distances[i]);
                continue;
            }
            else if (delta == 0)
            {
                Debug.LogWarning("weird, only one solution for race " + +i + ":   " + times[i] + " | " + distances[i] + " --> check if it's an int");
                float sol = times[i] /2;
                if (sol == MathF.Floor(sol))
                    result *= 1;
                else
                    result *= 0;  // ???
            }
            else
            {
                double x1 = (times[i] - MathF.Sqrt(delta)) / 2;
                double x2 = (times[i] + MathF.Sqrt(delta)) / 2;

                double solCount = Math.Floor(x2) - Math.Ceiling(x1) + 1;
                result *= solCount;
            }
        }

        return result.ToString();
    }

    protected override string part_2()
    {
        ulong time = ulong.Parse(System.String.Join("", _input.Split('\n')[0].Substring(5).Split(' ', StringSplitOptions.RemoveEmptyEntries)));
        ulong distance = ulong.Parse(System.String.Join("", _input.Split('\n')[1].Substring(9).Split(' ', StringSplitOptions.RemoveEmptyEntries)));

        ulong delta = time * time - 4 * distance;
        if (delta < 0)
        {
            Debug.LogError("no solution for race :  " + time + " | " + distance);
            return "0";
        }
        else if (delta == 0)
        {
            Debug.LogWarning("weird, only one solution for race :   " + time + " | " + distance + " --> check if it's an int");
            float sol = time / 2;
            if (sol == MathF.Floor(sol))
                return "1";
            else
                return "0";
        }
        else
        {
            double x1 = (time - MathF.Sqrt(delta)) / 2;
            double x2 = (time + MathF.Sqrt(delta)) / 2;

            double solCount = Math.Floor(x2) - Math.Ceiling(x1) + 1;
            return solCount.ToString();
        }
    }
}
