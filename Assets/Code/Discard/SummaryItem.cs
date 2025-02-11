using TMPro;
using UnityEngine;

namespace CowtasticGameStudio.MuuliciousHarvest
{
    public class SummaryItem : MonoBehaviour
    {
        public TMP_Text cardNameText;
        public TMP_Text costText;

        public CardTemplate CardTemplate { get; private set; }

        // Configurar el SummaryItem
        public void Setup(CardTemplate cardTemplate, int selectedCount)
        {
            CardTemplate = cardTemplate;
            cardNameText.text = cardTemplate.name;

            // Actualizar el costText en formato "1 x 5 = 5"
            costText.text = $"{selectedCount} x {CardTemplate.discardCost} = {(selectedCount * CardTemplate.discardCost).ToString()}";
        }

        // Actualizar el SummaryItem cuando se modifique el número de cartas seleccionadas
        public void UpdateSummary(int selectedCount)
        {
            // Actualizar el costText con el nuevo costo total en el formato "1 x 5 = 5"
            costText.text = $"{selectedCount} x {CardTemplate.discardCost} = {(selectedCount * CardTemplate.discardCost).ToString()}";
        }
    }
}
