using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CowtasticGameStudio.MuuliciousHarvest
{
    public class CardDisplayUI : MonoBehaviour
    {
        [Header("UI Elements")]
        [SerializeField] private Text nameText;
        [SerializeField] private Text descriptionText;
        [SerializeField] private Image artworkImage;
        [SerializeField] private Text actionPointsText;
        [SerializeField] private Text requieredTypeText;
        [SerializeField] private Text requieredQuantityText;
        [SerializeField] private Text producedTypeText;
        [SerializeField] private Text producedQuantityText;
        [SerializeField] private Image overlayImage;

        public void UpdateDisplay(CardTemplate cardTemplate, bool isActive)
        {
            if (cardTemplate == null)
            {
                Debug.LogError("No card template provided to CardDisplayUI.");
                return;
            }

            nameText.text = cardTemplate.name;
            descriptionText.text = !string.IsNullOrEmpty(cardTemplate.description) ? cardTemplate.description : "";
            artworkImage.material = cardTemplate.artwork;
            actionPointsText.text = cardTemplate.actionPointsCost.ToString();

            requieredTypeText.text = FormatResources(cardTemplate.requiredResources, true);
            requieredQuantityText.text = FormatResources(cardTemplate.requiredResources, false);
            producedTypeText.text = FormatResources(cardTemplate.producedResources, true);
            producedQuantityText.text = FormatResources(cardTemplate.producedResources, false);

            if (cardTemplate.cardType == CardType.PlaceActivator || cardTemplate.cardType == CardType.PlaceMultiplier)
            {
                producedTypeText.text = cardTemplate.targetCardType.ToString();
            }

            SetOverlayActive(!isActive);
        }

        private void SetOverlayActive(bool isActive)
        {
            if (overlayImage != null)
            {
                overlayImage.gameObject.SetActive(isActive);
            }
        }

        private string FormatResources(List<ResourceAmount> resources, bool isType)
        {
            if (resources == null || resources.Count == 0) return "N/A";
            return string.Join("\n", resources.ConvertAll(resource => isType ? resource.resourceType.ToString() : resource.resourceQuantity.ToString()));
        }
    }
}
