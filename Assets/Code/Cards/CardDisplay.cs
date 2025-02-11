using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace CowtasticGameStudio.MuuliciousHarvest
{
    public class CardDisplay : MonoBehaviour
    {
        public TMP_Text nameText;
        public TMP_Text descriptionText;
        public TMP_Text actionPointsText;
        public TMP_Text lifeCycleDaysText;
        public TMP_Text requieredTypeText;
        public TMP_Text requieredQuantityText;
        public TMP_Text producedTypeText;
        public TMP_Text producedQuantityText;
        public int cost;

        //TODO: El Image que act�a como filtro gris
        [SerializeField] private MeshRenderer targetMeshRenderer;
        public Material newMaterial;
        public Material baseCardMaterial;

        void Awake()
        {
            if (targetMeshRenderer != null && newMaterial != null)
            {
                // Verifica si el MeshRenderer tiene suficientes materiales
                if (targetMeshRenderer.sharedMaterials.Length > 1)
                {
                    // Crea una copia de los materiales para asignar el nuevo material
                    Material[] materials = targetMeshRenderer.materials;
                    materials[0] = baseCardMaterial;
                    materials[1] = newMaterial;
                    targetMeshRenderer.materials = materials;

                    //Debug.Log("Material en el índice 1 actualizado correctamente.");
                }
            }
            else
            {
                Debug.LogWarning("Falta asignar el MeshRenderer o el nuevo material.");
            }
        }

        // M�todo para actualizar la visualizaci�n con los datos de la plantilla
        public void UpdateDisplay(CardTemplate cardTemplate, bool isActive)
        {
            if (cardTemplate != null)
            {
                nameText.text = cardTemplate.name;

                if (!string.IsNullOrEmpty(cardTemplate.description))
                    descriptionText.text = cardTemplate.description;
                else
                    descriptionText.text = "";

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

                cost = cardTemplate.marketCost;

                // Actualiza la visibilidad del filtro gris seg�n el estado de la carta
                SetOverlayActive(!isActive);
            }
            else
            {
                Debug.LogError("No card template provided to CardDisplay.");
            }
        }

        public void UpdateDisplayAndMat(CardTemplate cardTemplate, bool isActive)
        {
            if (targetMeshRenderer != null)
            {
                // Verifica si el MeshRenderer tiene suficientes materiales
                if (targetMeshRenderer.sharedMaterials.Length > 1)
                {
                    // Crea una copia de los materiales para asignar el nuevo material
                    Material[] materials = targetMeshRenderer.materials;
                    materials[0] = cardTemplate.baseCard;
                    materials[1] = cardTemplate.artwork;
                    targetMeshRenderer.materials = materials;

                    //Debug.Log("Material en el índice 1 actualizado correctamente.");
                }
            }

            UpdateDisplay(cardTemplate, isActive);
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
