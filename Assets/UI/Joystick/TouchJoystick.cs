using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TouchJoystick : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    public Vector2 DragDirection { get; private set; }
    public bool currentlyActive { get; private set; }
    public GameObject stick_in;

    private float halfWidth;

    // Start is called before the first frame update
    void Start()
    {
        halfWidth = (transform as RectTransform).rect.width/2;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnDrag(PointerEventData eventData) {
        DragDirection =  eventData.position - (Vector2)transform.position;
        DragDirection = DragDirection/halfWidth;
        if(DragDirection.magnitude > 1)
        {
            DragDirection = DragDirection.normalized;
        }
        stick_in.transform.localPosition = new Vector3(DragDirection.x, DragDirection.y, 0) * halfWidth;
        //Debug.Log(DragDirection);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        DragDirection = new Vector2(0,0);
        stick_in.transform.localPosition = Vector3.zero;
        currentlyActive = false;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        currentlyActive = true;
    }
}
