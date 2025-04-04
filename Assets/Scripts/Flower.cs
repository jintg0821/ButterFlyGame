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

    void Start()
    {
        var flowerColor = gameObject.GetComponent<Image>();
        switch (flowerType)
        {
            case FlowerType.Red:
                flowerColor.color = Color.red;
                break;
            case FlowerType.Orange:
                flowerColor.color = new Color(1.0f, 0.5f, 0.0f);
                break;
            case FlowerType.Yellow:
                flowerColor.color = Color.yellow;
                break;
            case FlowerType.Green:
                flowerColor.color = Color.green;
                break;
            case FlowerType.Blue:
                flowerColor.color = Color.blue;
                break;
            case FlowerType.Pink:
                flowerColor.color = new Color(1.0f, 0.75f, 0.8f);
                break;
            case FlowerType.Purple:
                flowerColor.color = new Color(0.6f, 0.2f, 0.8f);
                break;
        }
    }
    
    void Update()
    {
        
    }
}
