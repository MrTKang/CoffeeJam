using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public class PotBehaviour : MonoBehaviour
    {
        public ChemicalTable chemicalTable = ChemicalTable.Instance;
        private GameObject leftChemical = null;
        private GameObject rightChemical = null;
        private WinampBehaviour winampBehaviour;

        public GameObject ProductSlot;
        public GameObject Winamp;

        void Start()
        {
            winampBehaviour = Winamp.GetComponent<WinampBehaviour>();   
        }

        void Update()
        {

        }

        public void FillLeftSlot(GameObject ingredient)
        {
            leftChemical = ingredient;
            var product = Combine();
            if (product != null)
            {
                var slotPosition = ProductSlot.transform.position;
                slotPosition.z = 1;
                var productPrefab = Resources.Load<GameObject>(product.Name);
                if (productPrefab == null)
                {
                    Debug.Log($"Prefab, {product.Name}, does not exist");
                }
                else
                {
                    leftChemical = Instantiate(productPrefab, slotPosition, Quaternion.identity);
                }
            }
        }

        public void FillRightSlot(GameObject ingredient)
        {
            rightChemical = ingredient;
            var product = Combine();
            if (product != null)
            {
                var slotPosition = ProductSlot.transform.position;
                slotPosition.z = 1;
                var productPrefab = Resources.Load<GameObject>(product.Name);
                if (productPrefab == null)
                {
                    Debug.Log($"Prefab, {product.Name}, does not exist");
                }
                else
                {
                    leftChemical = Instantiate(productPrefab, slotPosition, Quaternion.identity);
                }
            }
        }

        public Chemical Combine()
        {
            if (leftChemical != null && rightChemical != null)
            {
                winampBehaviour.PlayCelebration();
                var leftName = leftChemical.GetComponent<PotionBehaviour>().Name;
                var rightName = rightChemical.GetComponent<PotionBehaviour>().Name;
                Destroy(leftChemical);
                Destroy(rightChemical);
                leftChemical = null;
                rightChemical = null;
                return chemicalTable.Combine(new Chemical[] { chemicalTable[leftName], chemicalTable[rightName] });
            }
            return null;
        }
    }
}
