using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour, IDropHandler
{

    private Image image;

    public void OnDrop(PointerEventData eventData)
    {
        //Debug.Log("OnDrop");
        if (eventData.pointerDrag != null)
        {
            GetComponent<Image>().sprite = eventData.pointerDrag.GetComponent<Image>().sprite;
            image.color = new Color(image.color.r, image.color.g, image.color.b, 1);
        }
    }

    void Start()
    {
        image = GetComponent<Image>();
    }
}
