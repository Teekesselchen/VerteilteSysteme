using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TouchJoystick : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    public Vector2 DragDirection { get; private set; }
    public bool currentlyActive { get; private set; }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnDrag(PointerEventData eventData) {
        DragDirection =  eventData.position - (Vector2)transform.position;
        DragDirection /= ((transform as RectTransform).rect.width / 2);
        //Debug.Log(DragDirection);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        DragDirection = new Vector2(0,0);
        currentlyActive = false;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        currentlyActive = true;
    }
}
