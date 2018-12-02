using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{	
}

[System.Serializable]
public struct Vegetable
{
    public string itemID;
    public Sprite itemSprite;
    public Sprite choppedItemSprite;
}
