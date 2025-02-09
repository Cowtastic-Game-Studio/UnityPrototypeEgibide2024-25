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
        public TMP_Text discardLeftCountText;
        public TMP_Text muuneyCostText;

        private List<GameObject> allDiscardedCards = new List<GameObject>();
        private float totalCost = 0f;

        public int minDeckSize /*{ get; private set; }*/ = 35;
        public int maxDiscardLimit /*{ get; private set; }*/ = 5;
        public int currentDiscardCount = 0;

        private bool discardLimitReached = false;
        private List<CardToDelete> cardsToDelete = new List<CardToDelete>();

        private void Awake()
        {
            gameObject.SetActive(false);
            ResetDiscardCount();
        }

        void OnEnable()
        {
            UpdateDiscardGrid();
            UpdateSummaryGrid();
        }

        public void ToggleMenu()
        {
            // Verificar si el número de cartas en la baraja es suficiente
            if (GameManager.Instance.Tabletop.CardManager.getAllCardsList().Count <= minDeckSize)
            {
                Debug.LogWarning($"No se pueden borrar cartas. Se necesitan al menos {minDeckSize} cartas.");
                return;
            }

            //// Verificar si el número de descartes actuales ha alcanzado el límite
            //if (currentDiscardCount >= maxDiscardLimit)
            //{
            //    Debug.LogWarning($"No se pueden abrir más el menú. Ya has alcanzado el límite de {maxDiscardLimit} descartes.");
            //    return;
            //}

            // Abrir o cerrar el menú
            gameObject.SetActive(!gameObject.activeSelf);
        }

        private void UpdateDiscardGrid()
        {
            ClearDiscardGrid();

            allDiscardedCards = GameManager.Instance.Tabletop.CardManager.getAllCardsList()
                .OrderBy(card => card.GetComponent<CardBehaviour>()?.GetTemplate().cardType)
                .ToList();

            cardCountText.text = $"Total Cards: {allDiscardedCards.Count}";
            discardLeftCountText.text = $"{maxDiscardLimit - currentDiscardCount} discard left";

            var groupedCards = allDiscardedCards
                .GroupBy(card => card.GetComponent<CardBehaviour>()?.GetTemplate().cardType);

            foreach (var group in groupedCards)
            {
                GameObject stackedItem = Instantiate(discardItemPrefab, cardGridParent);
                var discardItem = stackedItem.GetComponent<DiscardItem>();
                discardItem.Setup(group.First(), group.Count());

                // Suscribirse al evento de cambio de selección
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

        private void UpdateTotalCost(GameObject cardGO, int selectedCount, CardTemplate cardTemplate)
        {
            // Calcular el costo total basado en la cantidad de cartas seleccionadas y su costo de descarte
            float cardCost = cardTemplate.discardCost;

            // Verificar si el número total de eliminaciones excede el límite
            if (currentDiscardCount > maxDiscardLimit)
            {
                Debug.LogWarning("No se pueden eliminar más de 5 cartas por turno.");
                return;
            }

            // Crear un SummaryItem en el summaryGridParent
            CreateSummaryItem(cardTemplate, selectedCount);

            // Actualizar la lista cardsToDelete con la cantidad seleccionada
            UpdateCardsToDeleteList(cardTemplate, selectedCount);

            // Actualizar el costo total de todas las cartas a eliminar
            UpdateTotalCostFromCardsToDelete();
        }

        // Esta función actualizará la lista cardsToDelete
        private void UpdateCardsToDeleteList(CardTemplate cardTemplate, int selectedCount)
        {
            // Verificar si ya existe este tipo de carta en la lista
            var existingCardToDelete = cardsToDelete
                .FirstOrDefault(card => card.CardType == cardTemplate.cardType);

            if (existingCardToDelete != null)
            {
                // Si ya existe, actualizamos la cantidad
                existingCardToDelete.Quantity = selectedCount;
            }
            else
            {
                // Si no existe, agregamos un nuevo objeto CardToDelete con la cantidad seleccionada
                CardToDelete newCardToDelete = new CardToDelete()
                {
                    CardType = cardTemplate.cardType,
                    Quantity = selectedCount,
                    CardTemplate = cardTemplate // Almacenamos el CardTemplate
                };

                cardsToDelete.Add(newCardToDelete);
            }
        }

        // Esta función recalcula el costo total de todas las cartas en cardsToDelete
        private void UpdateTotalCostFromCardsToDelete()
        {
            // Reiniciar el totalCost
            totalCost = 0f;

            // Recorrer la lista de cartas a eliminar y sumar el costo
            foreach (var cardToDelete in cardsToDelete)
            {
                // Usamos el CardTemplate almacenado directamente
                var cardTemplate = cardToDelete.CardTemplate;

                if (cardTemplate != null)
                {
                    // Sumar el costo de las cartas (cantidad * costo de descarte)
                    totalCost += cardToDelete.Quantity * cardTemplate.discardCost;
                }
            }

            // Actualizar el texto en el UI con el costo total de las cartas seleccionadas para eliminar
            muuneyCostText.text = $"Cost: {totalCost.ToString()}";
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
        public void ResetDiscardCount()
        {
            discardLimitReached = false;
            currentDiscardCount = 0;
        }
        public void AttemptToDeleteCards()
        {
            // Verificar si se ha alcanzado el límite de descartes
            if (currentDiscardCount >= maxDiscardLimit)
            {
                Debug.LogWarning($"No puedes eliminar más de {maxDiscardLimit} cartas por turno.");
                return;
            }

            // Llamar a la función del CardManager para eliminar las cartas
            GameManager.Instance.Tabletop.CardManager.DeleteCards(cardsToDelete);

            // Restablecer el contador de eliminaciones después de la eliminación
            //ResetDiscardCount();
            ResetPanel();
        }

        private void ResetPanel()
        {
            totalCost = 0f;

            UpdateDiscardGrid();
            UpdateSummaryGrid();
        }
    }
}
