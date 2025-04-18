using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum FlowerType
{
    Red,
    Orange,
    Yellow,
    Green,
    Blue,
    Pink,
    Purple
}

public class Flower : MonoBehaviour
{
    public FlowerType flowerType;
    public Sprite[] flowerSprite;

    void Start()
    {
        var flowerColor = gameObject.GetComponent<Image>();
        switch (flowerType)
        {
            case FlowerType.Red:
                //flowerColor.color = Color.red;
                flowerColor.sprite = flowerSprite[0];
                break;
            case FlowerType.Orange:
                //flowerColor.color = new Color(1.0f, 0.5f, 0.0f);
                flowerColor.sprite = flowerSprite[1];
                break;
            case FlowerType.Yellow:
                //flowerColor.color = Color.yellow;
                flowerColor.sprite = flowerSprite[2];
                break;
            case FlowerType.Green:
                //flowerColor.color = Color.green;
                flowerColor.sprite = flowerSprite[3];
                break;
            case FlowerType.Blue:
                //flowerColor.color = Color.blue;
                flowerColor.sprite = flowerSprite[4];
                break;
            case FlowerType.Pink:
                //flowerColor.color = new Color(1.0f, 0.75f, 0.8f);
                flowerColor.sprite = flowerSprite[5];
                break;
            case FlowerType.Purple:
                //flowerColor.color = new Color(0.6f, 0.2f, 0.8f);
                flowerColor.sprite = flowerSprite[6];
                break;
        }
    }
}
