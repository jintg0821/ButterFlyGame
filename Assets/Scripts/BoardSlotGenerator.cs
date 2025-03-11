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
        for (int i = 0; i < 25; i++)
        {
            var boardSlot = Instantiate(boardSlotPrefab, boardSlotContent);
            boardSlot.transform.SetParent(boardSlotContent);
        }
    }

    public void CreateFlowerSlot()
    {
        while (FlowerSlotList.Count < FlowerSlotPositionList.Count)
        {
            int index = FlowerSlotList.Count; // 현재 리스트 크기를 인덱스로 사용

            var Flowerslot = Instantiate(FlowerPrefab);
            Flowerslot.transform.SetParent(FlowerSlotPositionList[index], false); // 부모 설정 (false: 로컬 위치 유지)
            Flowerslot.transform.localPosition = Vector3.zero; // 로컬 포지션을 (0,0,0)으로 설정

            // 랜덤한 꽃 타입 지정
            FlowerType randomType = (FlowerType)Random.Range(0, System.Enum.GetValues(typeof(FlowerType)).Length);
            Flowerslot.GetComponent<Flower>().flowerType = randomType;

            // 리스트에 추가
            FlowerSlotList.Add(Flowerslot);
        }
    }
}