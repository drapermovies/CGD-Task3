using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BGMManager
{
    static bool BGMstart;
    static bool BGMend;

    public static bool GetBGMstart()
    {
        return BGMstart;
    }

    public static void SetBGMstart(bool newBGMstart)
    {
        BGMstart = newBGMstart;
    }

    public static bool GetBGMend()
    {
        return BGMend;
    }

    public static void SetBGMend(bool newBGMend)
    {
        BGMend = newBGMend;
    }
}
