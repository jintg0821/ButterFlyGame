using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FlowerDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    private Transform previousParent;
    private RectTransform rect;
    private Transform canvas;
    private CanvasGroup canvasGroup;
    private FlowerDrag targetFlower;
    private BoardSlotGenerator boardSlotGenerator;

    void Start()
    {
        canvas = FindObjectOfType<Canvas>().transform;
        rect = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        boardSlotGenerator = FindObjectOfType<BoardSlotGenerator>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        previousParent = transform.parent;

        transform.SetParent(canvas);
        transform.SetAsLastSibling();

        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rect.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Transform dropTarget = eventData.pointerEnter?.transform;

        // 👉 빈 보드 슬롯일 때만 드롭 허용
        if (dropTarget != null && dropTarget.CompareTag("BoardSlot") && dropTarget.childCount == 0)
        {
            transform.SetParent(dropTarget);
            transform.localPosition = Vector3.zero;

            boardSlotGenerator.RemoveFlower(gameObject);
            boardSlotGenerator.CreateFlowerSlot();

            FindObjectOfType<GameBoard>()?.CheckBingo();
        }
        else if (targetFlower != null)
        {
            // 꽃끼리 교환
            Transform targetParent = targetFlower.transform.parent;
            targetFlower.transform.SetParent(previousParent);
            transform.SetParent(targetParent);

            targetFlower.transform.localPosition = Vector3.zero;
            transform.localPosition = Vector3.zero;
        }
        else
        {
            // 👉 그 외 경우: 원래 자리로 복귀
            transform.SetParent(previousParent);
            transform.localPosition = Vector3.zero;
        }

        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
        targetFlower = null;
    }

    public void OnDrop(PointerEventData eventData)
    {
        // 드래그된 꽃 가져오기
        FlowerDrag draggedFlower = eventData.pointerDrag?.GetComponent<FlowerDrag>();

        // 👇 자신과 draggedFlower 모두 "FlowerSlot"에 있을 경우에만 교환 허용
        if (draggedFlower != null)
        {
            // 보드 슬롯에는 꽃끼리 교환 안 됨
            if (transform.parent.CompareTag("FlowerSlot") && draggedFlower.previousParent.CompareTag("FlowerSlot"))
            {
                draggedFlower.SetTargetFlower(this);
            }
        }
    }


    public void SetTargetFlower(FlowerDrag flower)
    {
        targetFlower = flower;
    }
}
