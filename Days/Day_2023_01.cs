using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Day_2023_01 : DayScript2023
{
    protected override string part_1()
    {
        int result = 0;
        foreach (string instruction in _input.Split('\n'))
        {
            string digits = "";
            foreach (char c in instruction)
            {
                if (c >= '0' && c <= '9')
                    digits += c.ToString();
            }

            if (digits.Length > 0)
                result += int.Parse(digits[0].ToString()) * 10 + int.Parse(digits.Last().ToString());
        }

        return result.ToString();
    }

    protected override string part_2()
    {
        int result = 0;
        foreach (string instruction in _input.Split('\n'))
        {
            string digits = "";
            for (int i = 0; i < instruction.Length; i++)
            {
                if (instruction[i] >= '0' && instruction[i] <= '9')
                {
                    digits += instruction[i].ToString();
                    continue;
                }

                int nbr = startingDigit(instruction.Substring(i));
                if (nbr >= 0)
                {
                    digits += nbr.ToString();
                    //i += digitAsLetterLength(nbr) - 1;
                }
            }

            if (digits.Length > 0)
                result += int.Parse(digits.First().ToString()) * 10 + int.Parse(digits.Last().ToString());
        }
        
        return result.ToString();
    }

    int startingDigit(string s)
    {
        if (s.StartsWith("one")) { return 1; }
        if (s.StartsWith("two")) { return 2; }
        if (s.StartsWith("three")) { return 3; }
        if (s.StartsWith("four")) { return 4; }
        if (s.StartsWith("five")) { return 5; }
        if (s.StartsWith("six")) { return 6; }
        if (s.StartsWith("seven")) { return 7; }
        if (s.StartsWith("eight")) { return 8; }
        if (s.StartsWith("nine")) { return 9; }

        return -1;
    }

    int digitAsLetterLength(int digit)
    {
        switch (digit)
        {
            case 1: return 3;
            case 2: return 3;
            case 3: return 5;
            case 4: return 4;
            case 5: return 3;
            case 6: return 3;
            case 7: return 5;
            case 8: return 5;
            case 9: return 4;
            default: Debug.LogError("wtf ! ?"); return -1;
        }
    }
}
