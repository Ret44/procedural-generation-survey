using UnityEngine;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
public class Stopwatch  {

    public static Stopwatch instance;
    public static System.Diagnostics.Stopwatch timer;
    public static Dictionary<string, long> registeredTimes;

    public static void StartTimer()
    {
        registeredTimes = new Dictionary<string, long>();
        registeredTimes.Clear();
        timer = new System.Diagnostics.Stopwatch();
        timer.Reset();
        timer.Start();        
    }


    public static void StopTimer()
    {
        timer.Stop();
        registeredTimes.Add("Timer stopped", timer.ElapsedMilliseconds);
    }

    public static void RegisterTime(string key)
    {
        registeredTimes.Add(key, timer.ElapsedMilliseconds);
    }

    public static void DebugTimes()
    {
        int i=0; long previous = 0;
        foreach (KeyValuePair<string, long> time in registeredTimes)
        {
            UnityEngine.Debug.Log((i++).ToString() + ":" + time.Key + ":" + time.Value.ToString() + "ms ("+(time.Value-previous).ToString()+"ms)");
            previous = time.Value;
        }
    }

}
