using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CowtasticGameStudio.MuuliciousHarvest
{
    public class CardDisplay : MonoBehaviour
    {
        public CardTemplate card;

        public Text nameText;
        public Text descriptionText;

        public Image artworkImage;
        public Image baseCardImage;

        public Text actionPointsText;

        public Text requieredTypeText;
        public Text requieredQuantityText;

        public Text producedTypeText;
        public Text producedQuantityText;

        // Use this for initialization
        void Start()
        {
            if (card != null)
            {

                //card.Print();

                // Configuración básica de texto y sprites
                nameText.text = card.name;
                descriptionText.text = card.description;
                artworkImage.sprite = card.artwork;
                baseCardImage.sprite = card.baseCard;
                actionPointsText.text = card.actionPointsCost.ToString();

                // Configuración de textos para los recursos requeridos y producidos
                requieredTypeText.text = FormatResources(card.requiredResources, true);
                requieredQuantityText.text = FormatResources(card.requiredResources, false);
                producedTypeText.text = FormatResources(card.producedResources, true);
                producedQuantityText.text = FormatResources(card.producedResources, false);
            }
        }

        // Método para formatear los recursos en un string
        private string FormatResources(List<ResourceAmount> resources, bool isType)
        {
            if (resources == null || resources.Count == 0) return "N/A"; // Si no hay recursos, muestra "N/A"

            string formattedText = "";

            foreach (var resource in resources)
            {
                formattedText += isType ? $"{resource.resourceType}\n" : $"{resource.resourceQuantity}\n";
            }

            return formattedText;
        }
    }
}