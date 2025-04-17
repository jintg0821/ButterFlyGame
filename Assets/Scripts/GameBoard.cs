using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameBoard : MonoBehaviour
{
    public BoardSlotGenerator boardSlotGenerator;
    private const int GRID_SIZE = 5;
    int bingoCount;
    public TextMeshProUGUI bingoText;
    public TextMeshProUGUI scoreText;

    public void CheckBingo()
    {
        var slots = boardSlotGenerator.boardSlots;
        if (slots.Count != 25)
        {
            Debug.LogWarning("슬롯 수가 부족함");
            return;
        }

        // 가로 검사
        for (int row = 0; row < GRID_SIZE; row++)
        {
            int startIndex = row * GRID_SIZE;
            FlowerType? type = GetFlowerType(slots[startIndex]);
            if (type == null) continue;

            bool isBingo = true;
            for (int col = 1; col < GRID_SIZE; col++)
            {
                if (GetFlowerType(slots[startIndex + col]) != type)
                {
                    isBingo = false;
                    break;
                }
            }

            if (isBingo)
            {
                bingoCount++;
                for (int col = 0; col < GRID_SIZE; col++)
                {
                    RemoveFlowerFromSlot(slots[startIndex + col]);
                }
            }
        }

        // 세로 검사
        for (int col = 0; col < GRID_SIZE; col++)
        {
            FlowerType? type = GetFlowerType(slots[col]);
            if (type == null) continue;

            bool isBingo = true;
            for (int row = 1; row < GRID_SIZE; row++)
            {
                if (GetFlowerType(slots[row * GRID_SIZE + col]) != type)
                {
                    isBingo = false;
                    break;
                }
            }

            if (isBingo)
            {
                bingoCount++;
                for (int row = 0; row < GRID_SIZE; row++)
                {
                    RemoveFlowerFromSlot(slots[row * GRID_SIZE + col]);
                }
            }
        }

        bingoText.text = $"Bingo : {bingoCount}";
    }

    private FlowerType? GetFlowerType(Transform slot)
    {
        if (slot.childCount == 0) return null;
        var flower = slot.GetChild(0).GetComponent<Flower>();
        return flower?.flowerType;
    }

    private void RemoveFlowerFromSlot(Transform slot)
    {
        if (slot.childCount > 0)
        {
            Destroy(slot.GetChild(0).gameObject);
        }
    }

}