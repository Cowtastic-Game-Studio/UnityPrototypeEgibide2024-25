using System.Collections.Generic;

using UnityEngine;

namespace CowtasticGameStudio.MuuliciousHarvest
{
    public class StorageManager : MonoBehaviour
    {
        [SerializeField] private PAStorage _paStorage;
        [SerializeField] private Bank _bankStorage;
        [SerializeField] private Fridge _fridgeStorage;
        [SerializeField] private Silo _silo;

        private List<ResourceAmount> _requiredResources;
        private List<ResourceAmount> _producedResources;

        /// <summary>
        /// Comprueba si hay suficientes puntos de acción para realizar la acción.
        /// </summary>
        /// <param name="nAP">Cantidad de puntos de acción necesarios.</param>
        /// <returns>Devuelve true si hay PA. False si no hay.</returns>
        public bool CheckActionPoints(int nAP)
        {
            if (_paStorage.Resource < nAP)
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

            foreach (ResourceAmount resource in requiredResources)
            {
                int requireQuantity = resource.resourceQuantity;
                GameResource requireType = resource.resourceType;

                var storage = GetStorage<IStorage>(requireType);

                if (!CheckStorage(requireQuantity, storage))
                {
                    return false;
                }
            }

            foreach (ResourceAmount resource in producedResources)
            {
                int producedQuantity = resource.resourceQuantity;
                GameResource producedType = resource.resourceType;

                var storage = GetStorage<IStorage>(producedType);

                if (!CheckMaxStorage(producedQuantity, storage))
                {
                    return false;
                }
            }

            _requiredResources = requiredResources;
            _producedResources = producedResources;

            return true;
        }

        /// <summary>
        /// Realiza la acción de producir recursos.
        /// </summary>
        /// <returns>Devuelve true si se ha podido hacer la acción.</returns>
        public bool ProduceResources()
        {
            foreach (ResourceAmount resource in _requiredResources)
            {
                int requireQuantity = resource.resourceQuantity;
                GameResource requireType = resource.resourceType;

                var storage = GetStorage<IStorage>(requireType);

                RemoveResource(requireQuantity, storage);
            }

            foreach (ResourceAmount resource in _producedResources)
            {
                int producedQuantity = resource.resourceQuantity;
                GameResource producedType = resource.resourceType;

                var storage = GetStorage<IStorage>(producedType);


                AddResources(producedQuantity, storage);
            }

            return false;
        }

        /// <summary>
        /// Metodo para obtener el almacenamiento de un recurso.
        /// </summary>
        /// <typeparam name="T">La clase obtenida</typeparam>
        /// <param name="type">Los tipos de GameResource</param>
        /// <returns>Una clase</returns>
        private T GetStorage<T>(GameResource type) where T : class
        {
            switch (type)
            {
                case GameResource.ActionPoints:
                    return _paStorage as T;
                case GameResource.Muuney:
                    return _bankStorage as T;
                case GameResource.Milk:
                    return _fridgeStorage as T;
                case GameResource.Cereal:
                    return _silo as T;
                default:
                    return null;
            }
        }

        /// <summary>
        /// Comprueba si hay suficientes recursos para realizar la acción.
        /// </summary>
        /// <param name="quantity">La cantidad que pide</param>
        /// <param name="storage">El almacen</param>
        /// <returns>Devuelve true si hay recursos suficientes.</returns>
        private bool CheckStorage(int quantity, IStorage storage)
        {
            int leftResources = storage.Resource - quantity;
            if (quantity > storage.Resource || leftResources < 0)
            {
                Debug.Log("No hay suficientes recursos");
                return false;
            }

            return true;
        }

        /// <summary>
        /// Comprueba si hay suficiente espacio para almacenar el recurso.
        /// </summary>
        /// <param name="quantity">La cantidad que pide</param>
        /// <param name="storage">El almacen</param>
        /// <returns>Devuelve true si se aun hay espacio suficiente.</returns>
        private bool CheckMaxStorage(int quantity, IStorage storage)
        {
            int newResources = quantity + storage.Resource;

            if (newResources > storage.MaxResources)
            {
                Debug.Log("No hay suficientes espacio para almacenar el recurso.");
                return false;
            }

            return true;
        }

        private void AddResources(int quantity, IStorage storage)
        {
            storage.Resource += quantity;
        }

        private void RemoveResource(int quantity, IStorage storage)
        {
            storage.Resource -= quantity;
        }

        private void AddLevel()
        {
            // TODO: Implementar
        }

        private void UpgradeStorage()
        {
            // TODO: Implementar
        }

        public void RestartPA() {
            // TODO: Implementar
        }
    }
}
