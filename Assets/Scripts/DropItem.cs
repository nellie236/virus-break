using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DropItem : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        Pattern myPattern = gameObject.GetComponent<Pattern>();
        Pattern dropPattern = DragAndDrop.itemBeingDragged.GetComponent<Pattern>();

        Image dropSprite = DragAndDrop.itemBeingDragged.GetComponent<Image>();
        gameObject.GetComponent<Button>().image.sprite = dropSprite.sprite;

        myPattern.patternID = dropPattern.patternID;

        Destroy(DragAndDrop.itemBeingDragged);
        DragAndDrop.itemBeingDragged = null;
    }

    
}
