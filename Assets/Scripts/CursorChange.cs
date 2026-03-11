using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorChange : MonoBehaviour
{
    [SerializeField] Texture2D cursorTexture;


    void Start()
    {
        Cursor.SetCursor(cursorTexture, new Vector2(4,2), CursorMode.Auto);
    }
}
