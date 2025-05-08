using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardSlotGenerator : MonoBehaviour
{
    public Transform flowerSpawnpoint;

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
                flower.transform.tag = "SlotFlower";

                // 랜덤한 꽃 타입 지정
                FlowerType randomType = (FlowerType)Random.Range(0, System.Enum.GetValues(typeof(FlowerType)).Length);
                flower.GetComponent<Flower>().flowerType = randomType;

                FlowerSlotList.Add(flower);

                StartCoroutine(SmoothMoveToCenter(flower.transform));
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
    private IEnumerator SmoothMoveToCenter(Transform flower)
    {
        Vector3 startLocalPos = flower.parent.InverseTransformPoint(flowerSpawnpoint.position);
        Vector3 targetLocalPos = Vector3.zero;
        float duration = 0.5f;
        float elapsed = 0f;

        flower.localPosition = startLocalPos;

        while (elapsed < duration)
        {
            flower.localPosition = Vector3.Lerp(startLocalPos, targetLocalPos, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        flower.localPosition = targetLocalPos;
    }

}