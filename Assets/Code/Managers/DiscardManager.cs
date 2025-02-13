using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
        public int TotalCost { get; private set; } = 0;

        public int minDeckSize /*{ get; private set; }*/ = 35;
        public int maxDiscardLimit /*{ get; private set; }*/ = 5;
        public int currentDiscardCount = 0;

        private List<CardToDelete> cardsToDelete = new List<CardToDelete>();

        public Sprite openSprite;
        public Sprite closedSprite;

        public Button button;

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

        public void CloseMenu()
        {
            gameObject.SetActive(false);
        }

        public void ToggleMenu()
        {
            // Verificar si el número de cartas en la baraja es suficiente
            if (GameManager.Instance.Tabletop.CardManager.getAllCardsList().Count < minDeckSize)
            {
                MessageManager.Instance.ShowMessage($"No se pueden borrar cartas. Se necesitan al menos {minDeckSize} cartas.");
                //Debug.LogWarning($"No se pueden borrar cartas. Se necesitan al menos {minDeckSize} cartas.");
                return;
            }

            //// Verificar si el número de descartes actuales ha alcanzado el límite
            if (currentDiscardCount >= maxDiscardLimit && !gameObject.activeSelf)
            {
                MessageManager.Instance.ShowMessage($"No se pueden abrir más el menú. Ya has alcanzado el límite de {maxDiscardLimit} descartes.");
                //Debug.LogWarning($"No se pueden abrir más el menú. Ya has alcanzado el límite de {maxDiscardLimit} descartes.");
                return;
            }

            // Abrir o cerrar el menú
            gameObject.SetActive(!gameObject.activeSelf);

            if (button != null && button.image != null)
            {
                if (gameObject.activeSelf)
                {
                    button.image.sprite = openSprite;
                }
                else
                {
                    button.image.sprite = closedSprite;
                }
            }
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

            muuneyCostText.text = $"{TotalCost.ToString()}";
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
                MessageManager.Instance.ShowMessage("No se pueden eliminar más de 5 cartas por turno.");
                //Debug.LogWarning("No se pueden eliminar más de 5 cartas por turno.");
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
                if (selectedCount <= 0)
                    cardsToDelete.Remove(existingCardToDelete);
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
            TotalCost = 0;

            // Recorrer la lista de cartas a eliminar y sumar el costo
            foreach (var cardToDelete in cardsToDelete)
            {
                // Usamos el CardTemplate almacenado directamente
                var cardTemplate = cardToDelete.CardTemplate;

                if (cardTemplate != null)
                {
                    // Sumar el costo de las cartas (cantidad * costo de descarte)
                    TotalCost += cardToDelete.Quantity * cardTemplate.discardCost;
                }
            }

            // Actualizar el texto en el UI con el costo total de las cartas seleccionadas para eliminar
            muuneyCostText.text = $"Cost: {TotalCost.ToString()}";
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
                if (selectedCount <= 0)
                    Destroy(existingSummaryItem.gameObject);

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
            currentDiscardCount = 0;
            TotalCost = 0;
            cardsToDelete = new List<CardToDelete>();
        }
        public void AttemptToDeleteCards()
        {
            // Verificar si se ha alcanzado el límite de descartes
            if (currentDiscardCount > maxDiscardLimit)
            {
                MessageManager.Instance.ShowMessage($"No puedes eliminar más de {maxDiscardLimit} cartas por turno.");
                //Debug.LogWarning($"No puedes eliminar más de {maxDiscardLimit} cartas por turno.");
                return;
            }

            if (!GameManager.Instance.Tabletop.StorageManager.CheckMuuney(TotalCost))
            {
                MessageManager.Instance.ShowMessage("No hay suficiente dinero para eliminar esa cartas.");
                //Debug.LogWarning($"No hay suficnte dinero para eliminar esa cartas.");
                return;
            }

            // Llamar a la función del CardManager para eliminar las cartas
            GameManager.Instance.Tabletop.StorageManager.WasteMuuney(TotalCost);
            GameManager.Instance.Tabletop.CardManager.DeleteCards(cardsToDelete);

            ResetPanel();
        }

        private void ResetPanel()
        {
            TotalCost = 0;
            cardsToDelete = new List<CardToDelete>();

            UpdateDiscardGrid();
            UpdateSummaryGrid();
        }

        public bool CheckMinDeckReached()
        {
            int currentDeckSize = GameManager.Instance.Tabletop.CardManager.getAllCardsList().Count;
            int selectedForDeletion = cardsToDelete.Sum(card => card.Quantity);

            return (currentDeckSize - selectedForDeletion) <= minDeckSize;
        }
    }
}
