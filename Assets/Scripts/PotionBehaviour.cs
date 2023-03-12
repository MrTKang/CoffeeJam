using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PotionBehaviour : MonoBehaviour
{
    private Vector3 mouseDelta = new Vector2(0, 0);
    private bool isMouseDown = false;
    private Vector3 mouseDownPosition = new Vector2(0, 0);
    public bool IsAnimation = false;
    private AudioSource pickDropSound;

    private bool isOnLeftSlot = false;
    private bool isOnRightSlot = false;
    private Vector3 slotPosition = new Vector3();

    public string Name;
    private GameObject pot;

    public GameObject PotionLabelPrefab;
    private GameObject potionLabelPrefab;

    private PotBehaviour potBehaviour;
    public EventHandler OnSlotted;

    private Vector3 originalPosition;
    void Start()
    {
        pickDropSound = GetComponent<AudioSource>();
        originalPosition = gameObject.transform.position;

        pot = GameObject.Find("Pot");
        potBehaviour = pot.GetComponent<PotBehaviour>();

        potionLabelPrefab = Instantiate(PotionLabelPrefab, gameObject.transform.position + (new Vector3(-0.1f, 0.25f, 0f) / 2), Quaternion.identity, gameObject.transform);
        potionLabelPrefab.GetComponent<TextMeshPro>().text = Name;
        potionLabelPrefab.SetActive(false);
    }
    void OnMouseDown()
    {
        if (!isMouseDown)
        {
            mouseDownPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            isMouseDown = true;
            potionLabelPrefab.SetActive(false);
            pickDropSound.Play();
        }
    }
    private void OnMouseUp()
    {
        isMouseDown = false;
        if (isOnLeftSlot || isOnRightSlot)
        {
            this.transform.position = new Vector3(slotPosition.x, slotPosition.y, this.transform.position.z);
        }
        if (isOnLeftSlot)
        {
            if (potBehaviour.LeftChemical == null)
            {
                potBehaviour.FillLeftSlot(this.gameObject);
                transform.localScale = new Vector3(1f, 1f, 1f);
                OnSlotted?.Invoke(this, EventArgs.Empty);
                GetComponent<Rigidbody2D>().simulated = false;
                this.gameObject.transform.SetParent(pot.transform);
            }
            else
            {
                this.transform.position = originalPosition;
            }
        }
        if (isOnRightSlot)
        {
            if (potBehaviour.RightChemical == null)
            {
                potBehaviour.FillRightSlot(this.gameObject);
                transform.localScale = new Vector3(1f, 1f, 1f);
                OnSlotted?.Invoke(this, EventArgs.Empty);
                GetComponent<Rigidbody2D>().simulated = false;
                this.gameObject.transform.SetParent(pot.transform);
            }
            else
            {
                this.transform.position = originalPosition;
            }
        }
        Debug.Log("dropped");
    }

    void OnMouseEnter()
    {
        if (!IsAnimation && !isMouseDown) potionLabelPrefab.SetActive(true);
    }

    void OnMouseExit()
    {
        potionLabelPrefab.SetActive(false);
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
        if (slotBehaviour == null) return;
        isOnLeftSlot = slotBehaviour.Left;
        isOnRightSlot = slotBehaviour.Right;
    }
    void OnTriggerExit2D(Collider2D col)
    {
        isOnLeftSlot = false;
        isOnRightSlot = false;
    }
}
