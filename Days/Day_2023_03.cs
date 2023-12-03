using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Day_2023_03 : DayScript2023
{
    string[] datas;

    protected override string part_1()
    {
        int result = 0;
        datas = _input.Split('\n');
        for( int i = 0; i < datas.Length; i++)
        {
            for (int j = 0; j < datas[i].Length; j++)
            {
                if (datas[i][j] >= '0' && datas[i][j] <= '9')
                {
                    int endIndex = -1;
                    for (int cnt = j +1; cnt < datas[i].Length; cnt++)
                    {
                        if (datas[i][cnt] < '0' || datas[i][cnt] > '9')
                        {
                            endIndex = cnt;
                            break;
                        }
                    }

                    int number = 0;
                    if (endIndex == -1) //number ends line
                        number = int.Parse(datas[i].Substring(j));
                    else
                        number = int.Parse(datas[i].Substring(j, endIndex - j));

                    if (hasSymbolNeighbour(i, j, endIndex))
                        result += number;

                    j = endIndex > 0 ? endIndex : datas[i].Length;
                }

            }
        }

        return result.ToString();
    }

    bool hasSymbolNeighbour(int lineIndex, int startIndex, int endIndex)
    {
        if (startIndex > 0 && isSymbol(datas[lineIndex][startIndex - 1]))
            return true;
        if (endIndex > 0 && endIndex < datas[lineIndex].Length && isSymbol(datas[lineIndex][endIndex]))
            return true;

        if (lineIndex > 0)
        {
            for (int x = Mathf.Max(0, startIndex-1); x < (endIndex < 0 ? datas[lineIndex-1].Length : Mathf.Min(datas[lineIndex - 1].Length, endIndex + 1)); x++)
            {
                if (isSymbol(datas[lineIndex - 1][x]))
                    return true;
            }
        }

        if (lineIndex < datas.Length - 1)
        {
            for (int x = Mathf.Max(0, startIndex - 1); x < (endIndex < 0 ? datas[lineIndex + 1].Length : Mathf.Min(datas[lineIndex + 1].Length, endIndex + 1)); x++)
            {
                if (isSymbol(datas[lineIndex + 1][x]))
                    return true;
            }
        }

        return false;
    }

    bool isSymbol (char c)
    {
        return c != '.' && (c < '0' || c > '9');
    }

    protected override string part_2()
    {
        long result = 0;
        datas = _input.Split('\n');
        for (int i = 0; i < datas.Length; i++)
        {
            for (int j = 0; j < datas[i].Length; j++)
            {
                if (datas[i][j] == '*')
                {
                    (bool, int) gear = isGear(i, j);
                    if (gear.Item1)
                        result += gear.Item2;
                }
            }
        }
        
        return result.ToString();
    }

    (bool, int) isGear(int lineIndex, int colIndex)
    {
        int count = 0;
        int result = 1;
        if (colIndex > 0 && isDigit(lineIndex, colIndex -1))
        {
            count++;
            string number = datas[lineIndex][colIndex - 1].ToString();
            for (int cnt  = colIndex -2; cnt >= 0; cnt--)
            {
                if (isDigit(lineIndex, cnt))
                    number += datas[lineIndex][cnt].ToString();
                else
                    break;
            }
            result *= int.Parse( reverseStr(number));
        }

        if (colIndex < datas[lineIndex].Length -1 && isDigit(lineIndex, colIndex + 1))
        {
            count++;
            string number = datas[lineIndex][colIndex + 1].ToString();
            for (int cnt = colIndex + 2; cnt < datas[lineIndex].Length; cnt++)
            {
                if (isDigit(lineIndex, cnt))
                    number += datas[lineIndex][cnt].ToString();
                else
                    break;
            }
            result *= int.Parse(number);
        }

        if (lineIndex > 0)
        {
            if (isDigit(lineIndex - 1, colIndex))
            {
                count++;
                string number = datas[lineIndex - 1][colIndex].ToString();
                for (int cnt = colIndex - 1; cnt >= 0; cnt--)
                {
                    if (isDigit(lineIndex - 1, cnt))
                        number += datas[lineIndex - 1][cnt].ToString();
                    else
                        break;
                }
                number = reverseStr(number);
                for (int cnt = colIndex + 1; cnt < datas[lineIndex - 1].Length; cnt++)
                {
                    if (isDigit(lineIndex - 1, cnt))
                        number += datas[lineIndex - 1][cnt].ToString();
                    else
                        break;
                }

                result *= int.Parse(number);
            }
            else
            {
                if (colIndex > 0 && isDigit(lineIndex - 1, colIndex - 1))
                {
                    count++;
                    string number = datas[lineIndex - 1][colIndex - 1].ToString();
                    for (int cnt = colIndex - 2; cnt >= 0; cnt--)
                    {
                        if (isDigit(lineIndex - 1, cnt))
                            number += datas[lineIndex - 1][cnt].ToString();
                        else
                            break;
                    }
                    result *= int.Parse(reverseStr(number));
                }

                if (colIndex < datas[lineIndex - 1].Length - 1 && isDigit(lineIndex - 1, colIndex + 1))
                {
                    count++;
                    string number = datas[lineIndex - 1][colIndex + 1].ToString();
                    for (int cnt = colIndex + 2; cnt < datas[lineIndex - 1].Length; cnt++)
                    {
                        if (isDigit(lineIndex - 1, cnt))
                            number += datas[lineIndex - 1][cnt].ToString();
                        else
                            break;
                    }
                    result *= int.Parse(number);
                }
            }
        }


        if (lineIndex < datas.Length - 1)
        {
            if (isDigit(lineIndex + 1, colIndex))
            {
                count++;
                string number = datas[lineIndex + 1][colIndex].ToString();
                for (int cnt = colIndex - 1; cnt >= 0; cnt--)
                {
                    if (isDigit(lineIndex + 1, cnt))
                        number += datas[lineIndex + 1][cnt].ToString();
                    else
                        break;
                }
                number = reverseStr(number);
                for (int cnt = colIndex + 1; cnt < datas[lineIndex + 1].Length; cnt++)
                {
                    if (isDigit(lineIndex + 1, cnt))
                        number += datas[lineIndex + 1][cnt].ToString();
                    else
                        break;
                }

                result *= int.Parse(number);
            }
            else
            {
                if (colIndex > 0 && isDigit(lineIndex + 1, colIndex - 1))
                {
                    count++;
                    string number = datas[lineIndex + 1][colIndex - 1].ToString();
                    for (int cnt = colIndex - 2; cnt >= 0; cnt--)
                    {
                        if (isDigit(lineIndex + 1, cnt))
                            number += datas[lineIndex + 1][cnt].ToString();
                        else
                            break;
                    }
                    result *= int.Parse(reverseStr(number));
                }

                if (colIndex < datas[lineIndex + 1].Length - 1 && isDigit(lineIndex + 1, colIndex + 1))
                {
                    count++;
                    string number = datas[lineIndex + 1][colIndex + 1].ToString();
                    for (int cnt = colIndex + 2; cnt < datas[lineIndex + 1].Length; cnt++)
                    {
                        if (isDigit(lineIndex + 1, cnt))
                            number += datas[lineIndex + 1][cnt].ToString();
                        else
                            break;
                    }
                    result *= int.Parse(number);
                }
            }
        }

        return ((count == 2), result);
    }

    bool isDigit(int lineIndex, int colIndex)
    {
        char c = datas[lineIndex][colIndex];
        return c >= '0' && c <= '9';
    }

    string reverseStr(string s)
    {
        char[] charArray = s.ToCharArray();
        Array.Reverse(charArray);
        return new string(charArray);
    }
}
