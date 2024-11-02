using UnityEngine;

namespace CowtasticGameStudio.MuuliciousHarvest
{
    public class Silo : MonoBehaviour, IStorage
    {
        #region Propiedades

        public GameResource Type { get; set; }
        public int MaxResources { get; set; }
        public int Level { get; set; }
        public int Resource { get; set; }

        #endregion

        #region Constructor
        /// <summary>
        /// Constructor del almacen
        /// </summary>
        public Silo()
        {
            // Le indicamos que el tipo del recurso es
            Type = GameResource.Cereal;
            MaxResources = 5;
            Level = 1;
            Resource = 3;
        }

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