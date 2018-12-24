using UnityEngine;
using System;


public class SwipeManager : MonoBehaviour
{
    [Flags]
    public enum SwipeDirection
    {
        None = 0, Left = 1, Right = 2, Up = 4, Down = 8
    }

    private static SwipeManager m_instance;

    public static SwipeManager Instance
    {
        get
        {
            if (!m_instance)
            {
                m_instance = new GameObject("SwipeManager").AddComponent<SwipeManager>();
            }

            return m_instance;
        }
    }


    public SwipeDirection Direction { get; private set; }

    private Vector3 m_touchPosition;
    private float m_swipeResistanceX = Screen.width / 20;
    private float m_swipeResistanceY = Screen.height / 40;

    private void Start()
    {
        if (m_instance != this)
        {
            Debug.LogError("Don't instantiate SwipeManager manually");
            DestroyImmediate(this);
        }

        Debug.Log(m_swipeResistanceX);
        Debug.Log(m_swipeResistanceY);
    }

    private void Update()
    {
        Direction = SwipeDirection.None;

        if (Input.GetMouseButtonDown(0))
        {
            m_touchPosition = Input.mousePosition;

            Debug.Log(m_touchPosition);
        }

        if (Input.GetMouseButtonUp(0))
        {
            Vector2 deltaSwipe = m_touchPosition - Input.mousePosition;

            Debug.Log(deltaSwipe);

            if (Mathf.Abs(deltaSwipe.x) > Mathf.Abs(deltaSwipe.y))
                deltaSwipe.y = 0;
            else
                deltaSwipe.x = 0;

            if (Mathf.Abs(deltaSwipe.x) > m_swipeResistanceX)
            {
                // Swipe on the X axis
                Direction |= (deltaSwipe.x < 0) ? SwipeDirection.Right : SwipeDirection.Left;

            }

            if (Mathf.Abs(deltaSwipe.y) > m_swipeResistanceY)
            {
                // Swipe on the Y axis
                Direction |= (deltaSwipe.y < 0) ? SwipeDirection.Up : SwipeDirection.Down;


            }
        }

    }

    public bool IsSwiping(SwipeDirection dir)
    {
        return (Direction & dir) == dir;
    }

}