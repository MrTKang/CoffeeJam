using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionBehaviour : MonoBehaviour
{
    private Vector3 mouseDelta = new Vector2(0, 0);
    private bool isMouseDown = false;
    private Vector3 mouseDownPosition = new Vector2(0, 0);

    private bool isOnLeftSlot = false;
    private bool isOnRightSlot = false;
    private Vector3 slotPosition = new Vector3();

    public string Name;
    private GameObject pot;

    private PotBehaviour potBehaviour;
    public EventHandler OnSlotted;
    void Start()
    {
        pot = GameObject.Find("Pot");
        potBehaviour = pot.GetComponent<PotBehaviour>();
    }
    void OnMouseDown()
    {
        if (!isMouseDown)
        {
            mouseDownPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            isMouseDown = true;
        }
        Debug.Log("picked");
    }
    private void OnMouseUp()
    {
        isMouseDown = false;
        if (isOnLeftSlot || isOnRightSlot)
        {
            this.transform.position = new Vector3(slotPosition.x, slotPosition.y, this.transform.position.z) ;
        }
        if (isOnLeftSlot)
        {
            potBehaviour.FillLeftSlot(this.gameObject);
            OnSlotted?.Invoke(this, EventArgs.Empty);
        }
        if (isOnRightSlot)
        {
            potBehaviour.FillRightSlot(this.gameObject);
            OnSlotted?.Invoke(this, EventArgs.Empty);
        }
        Debug.Log("dropped");
    }
    // Update is called once per frame
    void Update()
    {
        if (isMouseDown)
        {
            var mouseCurrentPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseDelta = mouseCurrentPosition - mouseDownPosition;
            this.transform.position += mouseDelta;
            mouseDownPosition = mouseCurrentPosition;
        }
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("in slot");
        slotPosition = col.gameObject.transform.position;
        var slotBehaviour = col.gameObject.GetComponent<SlotBehaviour>();
        isOnLeftSlot = slotBehaviour.Left;
        isOnRightSlot = slotBehaviour.Right;
    }
    void OnTriggerExit2D(Collider2D col)
    {
        isOnLeftSlot = false;
        isOnRightSlot = false;
    }
}
