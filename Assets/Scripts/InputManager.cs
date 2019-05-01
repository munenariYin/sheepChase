using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager
{
    [SerializeField] private float touchPhaseMoveMagunitude = 1.0f;
    private TouchPhase touchPhase;
    private Vector2 deltaPosition;
    private Vector2 position;

    private const int UseTouchArrayNum = 0;

    public Vector2 Position
    {
        get
        {
            return this.position;
        }
    }

    public TouchPhase TouchPhase
    {
        get
        {
            return this.touchPhase;
        }
    }


    public void Update()
    {

#if UNITY_ANDROID
        AndroidInput();
#endif

#if UNITY_EDITOR
        EditorInput();
#endif
        Debug.Log(position);
    }

    private void AndroidInput()
    {
        int touchCount = Input.touchCount;

        if (touchCount == 0)
        {
            return;
        }

        this.touchPhase = Input.GetTouch(UseTouchArrayNum).phase;
        this.deltaPosition = Input.GetTouch(UseTouchArrayNum).deltaPosition;
        this.position = Input.GetTouch(UseTouchArrayNum).position;
    }

    private void EditorInput()
    {
        if(!Input.GetMouseButtonDown(0))
        {
            // 離れた状態の時の管理
            if(this.touchPhase == TouchPhase.Canceled)
            {
                this.touchPhase = TouchPhase.Ended;
            }

            if(this.touchPhase != TouchPhase.Ended)
            {
                this.touchPhase = TouchPhase.Canceled;
            }

            return;
        }

        Vector2 nowMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if(this.touchPhase == TouchPhase.Canceled || this.touchPhase == TouchPhase.Ended)
        {
            this.deltaPosition = Vector2.zero;
            this.position = nowMousePosition;

            this.touchPhase = TouchPhase.Began;

            return;
        }

        Vector2 deltaMousePosition = nowMousePosition - this.position;

        this.deltaPosition = deltaMousePosition;
        this.position = nowMousePosition;
        if (this.touchPhaseMoveMagunitude < deltaMousePosition.magnitude)
        {
            this.touchPhase = TouchPhase.Moved;
        }
        else
        {
            this.touchPhase = TouchPhase.Stationary;
        }
    }



}
