using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Day_2023_02 : DayScript2023
{
    protected override string part_1()
    {
        int result = 0;
        foreach (string instruction in _input.Split('\n'))
        {
            string[] datas = instruction.Split(": ");
            if (datas.Length != 2) { Debug.LogError("Instruction doesnt match pattern : " + instruction); continue; }

            int id = int.Parse(datas[0].Split(' ')[1]);
            bool gameOk = true;
            foreach (string cubes in datas[1].Split(new string[] { ", ", "; " }, StringSplitOptions.RemoveEmptyEntries))
            {
                int cubeCount = int.Parse(cubes.Split(' ')[0]);
                string color = cubes.Split(' ')[1];
                
                switch (color)
                {
                    case "red": if (cubeCount > 12) { gameOk = false; } break;
                    case "green": if (cubeCount > 13) { gameOk = false; } break;
                    case "blue": if (cubeCount > 14) { gameOk = false; } break;
                    default: Debug.LogWarning("wtf : " + cubes);break;
                }

                if (!gameOk)
                    break;
            }

            if (gameOk)
                result += id;
        }

        return result.ToString();
    }

    protected override string part_2()
    {
        long result = 0;
        foreach (string instruction in _input.Split('\n'))
        {
            string[] datas = instruction.Split(": ");
            if (datas.Length != 2) { Debug.LogError("Instruction doesnt match pattern : " + instruction); continue; }

            int minRed = 0, minGreen = 0, minBlue = 0;
            foreach (string cubes in datas[1].Split(new string[] { ", ", "; " }, StringSplitOptions.RemoveEmptyEntries))
            {
                int cubeCount = int.Parse(cubes.Split(' ')[0]);
                string color = cubes.Split(' ')[1];

                switch (color)
                {
                    case "red": minRed =  Mathf.Max(minRed, cubeCount); break;
                    case "green": minGreen = Mathf.Max(minGreen, cubeCount); break;
                    case "blue": minBlue = Mathf.Max(minBlue, cubeCount); break;
                    default: Debug.LogWarning("wtf : " + cubes); break;
                }
            }

            result += minRed * minGreen * minBlue;
        }
        
        return result.ToString();
    }
}
