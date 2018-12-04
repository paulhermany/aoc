using System;
using System.Collections.Generic;
using System.Linq;
using Hermany.AoC.Common;

namespace Hermany.AoC._2018._04
{
    public class Solution : ISolution
    {
        public string Part1(params string[] input)
        {
            var sleepStats = GetSleepStats(input);

            // strategy 1: sort by guard with the most number of sleeping minutes
            var id = sleepStats.OrderByDescending(_ => _.Value.Sum(m => m.Value)).First().Key;
            // get the most sleeping minute for the guard with the most sleep time
            var minute = sleepStats[id].OrderByDescending(_ => _.Value).First().Key;
            
            return (id * minute).ToString();

        }
        
        public string Part2(params string[] input)
        {
            var sleepStats = GetSleepStats(input);

            // strategy 2: sort by guard with the most frequent single minute sleeping
            // at least one of the guards in the input didn't have any sleep time
            var id = sleepStats.Where(_ => _.Value.Values.Count > 0).OrderByDescending(_ => _.Value.Values.Max()).First().Key;
            // get the sleeping minute for the guard found
            var minute = sleepStats[id].OrderByDescending(_ => _.Value).First().Key;

            return (id * minute).ToString();
        }

        private Dictionary<int, Dictionary<int, int>> GetSleepStats(string[] input)
        {
            // first sort the log entries
            var logEntries = input.ToList();
            logEntries.Sort();

            // run through all log entries in chronological order, keeping track of the "current" guard id
            var guardId = 0;

            // store sleep stats in a dictionary of dictionaries to capture the number of sleep occurrences per minute per guard
            var sleepStats = new Dictionary<int, Dictionary<int, int>>();

            // keep track of the last timestamp
            // when the guard wakes up, walk forward minute by minute from the last timestamp adding to the sleep stats
            var lastTimestamp = string.Empty;

            foreach (var logEntry in logEntries)
            {
                // basic parsing
                var splitIndex = logEntry.IndexOf(']');
                var timestamp = logEntry.Substring(1, splitIndex - 1);
                var tokens = logEntry.Substring(splitIndex + 2).Split(' ');

                // add the guard to the dictionary if it doesn't exist
                if (tokens[0] == "Guard")
                {
                    guardId = int.Parse(tokens[1].Substring(1));
                    if (!sleepStats.ContainsKey(guardId))
                        sleepStats.Add(guardId, new Dictionary<int, int>());
                }

                // if the guard wakes up, walk forward from the previous timestamp to the current timestamp minute by minute
                // increment the specific minute counter
                if (tokens[0] == "wakes")
                {
                    // parse the timestamps
                    var current = DateTime.Parse(timestamp);
                    var previous = DateTime.Parse(lastTimestamp);

                    // begin walking forward minute by minute
                    while (DateTime.Compare(previous, current) == -1)
                    {
                        // increment the counter for the specific minute
                        if (!sleepStats[guardId].ContainsKey(previous.Minute))
                            sleepStats[guardId].Add(previous.Minute, 0);
                        sleepStats[guardId][previous.Minute]++;

                        // walk forward to the next minute
                        previous = previous.AddMinutes(1);
                    }
                }

                // save current timestamp as previous and continue
                lastTimestamp = timestamp;
            }

            return sleepStats;
        }
    }
}
