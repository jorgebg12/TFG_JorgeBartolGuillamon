using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static event Action RecalculateLine;
    public static event Action ClearLine;
    public static event Action PressButton;

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

    public static void OnPressButton()
    {
        if (PressButton != null)
        {
            PressButton();
        }
    }
}
