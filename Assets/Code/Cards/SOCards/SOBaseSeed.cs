using UnityEngine;

namespace CowtasticGameStudio.MuuliciousHarvest
{
    [CreateAssetMenu(fileName = "New Cow", menuName = "Cards/Seed")]
    public class SOBaseSeed : CardTemplate
    {
        public SOBaseSeed()
        {
            name = "Seed";
            lifeCycleDays = 10;
            cardType = CardType.Seed;
        }

        private void Awake()
        {
            // Cargar los sprites directamente desde la carpeta Resources
            artwork = Resources.Load<Sprite>("Cards/Seed");
            baseCard = Resources.Load<Sprite>("Base");
        }

    }
}