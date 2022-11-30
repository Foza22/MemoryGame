using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButton : MonoBehaviour
{
    [SerializeField]
    private GameObject targetObject;

    [SerializeField]
    private string targetMessage;

    public Color highlightColor = Color.cyan;

    // Is called when cursor hover button
    public void OnMouseEnter()
    {
        // get sprite component from button
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();

        // if successful - change color
        if(sprite)
        {
            sprite.color = highlightColor;
        }
    }

    // Is called when cursor leave button
    public void OnMouseExit()
    {
        // get sprite component from button
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();

        // if successful - change color
        if (sprite)
        {
            sprite.color = Color.white;
        }
    }

    // Is called when mouse button pressed
    public void OnMouseDown()
    {
        // Make button a bit bigger on press
        transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
    }

    // Is called when mouse button released
    public void OnMouseUp()
    {
        // return scale to default
        transform.localScale = Vector3.one;

        // Send command to target
        if(targetObject)
        {
            targetObject.SendMessage(targetMessage);
        }
    }
}
