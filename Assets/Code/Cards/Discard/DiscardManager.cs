using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace CowtasticGameStudio.MuuliciousHarvest
{
    public class DiscardManager : MonoBehaviour
    {
        public GameObject discardItemPrefab;
        public Transform cardGridParent;

        public GameObject SummaryItemPrefab;
        public Transform summaryGridParent;

        public TMP_Text cardCountText;
        public TMP_Text muuneyCostText;

        private List<GameObject> allDiscardedCards = new List<GameObject>();
        private float totalCost = 0f;

        private void Awake()
        {
            gameObject.SetActive(false);
        }

        void OnEnable()
        {
            UpdateDiscardGrid();
            UpdateSummaryGrid();
        }

        public void ToggleMenu()
        {
            gameObject.SetActive(!gameObject.activeSelf);
        }

        private void UpdateDiscardGrid()
        {
            ClearDiscardGrid();

            allDiscardedCards = GameManager.Instance.Tabletop.CardManager.getAllCardsList()
                .OrderBy(card => card.GetComponent<CardBehaviour>()?.GetTemplate().cardType)
                .ToList();

            cardCountText.text = $"Total Cards: {allDiscardedCards.Count}";

            var groupedCards = allDiscardedCards
                .GroupBy(card => card.GetComponent<CardBehaviour>()?.GetTemplate().cardType);

            foreach (var group in groupedCards)
            {
                GameObject stackedItem = Instantiate(discardItemPrefab, cardGridParent);
                var discardItem = stackedItem.GetComponent<DiscardItem>();
                discardItem.Setup(group.First(), group.Count());

                // Suscribirse al evento de cambio de selecci�n
                discardItem.OnCountChanged += UpdateTotalCost;
            }

            muuneyCostText.text = $"Cost: {totalCost.ToString()}";
        }

        private void UpdateSummaryGrid()
        {
            ClearSummaryGrid();
        }

        private void ClearDiscardGrid()
        {
            foreach (Transform child in cardGridParent)
            {
                Destroy(child.gameObject);
            }
        }

        private void ClearSummaryGrid()
        {
            foreach (Transform child in summaryGridParent)
            {
                Destroy(child.gameObject);
            }
        }

        // Actualizar el costo total y crear un SummaryItem en el panel correspondiente
        private void UpdateTotalCost(GameObject cardGO, int selectedCount, CardTemplate cardTemplate)
        {
            // Calcular el costo total basado en la cantidad de cartas seleccionadas y su costo de descarte
            float cardCost = cardTemplate.discardCost;
            totalCost = selectedCount * cardCost;

            // Mostrar el costo total en el UI
            muuneyCostText.text = $"Cost: {totalCost.ToString()}";

            // Crear un SummaryItem en el summaryGridParent
            CreateSummaryItem(cardTemplate, selectedCount);
        }

        // Crear el SummaryItem en el panel de resumen
        private void CreateSummaryItem(CardTemplate cardTemplate, int selectedCount)
        {
            // Verificar si el SummaryItem ya existe para este tipo de carta
            var existingSummaryItem = summaryGridParent
                .Cast<Transform>()
                .FirstOrDefault(item => item.GetComponent<SummaryItem>().CardTemplate == cardTemplate);

            if (existingSummaryItem != null)
            {
                // Si ya existe, simplemente actualizar el conteo
                existingSummaryItem.GetComponent<SummaryItem>().UpdateSummary(selectedCount);
            }
            else
            {
                // Si no existe, crear un nuevo SummaryItem
                GameObject summaryItem = Instantiate(SummaryItemPrefab, summaryGridParent);
                var summaryItemComponent = summaryItem.GetComponent<SummaryItem>();
                summaryItemComponent.Setup(cardTemplate, selectedCount);
            }
        }
    }
}
