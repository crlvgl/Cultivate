using System;
using UnityEngine;

public class ChangeCursorAxe: MonoBehaviour
{
    public Texture2D objectCursor;

    protected virtual void OnMouseEnter()
    {
        if(Inventory.Relic != 0)
        {
        Cursor.SetCursor(objectCursor, Vector2.zero, CursorMode.Auto);
        }
    }

    protected virtual void OnMouseExit()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }
}
