using System;
using UnityEngine;

public class ChangeCursorPickaxe : MonoBehaviour
{
    public Texture2D objectCursor;

    protected virtual void OnMouseEnter()
    {
        if (EndGame.disableAll == true)
        {
            return;
        }
        if(Inventory.Pickaxe != 0)
        {
        Cursor.SetCursor(objectCursor, Vector2.zero, CursorMode.Auto);
        }
    }

    protected virtual void OnMouseExit()
    {
        if (EndGame.disableAll == true)
        {
            return;
        }
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }
}
