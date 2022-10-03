using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PotionContainerBehaviour : MonoBehaviour
{
    public string Name;
    public GameObject PotionLabelPrefab;
    private GameObject potionLabelPrefab;
    // Start is called before the first frame update
    void Start()
    {
        potionLabelPrefab = Instantiate(PotionLabelPrefab, gameObject.transform.position, Quaternion.identity, gameObject.transform);
        potionLabelPrefab.GetComponent<TextMeshPro>().text = Name;

        var productPrefab = Resources.Load<GameObject>(Name);
        if (productPrefab == null)
        {
            Debug.Log($"Prefab, {Name}, does not exist");
        }
        else
        {
            Instantiate(productPrefab, gameObject.transform.position, Quaternion.identity, gameObject.transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
