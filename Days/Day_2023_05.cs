using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Day_2023_05 : DayScript2023
{
    protected override string part_1()
    {
        long minLoc = long.MaxValue;
        string[] instructions = _input.Split("\n\n");

        List<long> seeds = instructions[0].Substring(7).Split(' ').Select(x => long.Parse(x)).ToList();
        Dictionary<long, (long, long)> seedToSoilMap = new Dictionary<long, (long, long)>();
        Dictionary<long, (long, long)> soilToFertiMap = new Dictionary<long, (long, long)>();
        Dictionary<long, (long, long)> fertiToWaterMap = new Dictionary<long, (long, long)>();
        Dictionary<long, (long, long)> waterToLightMap = new Dictionary<long, (long, long)>();
        Dictionary<long, (long, long)> lightToTempMap = new Dictionary<long, (long, long)>();
        Dictionary<long, (long, long)> tempToHumidMap = new Dictionary<long, (long, long)>();
        Dictionary<long, (long, long)> humidToLocMap = new Dictionary<long, (long, long)>();

        // Parsing
        for (long i = 1; i<instructions.Length; i++)
        {
            Dictionary<long, (long, long)> maps = new Dictionary<long, (long, long)>();
            string[] datas = instructions[i].Split("\n");
            for (long j = 1; j < datas.Length; j++)
            {
                long[] values = datas[j].Split(' ').Select(x => long.Parse(x)).ToArray();
                maps.Add(values[1], (values[0], values[2]));
            }

            switch (datas[0])
            {
                case "seed-to-soil map:":           seedToSoilMap = new Dictionary<long, (long, long)>(maps); break;
                case "soil-to-fertilizer map:":     soilToFertiMap = new Dictionary<long, (long, long)>(maps); break;
                case "fertilizer-to-water map:":    fertiToWaterMap = new Dictionary<long, (long, long)>(maps); break;
                case "water-to-light map:":         waterToLightMap = new Dictionary<long, (long, long)>(maps); break;
                case "light-to-temperature map:":   lightToTempMap = new Dictionary<long, (long, long)>(maps); break;
                case "temperature-to-humidity map:": tempToHumidMap = new Dictionary<long, (long, long)>(maps); break;
                case "humidity-to-location map:":   humidToLocMap = new Dictionary<long, (long, long)>(maps); break;
                default:    Debug.LogError("wtf " + datas[0]); break;
            }
        }


        foreach (long seed in seeds)
        {
            long soil = getDestination(seed, seedToSoilMap);
            long ferti = getDestination(soil, soilToFertiMap);
            long water = getDestination(ferti, fertiToWaterMap);
            long light = getDestination(water, waterToLightMap);
            long temp = getDestination(light, lightToTempMap);
            long humid = getDestination(temp, tempToHumidMap);
            long loc = getDestination(humid, humidToLocMap);

            Debug.Log($"seed {seed} -> soil {soil} -> ferti {ferti} -> water {water} -> light {light} -> temp {temp} -> humid {humid} -> loc {loc}");
            if (loc < minLoc)
                minLoc = loc;
        }


        return minLoc.ToString();
    }

    long getDestination(long source, Dictionary<long, (long, long)> map)
    {
        foreach (var match in map)
        {
            if (source < match.Key)
                continue;

            if (source >= match.Key + match.Value.Item2)
                continue;

            return (source - match.Key) + match.Value.Item1;
        }

        return source;
    }

    protected override string part_2()
    {
        long minLoc = long.MaxValue;
        string[] instructions = _input.Split("\n\n");

        List<long> seeds = instructions[0].Substring(7).Split(' ').Select(x => long.Parse(x)).ToList();
        Dictionary<long, (long, long)> seedToSoilMap = new Dictionary<long, (long, long)>();
        Dictionary<long, (long, long)> soilToFertiMap = new Dictionary<long, (long, long)>();
        Dictionary<long, (long, long)> fertiToWaterMap = new Dictionary<long, (long, long)>();
        Dictionary<long, (long, long)> waterToLightMap = new Dictionary<long, (long, long)>();
        Dictionary<long, (long, long)> lightToTempMap = new Dictionary<long, (long, long)>();
        Dictionary<long, (long, long)> tempToHumidMap = new Dictionary<long, (long, long)>();
        Dictionary<long, (long, long)> humidToLocMap = new Dictionary<long, (long, long)>();

        // Parsing
        for (long i = 1; i < instructions.Length; i++)
        {
            Dictionary<long, (long, long)> maps = new Dictionary<long, (long, long)>();
            string[] datas = instructions[i].Split("\n");
            for (long j = 1; j < datas.Length; j++)
            {
                long[] values = datas[j].Split(' ').Select(x => long.Parse(x)).ToArray();
                maps.Add(values[1], (values[0], values[2]));
            }

            switch (datas[0])
            {
                case "seed-to-soil map:": seedToSoilMap = new Dictionary<long, (long, long)>(maps); break;
                case "soil-to-fertilizer map:": soilToFertiMap = new Dictionary<long, (long, long)>(maps); break;
                case "fertilizer-to-water map:": fertiToWaterMap = new Dictionary<long, (long, long)>(maps); break;
                case "water-to-light map:": waterToLightMap = new Dictionary<long, (long, long)>(maps); break;
                case "light-to-temperature map:": lightToTempMap = new Dictionary<long, (long, long)>(maps); break;
                case "temperature-to-humidity map:": tempToHumidMap = new Dictionary<long, (long, long)>(maps); break;
                case "humidity-to-location map:": humidToLocMap = new Dictionary<long, (long, long)>(maps); break;
                default: Debug.LogError("wtf " + datas[0]); break;
            }
        }

        // Warning!
        // This is kind of bruteforce, dumb way.
        // But it worked in ~2.5h (while i was working on something else), with no high RAM or CPU needed so i'm happy with it
        for (int i = 0; i < seeds.Count; i += 2)
        {
            for (long seed = seeds[i]; seed < seeds[i] + seeds[i + 1]; seed++)
            {
                long soil = getDestination(seed, seedToSoilMap);
                long ferti = getDestination(soil, soilToFertiMap);
                long water = getDestination(ferti, fertiToWaterMap);
                long light = getDestination(water, waterToLightMap);
                long temp = getDestination(light, lightToTempMap);
                long humid = getDestination(temp, tempToHumidMap);
                long loc = getDestination(humid, humidToLocMap);

                if (loc < minLoc)
                    minLoc = loc;
            }
        }


        return minLoc.ToString();
    }
}
