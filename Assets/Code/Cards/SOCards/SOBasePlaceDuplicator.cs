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
    }
}
