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

        private void Awake()
        {
            // Cargar los sprites directamente desde la carpeta Resources
            artwork = Resources.Load<Sprite>("Cards/helper");
            baseCard = Resources.Load<Sprite>("Base");
        }

    }
}