using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Day_2023_10 : DayScript2023
{

    List<List<char>> datas;

    protected override string part_1()
    {
        List<string> instructions = _input.Split('\n').ToList();
        datas = new List<List<char>>();
        foreach (string instr in instructions)
        {
            datas.Add(new List<char>(instr.ToList()));
        }

        // hard dirty way, hard code Start Pos
        // Vector2Int curPos = new Vector2Int(16, 37);

        Vector2Int startPos = getStartPos();
        replaceStartPos(startPos);

        int curWeight = 0;
        Dictionary<Vector2Int, int> weights = new Dictionary<Vector2Int, int>();

        weights.Add(startPos, 0);

        Vector2Int prevPosA = startPos;
        Vector2Int prevPosB = startPos;
        curWeight++;
        (Vector2Int, Vector2Int) firstPos = getNextNeighbour(startPos);
        weights.Add(firstPos.Item1, 1);
        weights.Add(firstPos.Item2, 1);

        Vector2Int curPosA = firstPos.Item1;
        Vector2Int curPosB = firstPos.Item2;
        
        while (curPosA != curPosB && weights.ContainsKey(curPosA) && curWeight < 10000)
        {

            (Vector2Int, Vector2Int) neighboursA = getNextNeighbour(curPosA);
            Vector2Int tmpA = curPosA;
            curPosA = (neighboursA.Item1 == prevPosA ? neighboursA.Item2 : neighboursA.Item1);
            prevPosA = tmpA;

            (Vector2Int, Vector2Int) neighboursB = getNextNeighbour(curPosB);
            Vector2Int tmpB = curPosB;
            curPosB = (neighboursB.Item1 == prevPosB ? neighboursB.Item2 : neighboursB.Item1);
            prevPosB = tmpB;

            curWeight++;
            if (!weights.ContainsKey(curPosA))
                weights.Add(curPosA, curWeight);
            if (!weights.ContainsKey(curPosB))
                weights.Add(curPosB, curWeight);
        }

        return curWeight.ToString();
    }

    Vector2Int getStartPos()
    {
        for (int i = 0; i < datas.Count; i++)
        {
            for (int j = 0; j < datas[i].Count; j++)
            {
                if (datas[i][j] == 'S')
                   return new Vector2Int(i, j);
            }
        }

        Debug.LogError("Didn't find start in grid... :/ ");
        return Vector2Int.zero;
    }

    void replaceStartPos(Vector2Int startPos)
    {
        bool isLeft = startPos.y > 0 && (datas[startPos.x][startPos.y - 1] is '-' or 'L' or 'F');
        bool isUp = startPos.x > 0 && (datas[startPos.x - 1][startPos.y] is '|' or 'F' or '7');
        bool isRight = startPos.y < datas[startPos.x].Count - 1 && (datas[startPos.x][startPos.y + 1] is '-' or 'J' or '7');
        bool isDown = startPos.x < datas.Count - 1 && (datas[startPos.x + 1][startPos.y] is '|' or 'J' or 'L');

        int val = (isLeft ? 1 : 0) + (isUp ? 2 : 0) + (isRight ? 4 : 0) + (isDown ? 8 : 0);
        char c;
        switch (val)
        {
            case 3: c = 'J';break;      // left up
            case 5: c = '-'; break;     // left right
            case 9: c = '7'; break;     // left down
            case 6: c = 'L'; break;     // up right
            case 10: c = '|'; break;    // up down
            case 12: c = 'F'; break;    // right down
            default: Debug.LogError("Start is not a valid pipe"); return;
        }

        List<char> tmp = new List<char>(datas[startPos.x]);
        tmp[startPos.y] = c;
        datas[startPos.x] = tmp;
    }


    (Vector2Int, Vector2Int) getNextNeighbour(Vector2Int curPos)
    {
        switch (datas[curPos.x][curPos.y])
        {
            case '-': return (new Vector2Int(curPos.x, curPos.y - 1), new Vector2Int(curPos.x, curPos.y + 1));
            case '|': return (new Vector2Int(curPos.x -1, curPos.y), new Vector2Int(curPos.x + 1, curPos.y));
            case '7': return (new Vector2Int(curPos.x + 1, curPos.y), new Vector2Int(curPos.x, curPos.y - 1));
            case 'F': return (new Vector2Int(curPos.x + 1, curPos.y), new Vector2Int(curPos.x, curPos.y + 1));            
            case 'J': return (new Vector2Int(curPos.x - 1, curPos.y), new Vector2Int(curPos.x, curPos.y - 1));
            case 'L': return (new Vector2Int(curPos.x - 1, curPos.y), new Vector2Int(curPos.x, curPos.y + 1));
            default:
                Debug.LogError("wtf is that char :" + datas[curPos.x][curPos.y]);
                return (Vector2Int.zero, Vector2Int.zero);
        }
    }

    protected override string part_2()
    {
        List<string> instructions = _input.Split('\n').ToList();
        datas = new List<List<char>>();
        foreach (string instr in instructions)
        {
            datas.Add(new List<char>(instr.ToList()));
        }

        Vector2Int startPos = getStartPos();
        replaceStartPos(startPos);

        List<Vector2Int> loopPipes = new List<Vector2Int>();
        loopPipes.Add(startPos);

        Vector2Int prevPos = startPos;
        (Vector2Int, Vector2Int) firstPos = getNextNeighbour(startPos);
        Vector2Int curPos = firstPos.Item1;

        // Register all pipes of loop in list
        int safetyCount = 0;
        while (curPos != startPos && !loopPipes.Contains(curPos) && safetyCount < 10000)
        {
            loopPipes.Add(curPos);

            (Vector2Int, Vector2Int) neighbours = getNextNeighbour(curPos);
            Vector2Int tmp = curPos;
            curPos = (neighbours.Item1 == prevPos ? neighbours.Item2 : neighbours.Item1);
            prevPos = tmp;
        }

        // Go through all lines, and depending on which side of pipe we are, add 1 or not 
        long result = 0;        
        for (int i = 0; i < datas.Count; i++)
        {
            bool isIn = false;
            char lastCorner = '.';
            for (int j = 0; j < datas[i].Count; j++)
            {
                if (loopPipes.Exists(p => p.x == i && p.y == j))
                {
                    switch (datas[i][j])
                    {
                        case '-':
                            break;
                        case 'L': case 'F': 
                            lastCorner = datas[i][j]; 
                            break;
                        case '7':
                            if (lastCorner == 'L') { isIn = !isIn; }    // if something like L----7 we're changing side, if F----7 we are not
                            break;
                        case 'J':
                            if (lastCorner == 'F') { isIn = !isIn; }    // if something like F----J we're changing side, if L----J we are not
                            break;
                        default:
                            isIn = !isIn;
                            break;
                    }
                }
                else if (isIn)  // not a pipe of the loop, so we're either inside or outside
                    result++;
            }
        }

        return result.ToString();
    }
}
