using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Day_2023_08 : DayScript2023
{
    protected override string part_1()
    {
        Dictionary<string, (string, string)> map = new Dictionary<string, (string, string)>();

        string directions = _input.Split("\n\n")[0];
        foreach (string instruction in _input.Split("\n\n")[1].Split('\n'))
        {
            map.Add(instruction.Substring(0, 3), (instruction.Substring(7, 3), instruction.Substring(12, 3)));
        }

        int step = 0;
        string current = "AAA";
        while (current != "ZZZ" && step < 1000000000)
        {
            current = directions[step % directions.Length] == 'L' ? map[current].Item1 : map[current].Item2;
            step++;
        }
        return step.ToString();
    }

    protected override string part_2()
    {
        Dictionary<string, (string, string)> map = new Dictionary<string, (string, string)>();
        List<string> starts = new List<string>();
        string directions = _input.Split("\n\n")[0];
        foreach (string instruction in _input.Split("\n\n")[1].Split('\n'))
        {
            map.Add(instruction.Substring(0, 3), (instruction.Substring(7, 3), instruction.Substring(12, 3)));
            if (instruction[2] == 'A')
                starts.Add(instruction.Substring(0, 3));
        }

        foreach (string start in starts)
        {
            int patternCount = 0;
            double step = 0;
            string current = start;
            Dictionary<string, List<double>> patterns = new Dictionary<string, List<double>>();
            while (step < 100000000 && patternCount < 100)
            {
                current = directions[(int)(step % directions.Length)] == 'L' ? map[current].Item1 : map[current].Item2;
                step++;

                if (current[2] == 'Z')
                {
                    if (!patterns.ContainsKey(current))
                        patterns.Add(current, new List<double>());

                    patterns[current].Add(step);
                    patternCount++;
                }
            }

            Debug.LogWarning("For start " + start + ", found " + patterns.Count + " patterns :");
            foreach (var pat in patterns)
            {
                double period = pat.Value[2] - pat.Value[1];
                bool isCyclic = true;
                for (int i = 2; i < pat.Value.Count; i++)
                {
                    isCyclic &= (pat.Value[i] - pat.Value[i - 1]) == period;
                    if (!isCyclic)
                        break;
                }
                Debug.Log("Pattern '" + pat.Key + "' is cyclic ? " + isCyclic.ToString() + " with period " + period.ToString());
            }
        }

        // Idea : let's find a pattern for each start 
        // Then compute PPCM (LCM) of every period (took external website)

        return "";
    }
}
