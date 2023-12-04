using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Day_2023_04 : DayScript2023
{
    protected override string part_1()
    {
        long result = 0;
        foreach (string instruction in _input.Split('\n'))
        {
            string[] datas = instruction.Substring(instruction.IndexOf(':') +1).Split('|');
            if (datas.Length != 2) { Debug.LogError("wtf ! " + instruction); continue; }

            datas[0].Split(' ', StringSplitOptions.RemoveEmptyEntries);
            List<int> winningNumbers = datas[0].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToList();
            List<int> pickedNumbers = datas[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToList();

            int matches = 0;
            foreach (int nb in pickedNumbers)
            {
                if (winningNumbers.Contains(nb))
                    matches++;
            }

            if (matches > 0)
                result += (long)Mathf.Pow(2, matches - 1);
        }

        return result.ToString();
    }

    protected override string part_2()
    {
        Dictionary<int, int> copies = new Dictionary<int, int>();
        string[] instructions = _input.Split('\n');
        for (int i = 0; i < instructions.Length; i++)
        {
            string[] datas = instructions[i].Substring(instructions[i].IndexOf(':') + 1).Split('|');
            if (datas.Length != 2) { Debug.LogError("wtf ! " + instructions[i]); continue; }

            datas[0].Split(' ', StringSplitOptions.RemoveEmptyEntries);
            List<int> winningNumbers = datas[0].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToList();
            List<int> pickedNumbers = datas[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToList();

            int matches = 0;
            foreach (int nb in pickedNumbers)
            {
                if (winningNumbers.Contains(nb))
                    matches++;
            }

            if (!copies.ContainsKey(i))
                copies.Add(i, 0);

            copies[i] += 1;

            if (matches > 0)
            {
                for (int j = 0; j < matches; j++)
                {
                    if (!copies.ContainsKey(i + 1 + j))
                        copies.Add(i + 1 + j, 0);

                    copies[i + 1 + j] += copies[i];
                }
            }

            //if (i < 5)
            //{
                //Debug.Log($"i = {i}, instruction is {instructions[i]}" + System.Environment.NewLine + $"Has {matches} matches, then copies is : ");
                //Debug.Log(System.String.Join(" || ", copies.Select(x => x.Key + "->" + x.Value)));
            //}
        }

        long result = 0;
        foreach (var copy in copies.Values) { result += copy; }
        return result.ToString();
    }
}
