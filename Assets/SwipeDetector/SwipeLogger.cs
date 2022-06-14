﻿using UnityEngine;

public class SwipeLogger : MonoBehaviour
{
    
    private void Awake()
    {
        SwipeDetector.OnSwipe += SwipeDetector_OnSwipe;
    
    }

    private void SwipeDetector_OnSwipe(SwipeData data)
    {
       //Debug.Log("Swipe in Direction: " + data.Direction);
       //Debug.Log("End - Start " + (data.EndPosition - data.StartPosition));
        if(data.Direction == SwipeDirection.Right)
        {
           
        }
    }
}