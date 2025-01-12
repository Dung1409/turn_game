using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class Inventory_Slot : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        if(this.transform.childCount != 0)
        {
            return;
        }
        GameObject dropped = eventData.pointerDrag;
        DragItem draggableItem = dropped.GetComponent<DragItem>();
        draggableItem.parentAfterDrag = transform;

    }
}
