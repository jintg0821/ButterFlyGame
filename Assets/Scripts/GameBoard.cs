using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameBoard : MonoBehaviour
{
    public BoardSlotGenerator boardSlotGenerator;
    private const int GRID_SIZE = 5;
    private int bingoCount;
    private int score;
    public TextMeshProUGUI bingoText;
    public TextMeshProUGUI scoreText;

    private ItemManager itemManager;

    void Start()
    {
        itemManager = GetComponent<ItemManager>();
    }

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
                score += 250;
                scoreText.text = $"Score : {score}";
                for (int col = 0; col < GRID_SIZE; col++)
                {
                    RemoveFlowerFromSlot(slots[startIndex + col]);
                }
                RandomItem();
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
                score += 250;
                scoreText.text = $"Score : {score}";
                for (int row = 0; row < GRID_SIZE; row++)
                {
                    RemoveFlowerFromSlot(slots[row * GRID_SIZE + col]);
                }
                RandomItem();
            }
        }

        // 대각선 ↘ 검사
        FlowerType? diagType1 = GetFlowerType(slots[0]);
        if (diagType1 != null)
        {
            bool isBingo = true;
            for (int i = 1; i < GRID_SIZE; i++)
            {
                if (GetFlowerType(slots[i * (GRID_SIZE + 1)]) != diagType1)
                {
                    isBingo = false;
                    break;
                }
            }

            if (isBingo)
            {
                bingoCount++;
                score += 250;
                scoreText.text = $"Score : {score}";
                for (int i = 0; i < GRID_SIZE; i++)
                {
                    RemoveFlowerFromSlot(slots[i * (GRID_SIZE + 1)]);
                }
                RandomItem();
            }
        }

        // 대각선 ↙ 검사
        FlowerType? diagType2 = GetFlowerType(slots[GRID_SIZE - 1]);
        if (diagType2 != null)
        {
            bool isBingo = true;
            for (int i = 1; i < GRID_SIZE; i++)
            {
                if (GetFlowerType(slots[i * (GRID_SIZE - 1)]) != diagType2)
                {
                    isBingo = false;
                    break;
                }
            }

            if (isBingo)
            {
                bingoCount++;
                score += 250;
                scoreText.text = $"Score : {score}";
                for (int i = 0; i < GRID_SIZE; i++)
                {
                    RemoveFlowerFromSlot(slots[i * (GRID_SIZE - 1)]);
                }
                RandomItem();
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

    public void RemoveFlowerFromSlot(Transform slot)
    {
        if (slot.childCount > 0)
        {
            Destroy(slot.GetChild(0).gameObject);
        }
    }

    private void RandomItem()
    {
        float rand = Random.value;

        if (rand <= 0.1f)
        {
            if (Random.value < 0.5f)
            {
                itemManager.IncreaseRemove();
            }
            else
            {
                itemManager.IncreaseChange();
            }
        }
    }
}