using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PotionContainerBehaviour : MonoBehaviour
{
    public string Name;
    public GameObject PotionLabelPrefab;
    private GameObject potionLabelPrefab;
    private GameObject productPrefab;
    private GameObject productGameObject;

    public float RefreshTime;
    private float refreshTime;
    // Start is called before the first frame update

    void Start()
    {
        refreshTime = RefreshTime;
        potionLabelPrefab = Instantiate(PotionLabelPrefab, gameObject.transform.position, Quaternion.identity, gameObject.transform);
        potionLabelPrefab.GetComponent<TextMeshPro>().text = Name;

        productPrefab = Resources.Load<GameObject>(Name);
        if (productPrefab == null)
        {
            Debug.Log($"Prefab, {Name}, does not exist");
        }
        else
        {
            StockPotion();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (productGameObject != null) return;
        refreshTime -= Time.deltaTime;
        if (refreshTime < 0)
        {
            StockPotion();
            refreshTime = RefreshTime;
        }
    }

    void StockPotion()
    {
        productGameObject = Instantiate(productPrefab, gameObject.transform.position, Quaternion.identity, gameObject.transform);
        var potionBehaviour = productGameObject.GetComponent<PotionBehaviour>();
        potionBehaviour.OnSlotted += OnSlotted;
    }

    void OnSlotted(object sender, EventArgs args)
    {
        productGameObject = null;
    }
}
