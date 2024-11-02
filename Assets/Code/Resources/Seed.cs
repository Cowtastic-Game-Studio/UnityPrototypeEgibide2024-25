using UnityEngine;

namespace CowtasticGameStudio.MuuliciousHarvest
{
    public class Seed : MonoBehaviour, IEntity
    {
        #region Propiedades
        /// <summary>
        /// Cantidad de dias que dura el recurso
        /// </summary>
        public int LifeCycleDaysLeft { get; }

        /// <summary>
        /// Lista de recursos que produce
        /// </summary>
        public ResourceAmount ProducedResources { get; }

        /// <summary>
        /// Lista de recursos que requiere
        /// </summary>
        public ResourceAmount RequiredResources { get; }
        #endregion

        #region Metodos
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
        #endregion
    }
}