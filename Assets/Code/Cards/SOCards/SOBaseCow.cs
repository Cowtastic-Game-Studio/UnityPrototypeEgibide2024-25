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

        private void Awake()
        {
            // Cargar los sprites directamente desde la carpeta Resources
            artwork = Resources.Load<Sprite>("Cards/Cow");
            baseCard = Resources.Load<Sprite>("Base");
        }
    }
}