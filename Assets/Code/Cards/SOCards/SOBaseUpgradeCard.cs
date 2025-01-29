using UnityEngine;

namespace CowtasticGameStudio.MuuliciousHarvest
{
    [CreateAssetMenu(fileName = "New Upgrade", menuName = "Cards/Upgrade")]
    public class SOBaseUpgradeCard : CardTemplate
    {
        void Start()
        {
            name = "Upgrade";
            lifeCycleDays = 1;
            cardType = CardType.Upgrade;
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
