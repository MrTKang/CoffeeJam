using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class PotBehaviour : MonoBehaviour
    {
        public ChemicalTable chemicalTable = ChemicalTable.Instance;
        public GameObject LeftChemical = null;
        public GameObject RightChemical = null;
        private List<GameObject> productChemicals = new List<GameObject>();
        
        public GameObject ProductSlot;

        public GameObject Winamp;
        private WinampBehaviour winampBehaviour;

        public GameObject AlchemyTable;
        private AlchemyTableBehaviour alchemyTableBehaviour;
        private AudioSource pickDropSound;
        public GameObject PlaceHereRightText;
        public GameObject PlaceHereLeftText;
        public GameObject CongratsText;

        public bool IsGameOver;
        public float GameOverTimer;

        void Start()
        {
            winampBehaviour = Winamp.GetComponent<WinampBehaviour>();
            alchemyTableBehaviour = AlchemyTable.GetComponent<AlchemyTableBehaviour>();

            pickDropSound = GetComponent<AudioSource>();
        }

        void Update()
        {
            if (IsGameOver)
            {
                GameOverTimer -= Time.deltaTime;
            }
            if (GameOverTimer < 0f)
            {
                CongratsText.SetActive(true);
            }
            if (Input.anyKey && GameOverTimer < 0f)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
            }
        }

        public void FillLeftSlot(GameObject ingredient)
        {
            pickDropSound.Play();
            LeftChemical = ingredient;
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
                    var productChemical = Instantiate(productPrefab, slotPosition, Quaternion.identity);
                    productChemicals.Add(productChemical);
                    PlayCombineAnimation(productChemical);
                }
            }
            PlaceHereLeftText.SetActive(false);
            PlaceHereRightText.SetActive(false);
        }

        public void FillRightSlot(GameObject ingredient)
        {
            pickDropSound.Play();
            RightChemical = ingredient;
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
                    var productChemical = Instantiate(productPrefab, slotPosition, Quaternion.identity);
                    productChemicals.Add(productChemical);
                    PlayCombineAnimation(productChemical);
                }
            }
            PlaceHereLeftText.SetActive(false);
            PlaceHereRightText.SetActive(false);
        }

        public Chemical Combine()
        {
            if (LeftChemical != null && RightChemical != null)
            {
                winampBehaviour.PlayCelebration();
                var leftName = LeftChemical.GetComponent<PotionBehaviour>().Name;
                var rightName = RightChemical.GetComponent<PotionBehaviour>().Name;
                Destroy(LeftChemical);
                Destroy(RightChemical);
                LeftChemical = null;
                RightChemical = null;
                var product = chemicalTable.Combine(new Chemical[] { chemicalTable[leftName], chemicalTable[rightName] });
                alchemyTableBehaviour.RegisterChemical(product);
                return product;
            }
            return null;
        }
        public void PlayCombineAnimation(GameObject product)
        {
            var behaviour = product.GetComponent<PotionBehaviour>();
            behaviour.IsAnimation = true;
            if (behaviour.Name.Equals("Coffee"))
            {
                product.transform.DORotate(new Vector3(0, 0, 720), 2f, RotateMode.FastBeyond360);
                product.transform.DOMove(new Vector3(0, 0, 1f), 1f);
                product.transform.DOScale(new Vector3(5, 5, 1), 1f);
                IsGameOver = true;
            }
            else
            {
                product.transform.DORotate(new Vector3(0, 0, 360), 1f, RotateMode.FastBeyond360);
                product.transform.DOMove(new Vector3(0, 0, 1f), 1f);
                product.transform.DOScale(new Vector3(5, 5, 1), 1f);
                Destroy(product, 1.5f);
            }
        }
    }
}
