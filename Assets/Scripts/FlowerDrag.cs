using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FlowerDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Transform previousParent;
    private RectTransform rect;
    private Transform canvas;
    private CanvasGroup canvasGroup;
    private FlowerDrag targetFlower;

    void Start()
    {
        canvas = FindObjectOfType<Canvas>().transform;
        rect = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
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
        if (targetFlower != null)
        {
            // 서로 부모 위치 변경 (위치 교체)
            Transform targetParent = targetFlower.transform.parent;
            targetFlower.transform.SetParent(previousParent);
            transform.SetParent(targetParent);

            // 위치 초기화
            targetFlower.transform.localPosition = Vector3.zero;
            transform.localPosition = Vector3.zero;
        }
        else
        {
            // 원래 위치로 되돌리기
            transform.SetParent(previousParent);
            transform.localPosition = Vector3.zero;
        }

        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
        targetFlower = null; // 초기화
    }

    public void SetTargetFlower(FlowerDrag flower)
    {
        targetFlower = flower;
    }
}
