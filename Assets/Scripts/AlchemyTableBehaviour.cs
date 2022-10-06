using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlchemyTableBehaviour : MonoBehaviour
{
    public GameObject PotionContainerPrefab;
    private List<GameObject> potionContainers = new List<GameObject>();
    public GameObject Pot;
    public bool AllChemsMode;
    public List<string> CurrentChemicals = new List<string>();
    // Start is called before the first frame update
    void Start()
    {
        if (AllChemsMode)
        {
            var chemTable = Pot.GetComponent<PotBehaviour>().chemicalTable;
            foreach (var name in chemTable.GetChemNames())
            {
                CreatePotionContainer(name);
            }
            CreatePotionContainer("Pure Crystals");
        }
        else
        {
            foreach (var chemicalNames in CurrentChemicals)
            {
                CreatePotionContainer(chemicalNames);
            }
        }
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

    public void RegisterChemical(Chemical product)
    {
        if (CurrentChemicals.Contains(product.Name)) return;
        CreatePotionContainer(product.Name);
        CurrentChemicals.Add(product.Name);
    }
}
