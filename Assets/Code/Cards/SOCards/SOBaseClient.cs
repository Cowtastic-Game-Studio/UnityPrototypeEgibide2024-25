using UnityEngine;

namespace CowtasticGameStudio.MuuliciousHarvest
{
    [CreateAssetMenu(fileName = "New Client", menuName = "Cards/Client")]
    public class SOBaseClient : CardTemplate
    {

        public SOBaseClient()
        {
            name = "Client";
            lifeCycleDays = 1;
            cardType = CardType.Customer;
        }

        private void Awake()
        {
            // Cargar los sprites directamente desde la carpeta Resources
            artwork = Resources.Load<Sprite>("Cards/Client");
            baseCard = Resources.Load<Sprite>("Base");
        }

    }
}
