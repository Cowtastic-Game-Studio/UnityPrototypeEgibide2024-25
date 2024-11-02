using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CowtasticGameStudio.MuuliciousHarvest
{
    public class CardDisplay : MonoBehaviour
    {
        public Text nameText;
        public Text descriptionText;
        public Image artworkImage;
        public Image baseCardImage;
        public Text actionPointsText;
        public Text requieredTypeText;
        public Text requieredQuantityText;
        public Text producedTypeText;
        public Text producedQuantityText;

        //TODO: El Image que act�a como filtro gris
        [SerializeField]
        private Image overlayImage;

        // M�todo para actualizar la visualizaci�n con los datos de la plantilla
        public void UpdateDisplay(CardTemplate cardTemplate, bool isActive)
        {
            if (cardTemplate != null)
            {
                nameText.text = cardTemplate.name;
                descriptionText.text = cardTemplate.description;
                artworkImage.sprite = cardTemplate.artwork;
                baseCardImage.sprite = cardTemplate.baseCard;
                actionPointsText.text = cardTemplate.actionPointsCost.ToString();
                requieredTypeText.text = FormatResources(cardTemplate.requiredResources, true);
                requieredQuantityText.text = FormatResources(cardTemplate.requiredResources, false);
                producedTypeText.text = FormatResources(cardTemplate.producedResources, true);
                producedQuantityText.text = FormatResources(cardTemplate.producedResources, false);

                // Actualiza la visibilidad del filtro gris seg�n el estado de la carta
                SetOverlayActive(!isActive);
            }
            else
            {
                Debug.LogError("No card template provided to CardDisplay.");
            }
        }

        // M�todo para activar o desactivar el filtro gris
        public void SetOverlayActive(bool isActive)
        {
            // overlayImage.gameObject.SetActive(isActive);
        }

        // M�todo para formatear los recursos en un string
        private string FormatResources(List<ResourceAmount> resources, bool isType)
        {
            if (resources == null || resources.Count == 0) return "N/A";

            string formattedText = "";
            foreach (var resource in resources)
            {
                formattedText += isType ? $"{resource.resourceType}\n" : $"{resource.resourceQuantity}\n";
            }
            return formattedText;
        }
    }
}
