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

        private void Awake()
        {
            // Cargar los sprites directamente desde la carpeta Resources
            artwork = Resources.Load<Sprite>("Cards/Upgrade");
            baseCard = Resources.Load<Sprite>("Base");
        }
    }
}
