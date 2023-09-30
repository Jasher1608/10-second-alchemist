using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{

    [SerializeField] private Canvas canvas;

    private Sprite sourceImage;
    public GameObject itemPrefab;
    private RectTransform instantiatedTransform;

    private GameObject instantiated;

    private void Awake()
    {
        sourceImage = GetComponent<Image>().sprite;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("OnPointerDown");
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag");
        InstantiateItem();
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("OnDrag");
        instantiatedTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }
    
    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OnEndDrag");
        Destroy(instantiated);
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDrop");
    }

    public void InstantiateItem()
    {
        instantiated = (GameObject)Instantiate(itemPrefab, this.transform.position, Quaternion.identity);
        instantiated.transform.SetParent(canvas.transform, true);
        instantiated.GetComponent<Image>().sprite = sourceImage;
        instantiatedTransform = instantiated.GetComponent<RectTransform>();
    }
}
