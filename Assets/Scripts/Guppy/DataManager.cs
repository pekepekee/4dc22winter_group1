using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager
{
    protected static int currentGachaPoint;
    protected static int currentGachaCount;
    protected static bool tutorialMode;
    protected static bool endlessMode;
    protected static List<GachaItem> gachaResult;
    protected static Dictionary<GachaItem, int> gachaResultCount;

    public DataManager()
    {
        gachaResult = new List<GachaItem>();
        gachaResultCount = new Dictionary<GachaItem, int>();
    }

    public static void SetPoint(int point)
    {
        currentGachaPoint = point;
    }

    public static bool UsePoint(int point)
    {
        if(point > currentGachaPoint)
        {
            return false;
        }

        currentGachaPoint -= point;

        return true;
    }

    public static void AddPoint(int point)
    {
        currentGachaPoint += point;
    }

    public static int GetPoint()
    {
        return currentGachaPoint;
    }

    public static void SetGachaCount(int count)
    {
        currentGachaCount = count;
    }

    public static void AddGachaCount(int count)
    {
        currentGachaCount += count;
    }

    public static int GetGachaCount()
    {
        return currentGachaCount;
    }

    public static void SetTutorialMode(bool state)
    {
        tutorialMode = state;
    }

    public static bool IsTutorialMode()
    {
        return tutorialMode;
    }

    public static void StartEndlessMode()
    {
        endlessMode = true;
    }

    public static void EndEndlessMode()
    {
        endlessMode = false;
    }

    public static bool IsEndlessMode()
    {
        return endlessMode;
    }

    public static void ResetGachaItemResult()
    {
        InitializeGachaResult();

        gachaResult.Clear();
        gachaResultCount.Clear();
    }

    public static void AddGachaItemResult(GachaItem item)
    {
        InitializeGachaResult();

        gachaResult.Add(item);
        if (gachaResultCount.ContainsKey(item))
        {
            gachaResultCount[item] += 1;
        }
        else
        {
            gachaResultCount[item] = 1;
        }
    }

    public static int GetGachaItemResultCount(GachaItem item)
    {
        InitializeGachaResult();

        if (gachaResultCount.ContainsKey(item))
        {
            return gachaResultCount[item];
        }

        return 0;
    }

    private static void InitializeGachaResult()
    {
        if(gachaResult == null)
        {
            gachaResult = new List<GachaItem>();
        }
        if(gachaResultCount == null)
        {
            gachaResultCount = new Dictionary<GachaItem, int>();
        }
    }
}
