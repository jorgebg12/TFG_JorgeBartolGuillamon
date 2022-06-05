using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static event Action RecalculateLine;
    public static event Action ClearLine;

    public static void OnRecalculateLine()
    {
        if(RecalculateLine != null)
        {
            RecalculateLine();
        }
    }

    public static void OnClearLine()
    {
        if (ClearLine != null)
        {
            ClearLine();
        }
    }
}
