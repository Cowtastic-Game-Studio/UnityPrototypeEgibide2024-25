using UnityEngine;

namespace CowtasticGameStudio.MuuliciousHarvest
{
    public class TarvernZone : MonoBehaviour, IDropZone<Customer>
    {
        #region Propiedades

        public bool IsActive { get; }
        public Customer Type { get; }

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