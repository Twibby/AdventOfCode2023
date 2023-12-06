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

        long numberOfWays = 0;
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

            }
            else
            {

            }


        }

        return numberOfWays.ToString();
    }

    protected override string part_2()
    {
        foreach (string instruction in _input.Split('\n'))
        {

        }
        
        return base.part_2();
    }
}
