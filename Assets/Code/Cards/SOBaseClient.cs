using UnityEngine;

namespace CowtasticGameStudio.MuuliciousHarvest
{
    [CreateAssetMenu(fileName = "New Client", menuName = "Cards/Client")]
    public class BaseClient : CardTemplate
    {
        public BaseClient()
        {
            name = "Client";
            lifeCycleDays = 1;
        }

        private void Awake()
        {
            // Cargar los sprites directamente desde la carpeta Resources
            artwork = Resources.Load<Sprite>("Cards/Client");
            baseCard = Resources.Load<Sprite>("Base");
        }

    }
}
