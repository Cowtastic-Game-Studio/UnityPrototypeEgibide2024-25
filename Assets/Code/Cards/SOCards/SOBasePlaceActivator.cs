using UnityEngine;

namespace CowtasticGameStudio.MuuliciousHarvest
{
    [CreateAssetMenu(fileName = "New PlaceActivator", menuName = "Cards/PlaceActivator")]
    public class SOBasePlaceActivator : CardTemplate
    {
        public SOBasePlaceActivator()
        {
            name = "Upgrade";
            lifeCycleDays = 1;
            cardType = CardType.PlaceActivator;
            targetCardType = CardType.None;
        }
    }
}
