using UnityEngine;

namespace CowtasticGameStudio.MuuliciousHarvest
{
    [CreateAssetMenu(fileName = "New PlaceUnlock", menuName = "Cards/PlaceUnlock")]
    public class SOBasePlaceUnlock : CardTemplate
    {
        public SOBasePlaceUnlock()
        {
            name = "PlaceUnlock";
            lifeCycleDays = 1;
            cardType = CardType.None;
            targetCardType = CardType.None;
        }
    }
}
