using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class DragItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private Image image;
    [HideInInspector] public Transform parentAfterDrag;
    [HideInInspector] public Transform startDrag;

    private void Awake()
    {
        image = GetComponent<Image>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        parentAfterDrag = transform.parent;
        startDrag = transform.parent;
        this.transform.SetParent(transform.root);
        image.raycastTarget = false;
        transform.SetAsLastSibling();
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        this.transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if(startDrag == parentAfterDrag)
        {
            transform.SetParent(startDrag);
            image.raycastTarget = true;
            return;
        }
        parentAfterDrag.GetComponent<Image>().sprite = image.sprite;
        SelectionPlayer.intant.addItemForHero(image.sprite);
        image.raycastTarget = true;
        this.gameObject.SetActive(false);
        this.enabled = true;
    }

}
