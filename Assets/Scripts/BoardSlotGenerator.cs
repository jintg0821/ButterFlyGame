using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardSlotGenerator : MonoBehaviour
{
    [Header("보드 슬롯")]
    public GameObject boardSlotPrefab;
    public Transform boardSlotContent;

    [Header("꽃 슬롯")]
    public List<GameObject> FlowerSlotList;
    public GameObject FlowerPrefab;
    public Transform flowerSlotContent;

    void Start()
    {
        CreateBoardSlot();
        CreateFlowerSlot();
    }

    public void CreateBoardSlot()
    {
        for (int i = 0; i < 25; i++)
        {
            var boardSlot = Instantiate(boardSlotPrefab, boardSlotContent);
            boardSlot.transform.SetParent(boardSlotContent);
        }
    }

    public void CreateFlowerSlot()
    {
        if (FlowerSlotList.Count < 5)
        {
            for (int i = FlowerSlotList.Count; FlowerSlotList.Count < 5; i++)
            {
                var Flowerslot = Instantiate(FlowerPrefab, flowerSlotContent);
                RandomFlowerType(Flowerslot);
            }
        }
    }

    public void RandomFlowerType(GameObject flower)
    {
        FlowerType randomType = (FlowerType)Random.Range(0, System.Enum.GetValues(typeof(FlowerType)).Length);
        flower.gameObject.GetComponent<Flower>().flowerType = randomType;
        FlowerSlotList.Add(flower);
        flower.transform.SetParent(flowerSlotContent);
    }
}