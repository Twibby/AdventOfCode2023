using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Day_2023_13 : DayScript2023
{
    protected override string part_1()
    {
        long result = 0;
        foreach (string instruction in _input.Split("\n\n", StringSplitOptions.RemoveEmptyEntries))
        {
            List<string> datas = instruction.Split('\n').ToList();

            int score = 0;
            // check horizontal reflection for each line
            for (int i = 0; i < datas.Count -1; i++)
            {
                if (isSymetrical(datas, i))
                {
                    score = (i + 1);
                    break;
                }
            }

            if (score > 0)
            {
                result += (long)(100 * score);
                continue;
            }

            datas = pivotDatas(datas);
            // check vertical reflection, (actually we're looking for horizontal reflection after pivoting datas)
            for (int i = 0; i < datas.Count - 1; i++)
            {
                if (isSymetrical(datas, i))
                {
                    score = (i + 1);
                    break;
                }
            }

            if (score > 0)
                result += score;
            else
                Debug.LogError("no symetry found !! " + instruction);
        }

        return result.ToString();
    }

    bool isSymetrical(List<string> datas, int lineAboveindex)
    {
        for (int i = 0; i < datas.Count; i++)
        {
            if (lineAboveindex + i + 1 >= datas.Count || lineAboveindex - i < 0)
                return true;

            if (datas[lineAboveindex + i + 1] != datas[lineAboveindex - i])
                return false;
        }

        Debug.LogError("should not be there...");
        return false;
    }

    /// <summary>
    /// 1 2 3             1 4 7
    /// 4 5 6   becomes   2 5 8
    /// 7 8 9             3 6 9
    /// </summary>
    /// <param name="datas">List of string to pivot</param>
    /// <returns>pivoted list</returns>
    List<string> pivotDatas(List<string> datas)
    {
        List<string> result = new List<string>(datas[0].Length);
        for (int i = 0; i < datas.Count; i++)
        {
            for (int j = 0; j < datas[0].Length; j++)
            {
                if (i == 0)
                {
                    result.Add(datas[i][j].ToString());
                }
                else
                {
                    string tmp = result[j];
                    tmp += datas[i][j];
                    result[j] = tmp;
                }
            }
        }

        return result;
    }

    protected override string part_2()
    {
        long result = 0;
        foreach (string instruction in _input.Split("\n\n", StringSplitOptions.RemoveEmptyEntries))
        {
            List<string> datas = instruction.Split('\n').ToList();

            int score = 0;
            // check horizontal reflection
            for (int i = 0; i < datas.Count - 1; i++)
            {
                if (isAlmostSymetrical(datas, i))
                {
                    score = (i + 1);
                    break;
                }
            }

            if (score > 0)
            {
                result += (long)(100 * score);
                continue;
            }

            datas = pivotDatas(datas);
            // check vertical reflection, (actually we're looking for horizontal reflection after pivoting datas)
            for (int i = 0; i < datas.Count - 1; i++)
            {
                if (isAlmostSymetrical(datas, i))
                {
                    score = (i + 1);
                    break;
                }
            }

            if (score > 0)
                result += score;
            else
                Debug.LogError("no symetry found !! " + instruction);
        }

        return result.ToString();
    }

    bool isAlmostSymetrical(List<string> datas, int leftColumn)
    {
        int diffCount = 0;
        for (int i = 0; i < datas.Count; i++)
        {
            if (leftColumn + i + 1 >= datas.Count || leftColumn - i < 0)
                return diffCount == 1;

            diffCount += stringDifferencesCount(datas[leftColumn + i + 1], datas[leftColumn - i]);
            if (diffCount > 1)
                return false;
        }

        Debug.LogError("should not be there...");
        return false;
    }

    int stringDifferencesCount(string s1, string s2)
    {
        int count = 0;
        for (int i = 0; i < Mathf.Min(s1.Length, s2.Length); i++)
        {
            if (s1[i] != s2[i])
                count++;
        }
        return count;
    }
}
