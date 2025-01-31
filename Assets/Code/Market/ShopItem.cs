using TMPro;
using UnityEngine;

namespace CowtasticGameStudio.MuuliciousHarvest
{
    public class ShopItem : MonoBehaviour
    {
        [SerializeField] private TMP_Text price;
        [SerializeField] private CardDisplay card;

        public void UpdateDisplayData(CardTemplate cardTemplate)
        {
            card.UpdateDisplay(cardTemplate, false);
            price.text = cardTemplate.marketCost.ToString();
        }
    }
}
