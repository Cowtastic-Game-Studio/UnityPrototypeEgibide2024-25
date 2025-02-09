using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace CowtasticGameStudio.MuuliciousHarvest
{
    public class DiscardManager : MonoBehaviour
    {
        public GameObject discardItemPrefab;
        public Transform gridParent;
        public TMP_Text cardCountText;

        private List<GameObject> allDiscardedCards = new List<GameObject>();

        void OnEnable()
        {
            UpdateDiscardGrid();
        }

        public void ToggleMenu()
        {
            //if (GameManager.Instance.Tabletop.CardManager.getAllCardsList().Count < 36)
            //{
            //    Debug.LogWarning("No se pueden borrar cartas. Se necesitan al menos 36 cartas.");
            //    return;
            //}

            gameObject.SetActive(!gameObject.activeSelf);
        }

        private void UpdateDiscardGrid()
        {
            ClearGrid();

            allDiscardedCards = GameManager.Instance.Tabletop.CardManager.getAllCardsList()
                .OrderBy(card => card.GetComponent<CardBehaviour>()?.GetTemplate().cardType)
                .ToList();

            cardCountText.text = $"Total Cards: {allDiscardedCards.Count}";

            var groupedCards = allDiscardedCards
                .GroupBy(card => card.GetComponent<CardBehaviour>()?.GetTemplate().cardType);

            foreach (var group in groupedCards)
            {
                GameObject stackedItem = Instantiate(discardItemPrefab, gridParent);
                stackedItem.GetComponent<DiscardItem>().Setup(group.First(), group.Count());
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
