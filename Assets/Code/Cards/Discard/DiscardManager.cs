using System.Collections.Generic;
using UnityEngine;

namespace CowtasticGameStudio.MuuliciousHarvest
{
    public class DiscardManager : MonoBehaviour
    {
        public GameObject discardItemPrefab;
        public Transform gridParent;

        private List<GameObject> allDiscardedCards = new List<GameObject>();

        private void Awake()
        {
            gameObject.SetActive(false);
        }

        void OnEnable()
        {
            UpdateDiscardGrid();
        }

        public void ToggleMenu()
        {
            gameObject.SetActive(!gameObject.activeSelf);
        }

        private void UpdateDiscardGrid()
        {
            ClearGrid();

            allDiscardedCards = GameManager.Instance.Tabletop.CardManager.getAllCardsList();

            foreach (var card in allDiscardedCards)
            {
                Instantiate(discardItemPrefab, gridParent).GetComponent<DiscardItem>().Setup(card);
            }
        }

        private void ClearGrid()
        {
            foreach (Transform child in gridParent)
            {
                Destroy(child.gameObject);
            }
        }
    }
}