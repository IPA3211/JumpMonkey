﻿using System;
using UnityEngine;

public abstract class SwipeReceiver : MonoBehaviour
{
    //override this
    protected virtual void OnSwipeLeft()
    {
        Debug.Log("Left");
    }

    //override this
    protected virtual void OnSwipeRight()
    {
        Debug.Log("Right");
    }

    //override this
    protected virtual void OnSwipeUp()
    {
        Debug.Log("Up");
    }

    //override this
    protected virtual void OnSwipeDown()
    {
        Debug.Log("Down");
    }

    protected virtual void FixedUpdate()
    {
        if (SwipeManager.Instance.IsSwiping(SwipeManager.SwipeDirection.Right))
        {

            OnSwipeRight();
        }

        if (SwipeManager.Instance.IsSwiping(SwipeManager.SwipeDirection.Left))
        {
            OnSwipeLeft();
        }

        if (SwipeManager.Instance.IsSwiping(SwipeManager.SwipeDirection.Up))
        {
            OnSwipeUp();
        }

        if (SwipeManager.Instance.IsSwiping(SwipeManager.SwipeDirection.Down))
        {
            OnSwipeDown();
        }
    }


}