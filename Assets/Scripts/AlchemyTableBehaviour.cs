using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class AlchemyTableBehaviour : MonoBehaviour
{
    public GameObject PotionContainerPrefab;
    private Dictionary<string, GameObject> potionContainers = new Dictionary<string, GameObject>();
    public GameObject Pot;
    public bool AllChemsMode;
    public List<string> CurrentChemicals = new List<string>();
    // Start is called before the first frame update
    void Start()
    {
        var yOffSet = 0f;
        if (AllChemsMode)
        {
            var chemTable = Pot.GetComponent<PotBehaviour>().chemicalTable;
            foreach (var name in chemTable.GetChemNames())
            {
                CreatePotionContainer(name, true);
                yOffSet += 0.25f;
            }
        }
        else
        {
            var chemTable = Pot.GetComponent<PotBehaviour>().chemicalTable;
            foreach (var chemicalNames in CurrentChemicals)
            {
                CreatePotionContainer(chemicalNames, true);
                yOffSet += 0.25f;
            }

            foreach (var name in chemTable.GetChemNames().Except(CurrentChemicals))
            {
                CreatePotionContainer(name, false);
                yOffSet += 0.25f;
            }
        }
        this.transform.position = new Vector3(transform.position.x, 0.85f - yOffSet, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
    }

    void CreatePotionContainer(string name, bool active)
    {
        var container = Instantiate(PotionContainerPrefab, gameObject.transform.position, Quaternion.identity, gameObject.transform);
        var behaviour = container.GetComponent<PotionContainerBehaviour>();
        behaviour.Name = name;
        behaviour.IsActive = active;
        potionContainers.Add(name, container);
    }

    public void RegisterChemical(Chemical product)
    {
        if (CurrentChemicals.Contains(product.Name)) return;
        var behaviour = potionContainers[product.Name].GetComponent<PotionContainerBehaviour>();
        behaviour.Activate();
        CurrentChemicals.Add(product.Name);
    }
}
