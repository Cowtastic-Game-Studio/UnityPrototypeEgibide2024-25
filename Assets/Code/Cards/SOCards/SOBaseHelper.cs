using UnityEngine;

namespace CowtasticGameStudio.MuuliciousHarvest
{
    [CreateAssetMenu(fileName = "New Helper", menuName = "Cards/Helper")]
    public class SOBaseHelper : CardTemplate
    {
        public SOBaseHelper()
        {
            name = "Helper";
            lifeCycleDays = 1;
            cardType = CardType.Helper;
        }
    }
}