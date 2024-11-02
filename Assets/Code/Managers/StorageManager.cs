using System.Collections.Generic;

using UnityEngine;

namespace CowtasticGameStudio.MuuliciousHarvest
{
    public class StorageManager : IStorage
    {
        private PAStorage _paStorage;
        private Bank _bankStorage;
        private Fridge _fridgeStorage;
        private Silo _silo;

        public GameResource Type { get; }
        public int MaxResources { get; }
        public int Level { get; }
        public int Resource { get; }

        private List<ResourceAmount> _requiredResources;
        private List<ResourceAmount> _producedResources;

        /// <summary>
        /// Comprueba si hay suficientes puntos de acción para realizar la acción.
        /// </summary>
        /// <param name="nAP">Cantidad de puntos de acción necesarios.</param>
        /// <returns>Devuelve true si hay PA. False si no hay.</returns>
        public bool CheckActionPoints(int nAP)
        {
            if (Resource < nAP)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Comprueba si hay suficientes recursos para realizar la acción. Y si el almacenamiento es suficiente.
        /// </summary>
        /// <param name="requiredResources">Lista con la cantidad de recursos que necesita y el tipo de recurso.</param>
        /// <param name="producedResources">Lista con la cantidad de recursos que recibe y el tipo de recurso.</param>
        /// <returns>Devuelve true si hay recursos y espacio suficiente.</returns>
        public bool CheckResources(List<ResourceAmount> requiredResources, List<ResourceAmount> producedResources)
        {

            int requireQuantity = requiredResources[0].resourceQuantity;
            int leftResources = Resource - requireQuantity;

            if (requireQuantity > Resource || leftResources < 0)
            {
                Debug.Log("No hay suficientes recursos");
                return false;
            }

            int producedQuantity = producedResources[0].resourceQuantity;
            int newResources = producedQuantity + Resource;

            if (newResources > MaxResources)
            {
                Debug.Log("No hay suficientes espacio para almacenar el recurso.");
                return false;
            }

            _requiredResources = requiredResources;
            _producedResources = requiredResources;

            return true;
        }

        /// <summary>
        /// Realiza la acción de producir recursos.
        /// </summary>
        /// <returns>Devuelve true si se ha podido hacer la acción.</returns>
        public bool ProduceResources()
        {
            int requireQuantity = _requiredResources[0].resourceQuantity;
            GameResource requireType = _requiredResources[0].resourceType;

            RemoveResource(requireQuantity, requireType);

            int producedQuantity = _producedResources[0].resourceQuantity;
            GameResource producedType = _producedResources[0].resourceType;

            AddResources(producedQuantity, producedType);

            return false;
        }

        private int AddResources(int quantity, GameResource type)
        {

            return 0;
        }

        private int RemoveResource(int quantity, GameResource type)
        {
            return 0;
        }

        private void AddLevel()
        {

        }

        private void UpgradeStorage()
        {

        }




    }
}
