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
    }
}
