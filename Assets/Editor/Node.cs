using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Node
{
    private Rect nodeRect = new Rect(0.0f, 0.0f, 100.0f, 100.0f);

    public Rect GetRect()
    {
        return this.nodeRect;
    }

    bool holded = false;
    public Node()
    {
    }

    public void Hold(Vector2 _mousePosition)
    {
        if (!this.nodeRect.Contains(_mousePosition)) return;
        this.holded = true;
        Debug.Log("Holded");
    }

    public void Draged(Vector2 _moveDelta)
    {
        this.nodeRect.position += _moveDelta;
    }

    public void Pulled()
    {
        this.holded = false;
        Debug.Log("Release");
    }

}
