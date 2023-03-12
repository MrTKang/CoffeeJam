using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollBarBehaviour : MonoBehaviour
{
    private Vector3 mouseDownPosition;
    private bool isMouseDown;
    private Vector3 mouseDelta;
    public GameObject IngredientBar;
    private Vector3 startPosition;
    public GameObject ScrollBarText;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        if (isMouseDown)
        {
            var mouseCurrentPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseDelta = mouseCurrentPosition - mouseDownPosition;
            if (this.transform.position.y + mouseDelta.y < startPosition.y &&
                this.transform.position.y + mouseDelta.y > startPosition.y - 1.36623f)
            {
                ScrollBarText.SetActive(false);
                var traslateY = (IngredientBar.GetComponent<RectTransform>().rect.size.y - 1.36623f) / 1.36623f;
                IngredientBar.transform.position -= new Vector3(0f, traslateY * mouseDelta.y, 0f);
                this.transform.position += new Vector3(0f, mouseDelta.y, 0f);

            }
            mouseDownPosition = mouseCurrentPosition;
        }
    }

    void OnMouseDown()
    {
        if (!isMouseDown)
        {
            mouseDownPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            isMouseDown = true;
        }
    }
    private void OnMouseUp()
    {
        isMouseDown = false;
    }

}
