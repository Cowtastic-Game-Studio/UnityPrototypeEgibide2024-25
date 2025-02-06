using UnityEngine;

namespace CowtasticGameStudio.MuuliciousHarvest
{
    [CreateAssetMenu(fileName = "New Seed", menuName = "Cards/Seed")]
    public class SOBaseSeed : CardTemplate
    {
        public SOBaseSeed()
        {
            name = "Seed";
            lifeCycleDays = 10;
            cardType = CardType.Seed;
        }

    }
}