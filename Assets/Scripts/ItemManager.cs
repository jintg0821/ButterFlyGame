using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum ItemType
{
    Remove,
    Change,
    None
}

public class ItemManager : MonoBehaviour
{
    public ItemType itemType;
    public Image itemCursorImage;
    public Sprite[] itemImages;

    public int removeItemCount;
    public int changeItemCount;

    public TextMeshProUGUI removeText;
    public TextMeshProUGUI changeText;

    private GameBoard gameBoard;

    private void Start()
    {
        gameBoard = GetComponent<GameBoard>();
        itemType = ItemType.None;
    }

    void Update()
    {
        if (itemType != ItemType.None)
        {
            Vector2 mousePos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                itemCursorImage.canvas.transform as RectTransform,
                Input.mousePosition,
                itemCursorImage.canvas.worldCamera,
                out mousePos);

            itemCursorImage.rectTransform.anchoredPosition = mousePos;
            itemCursorImage.enabled = true; // 항상 표시
        }
        else
        {
            itemCursorImage.enabled = false; // 아이템 없을 때는 안 보이게
        }
    }

    public void PushItemButton(int n)
    {
        if (itemType == ItemType.None)
        {
            switch (n)
            {
                case 0:
                    if (removeItemCount <= 0) return;

                    itemType = ItemType.Remove;
                    itemCursorImage.sprite = itemImages[0];

                    break;

                case 1:
                    if (changeItemCount <= 0) return;

                    itemType = ItemType.Change;
                    itemCursorImage.sprite = itemImages[1];

                    break;
            }
        }
        else
        {
            switch (itemType)
            {
                case ItemType.Remove:
                    itemType = ItemType.None;
                    break;

                case ItemType.Change:
                    itemType = ItemType.None;
                    break;
            }
        }
    }

    public void IncreaseRemove()
    {
        removeItemCount++;
        removeText.text = removeItemCount.ToString();
    }
    
    public void DecreaseRemove()
    {
        removeItemCount--;
        removeText.text = removeItemCount.ToString();
    }

    public void IncreaseChange()
    {
        changeItemCount++;
        changeText.text = changeItemCount.ToString();
    }
    
    public void DecreaseChange()
    {
        changeItemCount--;
        changeText.text = changeItemCount.ToString();
    }
}
