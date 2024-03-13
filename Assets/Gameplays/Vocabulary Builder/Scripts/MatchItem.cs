using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MatchItem : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerEnterHandler, IPointerUpHandler
{
    static MatchItem hoverItem;
    public GameObject linePrefab;
    public string itemName;
    private GameObject line;
    public void OnDrag(PointerEventData eventData)
    {
        UpdateLine(eventData.position);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Down");
        line = Instantiate(linePrefab, transform.position, Quaternion.identity, transform.parent.parent);
        UpdateLine(eventData.position);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        hoverItem = this;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!this.Equals(hoverItem) && itemName.Equals(hoverItem.itemName))
        {
            UpdateLine(hoverItem.transform.position);
            ObjectMatchForm.AddPoint();
            Destroy(hoverItem);
            Destroy(this);
        }
        else
        {
            Destroy(line);
        }
    }
    void UpdateLine(Vector3 position)
    {
        // update direction
        Vector3 direction = position - transform.position;
        line.transform.right = direction;
        // update scale
        line.transform.localScale = new Vector3(direction.magnitude, 1, 1);
    }
}
