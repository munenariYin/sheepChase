using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public sealed class InputManager
{
    private TouchPhase touchPhase;
    private Vector2 position;
    private Vector2 deltaPosition;
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

#if UNITY_EDITOR
        EditorInput();
#else
        AndroidInput();
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
        // 入力があった
        if(Input.GetMouseButtonDown(0) || Input.GetMouseButton(0))
        {
            Vector2 nowMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // 以前に押されていない時の処理
            if(this.touchPhase == TouchPhase.Canceled || this.touchPhase == TouchPhase.Ended)
            {
                this.deltaPosition = Vector2.zero;
                this.position = nowMousePosition;
                this.touchPhase = TouchPhase.Began;
            }
            else
            {
                this.deltaPosition = nowMousePosition - this.position;;
                this.position = nowMousePosition;
                if (this.deltaPosition != Vector2.zero)
                {
                    this.touchPhase = TouchPhase.Moved;
                }
                else
                {
                    this.touchPhase = TouchPhase.Stationary;
                }
            }            
            return;
        }
        // 入力が終わった
        else if (Input.GetMouseButtonUp(0))
        {
            this.deltaPosition = Vector2.zero;
            this.touchPhase = TouchPhase.Canceled;            
        }
        else
        {
            this.deltaPosition = Vector2.zero;
        }
    }
}
