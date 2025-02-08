using UnityEngine;

namespace CowtasticGameStudio.MuuliciousHarvest
{
    [CreateAssetMenu(fileName = "New Cow", menuName = "Cards/Cow")]
    public class SOBaseCow : CardTemplate
    {
        public SOBaseCow()
        {
            name = "Cow";
            lifeCycleDays = 2;
            cardType = CardType.Cow;
        }
    }
}