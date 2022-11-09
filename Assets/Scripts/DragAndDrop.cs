using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    static public GameObject itemBeingDragged;
    static public Pattern patternInfo;

    public void OnBeginDrag(PointerEventData eventData)
    {
        GameObject duplicate = Instantiate(gameObject);
        itemBeingDragged = duplicate;
        RectTransform tmpRectTransform = gameObject.GetComponent<RectTransform>();

        RectTransform rt = itemBeingDragged.GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(tmpRectTransform.sizeDelta.x, tmpRectTransform.sizeDelta.y);

        GetComponent<CanvasGroup>().blocksRaycasts = false;
        patternInfo = GetComponent<Pattern>();
        Transform canvas = GameObject.FindGameObjectWithTag("Pattern Canvas").transform;
        itemBeingDragged.transform.SetParent(canvas);
        itemBeingDragged.GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        itemBeingDragged.transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        Destroy(DragAndDrop.itemBeingDragged);
        DragAndDrop.itemBeingDragged = null;
    }
}
