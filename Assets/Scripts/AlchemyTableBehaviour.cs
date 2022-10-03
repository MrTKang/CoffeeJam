using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlchemyTableBehaviour : MonoBehaviour
{
    public GameObject PotionContainerPrefab;
    private List<GameObject> potionContainers = new List<GameObject>();
    public GameObject Pot;
    // Start is called before the first frame update
    void Start()
    {
        var chemTable = Pot.GetComponent<PotBehaviour>().chemicalTable;
        foreach (var name in chemTable.GetChemNames())
        {
            CreatePotionContainer(name);
        }
        CreatePotionContainer("Pure Crystals");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CreatePotionContainer(string name)
    {
        var container = Instantiate(PotionContainerPrefab, gameObject.transform.position, Quaternion.identity, gameObject.transform);
        container.GetComponent<PotionContainerBehaviour>().Name = name;
        potionContainers.Add(container);
    }
}
