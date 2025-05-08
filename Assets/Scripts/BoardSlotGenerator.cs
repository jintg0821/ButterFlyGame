using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardSlotGenerator : MonoBehaviour
{
    [Header("보드 슬롯")]
    public GameObject boardSlotPrefab;
    public Transform boardSlotContent;
    public List<Transform> boardSlots = new List<Transform>();

    [Header("꽃 슬롯")]
    public List<GameObject> FlowerSlotList;
    public List<Transform> FlowerSlotPositionList;
    public GameObject FlowerPrefab;
    public Transform flowerSlotContent;

    void Start()
    {
        CreateBoardSlot();
        CreateFlowerSlot();
    }

    public void CreateBoardSlot()
    {
        boardSlots.Clear();

        for (int i = 0; i < 25; i++)
        {
            var boardSlot = Instantiate(boardSlotPrefab, boardSlotContent);
            boardSlot.transform.SetParent(boardSlotContent);
            boardSlots.Add(boardSlot.transform);
        }
    }

    public void CreateFlowerSlot()
    {
        for (int i = 0; i < FlowerSlotPositionList.Count; i++)
        {
            Transform slotTransform = FlowerSlotPositionList[i];

            // 슬롯이 비어있으면 꽃 생성
            if (slotTransform.childCount == 0)
            {
                var flower = Instantiate(FlowerPrefab);
                flower.transform.SetParent(slotTransform, false);
                flower.transform.localPosition = Vector3.zero;
                flower.transform.tag = "SlotFlower";

                // 랜덤한 꽃 타입 지정
                FlowerType randomType = (FlowerType)Random.Range(0, System.Enum.GetValues(typeof(FlowerType)).Length);
                flower.GetComponent<Flower>().flowerType = randomType;

                FlowerSlotList.Add(flower);
            }
        }
    }


    public void RemoveFlower(GameObject flower)
    {
        if (FlowerSlotList.Contains(flower))
        {
            FlowerSlotList.Remove(flower);
        }
    }
}