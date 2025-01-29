using UnityEngine;

namespace CowtasticGameStudio.MuuliciousHarvest
{
    [CreateAssetMenu(fileName = "New PlaceMultiplier", menuName = "Cards/PlaceMultiplier")]
    public class SOBasePlaceMultiplier : CardTemplate
    {
        public SOBasePlaceMultiplier()
        {
            name = "Upgrade";
            lifeCycleDays = 1;
            cardType = CardType.PlaceMultiplier;
            targetCardType = CardType.None;
        }

        private void Awake()
        {
            // Cargar los sprites directamente desde la carpeta Resources
            artwork = Resources.Load<Sprite>("Cards/Upgrade");
            baseCard = Resources.Load<Sprite>("Base");
        }
    }
}
