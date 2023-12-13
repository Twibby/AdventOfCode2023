using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Day_2023_12 : DayScript2023
{
    protected override string part_1()
    {
        long result = 0;
        foreach (string instruction in _input.Split('\n'))
        {
            string[] datas = instruction.Split(' ');
            List<int> pattern = datas[1].Split(',').Select(x => int.Parse(x)).ToList();
            string springsLine = datas[0];

            // First try to fill mandatory spaces
            // For example ????? 3 can be changed into ??#?? because in every possibilities, middle char is always part of the group of 3
            int sum = pattern.Sum() + pattern.Count -1;
            if (springsLine.Length == sum)  // only 1 pattern available if same length as sum, eg ???? 1,2 has to be #.##
            {
                result += 1;
                continue;
            }

            springsLine = replaceObviousChars(springsLine, pattern);
            //Debug.LogWarning("Start instruction : " + instruction);
            int cnt = countPossiblePatterns("", springsLine, pattern);
            //Debug.LogWarning(instruction + " -> " + cnt + "arrangements");

            result += cnt;
        }

        return result.ToString();
    }

    string replaceObviousChars(string springsLine, List<int> pattern)
    {
        string result = springsLine;
        int sum = pattern.Sum() + pattern.Count - 1;
        for (int i = 0; i < pattern.Count; i++)
        {
            if (sum + pattern[i] > result.Length)  // ?????? 3,1  -> sum = 5  +3 = 8 so we know 2nd&3rd chars are # -> ?##???
            {
                int minIndex = 0, maxIndex = 0;
                for (int j = 0; j <= i; j++)
                {
                    minIndex += pattern[j] + 1;
                }
                minIndex--; // -1 for last useless +1

                maxIndex = result.Length - (sum - minIndex + pattern[i]);      // ????###????? 1,5,2 -> sum 9; minIndex = 5 

                string tmp = "";
                for (int j = maxIndex; j < minIndex; j++)
                {
                    tmp += '#';
                }
                result = result.Substring(0, maxIndex) + tmp + result.Substring(minIndex);
            }
        }

        return result;
    }

    int countPossiblePatterns(string head, string tail, List<int> pattern)
    {
        //Debug.Log(head + "  " + tail);
        if (tail == "")
        {
            List<int> headGroups = head.Split('.', StringSplitOptions.RemoveEmptyEntries).Select(x => x.Length).ToList();
            //Debug.Log(" Pattern is " + String.Join(",", pattern) + " || result is " + String.Join(",", headGroups) + "\n --> " + areListEquals(headGroups, pattern).ToString());
            return areListEquals(headGroups, pattern) ? 1 : 0;
        }

        if (!isListIncluded(head.Split('.', StringSplitOptions.RemoveEmptyEntries).Select(x => x.Length).ToList(), pattern))
        {
            //Debug.Log("No need to go further, skip | " + String.Join(",", head.Split('.', StringSplitOptions.RemoveEmptyEntries).Select(x => x.Length)));
            return 0;
        }
        
        switch (tail[0])
        {
            case '.':
            case '#':
                return countPossiblePatterns(head + tail[0], tail[1..], pattern);
            case '?':
                return countPossiblePatterns(head + "#", tail[1..], pattern) + countPossiblePatterns(head + ".", tail[1..], pattern);
            default:
                Debug.LogError("wtf ?? " + tail + " | " + head + " | " + String.Join(",", pattern));
                return 0;
        }
    }

    bool areListEquals(List<int> list1, List<int> list2)
    {
        if (list1.Count != list2.Count)
            return false;

        for (int i = 0; i < list1.Count; i++)
        {
            if (list1[i] != list2[i])
                return false;
        }
        return true;
    }

    bool isListIncluded(List<int> list1, List<int> list2)
    {
        if (list1.Count == 0)
            return true;

        if (list1.Count > list2.Count)
            return false;

        for (int i = 0; i < list1.Count -1; i++)
        {
            if (list1[i] != list2[i])
                return false;
        }

        return list1[list1.Count - 1] <= list2[list1.Count - 1];
    }

    protected override string part_2()
    {
        StartCoroutine(coP2());
        return "";
    }
    public bool stop = false;
    IEnumerator coP2()
    {
        yield return new WaitForEndOfFrame();
        long result = 0;
        int index = 0;
        foreach (string instruction in _input.Split('\n'))
        {
            string[] datas = instruction.Split(' ');
            List<int> pattern = datas[1].Split(',').Select(x => int.Parse(x)).ToList();
            string springsLine = datas[0];

            List<int> fullPattern = new List<int>();
            fullPattern.AddRange(pattern);
            fullPattern.AddRange(pattern);
            fullPattern.AddRange(pattern);
            fullPattern.AddRange(pattern);
            fullPattern.AddRange(pattern);

            springsLine = springsLine + "?" + springsLine + "?" + springsLine + "?" + springsLine + "?" + springsLine;

            // First try to fill mandatory spaces
            // For example ????? 3 can be changed into ??#?? because in every possibilities, middle char is always part of the group of 3
            int sum = fullPattern.Sum() + fullPattern.Count - 1;
            if (springsLine.Length == sum)  // only 1 pattern available if same length as sum, eg ???? 1,2 has to be #.##
            {
                result += 1;
                continue;
            }

            springsLine = replaceObviousChars(springsLine, fullPattern);
            yield return new WaitForEndOfFrame();

            //Debug.LogWarning("Start instruction : " + instruction);
            int cnt = countPossiblePatterns("", springsLine, fullPattern);
            //Debug.LogWarning(instruction + " -> " + cnt + "arrangements");

            result += cnt;
            yield return new WaitForEndOfFrame();
            Debug.Log(index);
            yield return new WaitForSeconds(0.05f);
            if (stop)
                break;

            index++;

        }
        yield return new WaitForEndOfFrame();

        Debug.LogError(result.ToString());
    }
}
