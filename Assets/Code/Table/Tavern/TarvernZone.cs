using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CowtasticGameStudio.MuuliciousHarvest
{
    public class TarvernZone : MonoBehaviour, IDropZone<Customer>
    {
        #region Propiedades de la clase
        /// <summary>
        /// Si esta activo o no
        /// </summary>
        public bool IsActive { get; }

        /// <summary>
        /// El tipo de la zona
        /// </summary>
        public Customer Type { get; }
        #endregion

        #region Metodos de la clase
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