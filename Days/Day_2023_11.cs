using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Day_2023_11 : DayScript2023
{
    protected override string part_1()
    {
        List<string> instructions = _input.Split('\n').ToList();
        List<List<char>> datas = new List<List<char>>();
        foreach (string instr in instructions)
        {
            datas.Add(new List<char>(instr.ToList()));
        }

        for (int i = 0; i < datas[0].Count; i++)
        {
            bool hasGalaxy = false;
            foreach (List<char> line in datas)
            {
                hasGalaxy |= line[i] == '#';
                if (hasGalaxy)
                    break;
            }

            if (!hasGalaxy)
            {
                Debug.Log("No galaxy at column " + i);
                foreach (List<char> line in datas)
                {
                    line.Insert(i, '.');
                }

                i += 1;
            }

            if (i > 10000)
                break;
        }

        List<List<char>> modifiedDatas = new List<List<char>>();
        foreach (List<char> line in datas)
        {
            if (!line.Exists(x => x == '#'))
                modifiedDatas.Add(new List<char>(line));

            modifiedDatas.Add(new List<char>(line));
        }

        datas = new List<List<char>>(modifiedDatas);

        //Debug.Log(System.String.Join("\n", datas.Select(x => System.String.Join("", x))));

        List<Vector2> pos = new List<Vector2>();
        for (int i = 0; i < datas.Count; i++)
        {
            List<char> line = datas[i];
            for (int j = 0; j < line.Count; j++)
            {
                if (line[j] == '#')
                    pos.Add(new Vector2(i, j));
            }
        }

        long result = 0;
        for (int i = 0; i < pos.Count; i++)
        {
            for (int cnt = 0; cnt < i; cnt++)
            {
                float dist = getDistance(pos[i], pos[cnt]);
                //Debug.Log("Distance between " + (cnt+1).ToString() + " and " + (i+1).ToString() + " = " + dist);
                result += (long)dist;
            }
        }


        return result.ToString();
    }

    float getDistance(Vector2 posA, Vector2 posB)
    {
        return Mathf.Abs(posA.x - posB.x) + Mathf.Abs(posA.y - posB.y);
    }

    List<List<long>> datas = new List<List<long>>();

    protected override string part_2()
    {
        const long ExpandValue = 1000000;

        List<string> instructions = _input.Split('\n').ToList();
        foreach (string instr in instructions)
        {
            datas.Add(new List<long>(instr.Select(c => c == '#' ? (long)0 : (long)1).ToList()));
        }

        for (int i = 0; i < datas[0].Count; i++)
        {
            bool hasNoGalaxy = datas.Select(l => l[i]).All(x => x > 0);
            if (hasNoGalaxy)
            {
                Debug.Log("No galaxy at column " + i);
                foreach (List<long> line in datas)
                {
                    line[i] *= ExpandValue;
                }
            }
        }

        foreach (List<long> line in datas)
        {
            if (line.All(x => x > 0))
            {
                for (int i = 0; i < line.Count; i++)
                {
                    line[i] *= ExpandValue;
                }
            }
        }


        List<Vector2Int> pos = new List<Vector2Int>();
        for (int i = 0; i < datas.Count; i++)
        {
            for (int j = 0; j < datas[i].Count; j++)
            {
                if (datas[i][j] == 0)
                {
                    pos.Add(new Vector2Int(i, j));

                    List<long> tmp = new List<long>(datas[i]);
                    tmp[j] = 1;
                    datas[i] = tmp;
                }
            }
        }

        //if (IsTestInput)
        //    Debug.Log(System.String.Join("\n", datas.Select(x => System.String.Join("\t", x))));

        long result = 0;
        for (int i = 0; i < pos.Count; i++)
        {
            for (int cnt = 0; cnt < i; cnt++)
            {
                long dist = getDistanceP2(pos[i], pos[cnt]);
                //if (IsTestInput)
                //    Debug.Log("Distance between " + (cnt + 1).ToString() + " and " + (i + 1).ToString() + " = " + dist);
                result += dist;
            }
        }

        return result.ToString();
    }

    long getDistanceP2(Vector2Int posA, Vector2Int posB)
    {
        long result = 0;
        
        int startX = Mathf.Min(posA.x, posB.x);
        int endX = Mathf.Max(posA.x, posB.x);
        for (int i = startX+1; i <= endX; i++)
        {
            result += datas[i][posA.y];
        }

        int startY = Mathf.Min(posA.y, posB.y);
        int endY = Mathf.Max(posA.y, posB.y);
        for (int i = startY +1; i <= endY; i++)
        {
            result += datas[posB.x][i];
        }

        return result;
    }
}
