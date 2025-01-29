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

        private void Awake()
        {
            // Cargar los sprites directamente desde la carpeta Resources
            artwork = Resources.Load<Sprite>("Cards/Upgrade");
            baseCard = Resources.Load<Sprite>("Base");
        }
    }
}
