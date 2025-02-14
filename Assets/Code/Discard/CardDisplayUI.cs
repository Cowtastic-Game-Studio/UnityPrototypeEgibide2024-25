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
        [SerializeField] private Text lifeCycleDaysText;
        [SerializeField] private Text requieredTypeText;
        [SerializeField] private Text requieredQuantityText;
        [SerializeField] private Text producedTypeText;
        [SerializeField] private Text producedQuantityText;

        public void UpdateDisplay(CardTemplate cardTemplate, bool isActive)
        {
            if (cardTemplate != null)
            {
                nameText.text = cardTemplate.name;

                if (!string.IsNullOrEmpty(cardTemplate.description))
                    descriptionText.text = cardTemplate.description;
                else
                    descriptionText.text = "";

                if (cardTemplate.cardType != CardType.None)
                {

                    actionPointsText.text = cardTemplate.actionPointsCost.ToString();

                    lifeCycleDaysText.text = cardTemplate.lifeCycleDays.ToString();

                    if (cardTemplate.requiredResources.Count != 0)
                    {
                        requieredTypeText.text = FormatResources(cardTemplate.requiredResources, true);
                        requieredQuantityText.text = FormatResources(cardTemplate.requiredResources, false);
                    }
                    else
                        requieredTypeText.text = requieredQuantityText.text = "";

                    if (cardTemplate.producedResources.Count != 0)
                    {
                        producedTypeText.text = FormatResources(cardTemplate.producedResources, true);
                        producedQuantityText.text = FormatResources(cardTemplate.producedResources, false);
                    }
                    else
                        producedTypeText.text = producedQuantityText.text = "";

                    if (cardTemplate.cardType == CardType.PlaceActivator ||
                        cardTemplate.cardType == CardType.PlaceMultiplier)
                        producedTypeText.text = cardTemplate.targetCardType.ToString();
                }
                else
                {
                    actionPointsText.text = "";
                    lifeCycleDaysText.text = "";
                    requieredTypeText.text = requieredQuantityText.text = "";
                    producedTypeText.text = producedQuantityText.text = "";
                }

                if (artworkImage != null && cardTemplate.artwork != null)
                {
                    // Obtener la textura del material
                    Texture texture = cardTemplate.artwork.mainTexture;

                    // Crear una nueva Rect para tomar solo la parte superior izquierda
                    float width = texture.width / 3;
                    float height = texture.height / 3;
                    Rect newRect = new Rect(texture.height / 14, texture.height / 2, width, height);

                    // Crear un Sprite a partir de la textura
                    Sprite sprite = Sprite.Create((Texture2D)texture, newRect, new Vector2(0.5f, 0.5f));

                    // Asignar el sprite a la imagen
                    artworkImage.sprite = sprite;
                }
            }
            else
            {
                Debug.LogError("No card template provided to CardDisplay.");
            }
        }

        private string FormatResources(List<ResourceAmount> resources, bool isType)
        {
            if (resources == null || resources.Count == 0) return "";
            return string.Join("\n", resources.ConvertAll(resource => isType ? resource.resourceType.ToString() : resource.resourceQuantity.ToString()));
        }
    }
}
