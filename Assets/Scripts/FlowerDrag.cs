using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FlowerDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler, IPointerClickHandler
{
    private Transform previousParent;
    private RectTransform rect;
    private Transform canvas;
    private CanvasGroup canvasGroup;
    private FlowerDrag targetFlower;
    public Image changeFlowerImage;

    private BoardSlotGenerator boardSlotGenerator;
    private ItemManager itemManager;
    private GameBoard gameBoard;

    private static FlowerDrag selectedForChange = null;

    void Start()
    {
        canvas = FindObjectOfType<Canvas>().transform;
        rect = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        boardSlotGenerator = FindObjectOfType<BoardSlotGenerator>();
        itemManager = FindObjectOfType<ItemManager>();
        gameBoard = FindObjectOfType<GameBoard>();
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
            transform.tag = "BoardFlower";

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

    public void OnPointerClick(PointerEventData eventData)
    {
        if (itemManager != null)
        {
            if (itemManager.itemType == ItemType.Remove && transform.CompareTag("BoardFlower"))
            {
                if (transform.parent != null)
                {
                    gameBoard.RemoveFlowerFromSlot(transform.parent);
                    itemManager.DecreaseRemove();
                    itemManager.itemType = ItemType.None;
                }
            }

            else if (itemManager.itemType == ItemType.Change && transform.CompareTag("BoardFlower"))
            {
                if (selectedForChange == null)
                {
                    // 첫 번째 꽃 선택
                    selectedForChange = this;
                    changeFlowerImage.gameObject.SetActive(true);
                }
                else if (selectedForChange != this)
                {
                    // 두 번째 꽃 선택 - 두 꽃을 서로 교환
                    Transform parentA = selectedForChange.transform.parent;
                    Transform parentB = transform.parent;

                    selectedForChange.transform.SetParent(parentB);
                    transform.SetParent(parentA);

                    selectedForChange.transform.localPosition = Vector3.zero;
                    transform.localPosition = Vector3.zero;

                    if (selectedForChange.changeFlowerImage != null)
                        selectedForChange.changeFlowerImage.gameObject.SetActive(false);
                    if (changeFlowerImage != null)
                        changeFlowerImage.gameObject.SetActive(false);

                    itemManager.DecreaseChange();
                    // 초기화
                    selectedForChange = null;
                    itemManager.itemType = ItemType.None;

                    gameBoard.CheckBingo();
                }

            }
        }
    }

}
