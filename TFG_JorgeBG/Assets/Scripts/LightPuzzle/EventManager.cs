using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static event Action RecalculateLine;
    public static event Action ClearLine;
    public static event Action ReceptorHit;
    public static event Action PressButton;
    public static event Action CompleteLevel;

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
    
    public static void OnReceptorHit()
    {
        if (ReceptorHit != null)
        {
            ReceptorHit();
        }
    }

    public static void OnPressButton()
    {
        if (PressButton != null)
        {
            PressButton();
        }
    }

    public static void OnCompleteLevel() 
    { 
        if(CompleteLevel != null)
        {
            CompleteLevel();
        }
    }
}
