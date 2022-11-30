using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryCard : MonoBehaviour
{
    // Stores back part of card, which is deactivated on click
    [SerializeField]
    private GameObject cardBack;

    [SerializeField]
    private SceneController controller;

    private int _id;
    
    // read function (getter for id)
    public int getId
    {
        get { return _id; }
    }

    // Called in code by programmer
    public void SetCard(int id, Sprite image)
    {
        _id = id;
        GetComponent<SpriteRenderer>().sprite = image;
    }

    // Is called on click at the object
    public void OnMouseDown()
    {
        // First check if card active.
        // Then check is we can reveal card
        if (cardBack.activeSelf && controller.canReveal)
        {
            cardBack.SetActive(false);
            controller.CardRevealed(this);
        }
    }

    public void Unreveal()
    {
        // make card back active
        cardBack.SetActive(true);
    }
}
