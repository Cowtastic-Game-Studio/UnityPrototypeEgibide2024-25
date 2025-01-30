using System.Collections.Generic;
using UnityEngine;

namespace CowtasticGameStudio.MuuliciousHarvest
{
    public class StorageManager : MonoBehaviour
    {
        #region Variables
        [SerializeField] private PAStorage _paStorage;
        [SerializeField] private Bank _bankStorage;
        [SerializeField] private Fridge _fridgeStorage;
        [SerializeField] private Silo _silo;

        private int multi = 1;
        private GameResource typeResource = GameResource.None;
        private List<ResourceAmount> _requiredResources;
        private List<ResourceAmount> _producedResources;
        #endregion

        private void Awake()
        {
            _paStorage = new PAStorage();
            _bankStorage = new Bank();
            _fridgeStorage = new Fridge();
            _silo = new Silo();
        }

        #region Methods

        #region Public
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

                IStorage storage = GetStorage<IStorage>(requireType);

                RemoveResources(requireQuantity, storage);
                StatisticsManager.Instance.UpdateByResource(requireType, requireQuantity, true);
            }

            foreach (ResourceAmount resource in _producedResources)
            {
                int producedQuantity;

                GameResource producedType = resource.resourceType;

                // Comprobamos el tipo de recurso para aplicar el multiplicador en solo ese producto
                if (typeResource == producedType)
                {
                    producedQuantity = resource.resourceQuantity * multi;
                }
                else
                {
                    producedQuantity = resource.resourceQuantity;
                }

                var storage = GetStorage<IStorage>(producedType);
                AddResources(producedQuantity, storage);
                StatisticsManager.Instance.UpdateByResource(producedType, producedQuantity, false);
            }

            RemoveResources(1, _paStorage);
            StatisticsManager.Instance.UpdateByResource(GameResource.ActionPoints, 1, true);

            return true;
        }

        public int WasteMuuney(int quantity)
        {
            int leftMuuney = _bankStorage.Resource - quantity;
            if (leftMuuney >= 0)
            {
                _bankStorage.Resource -= quantity;
            }

            return _bankStorage.Resource;
        }

        /// <summary>
        /// Obtiene la cantidad de recursos actual de un tipo.
        /// </summary>
        /// <param name="type">Tipo de recurso que se recoge.</param>
        /// <returns>Devuelve la cantidad.</returns>
        public int GetResourceAmounts(GameResource type)
        {
            return GetStorage<IStorage>(type).Resource;
        }

        /// <summary>
        /// Obtiene la cantidad máxima de recursos de un tipo.
        /// </summary>
        /// <param name="type">Tipo de recurso que se recoge.</param>
        /// <returns>Devuelve la cantidad máxima.</returns>
        public int GetMaxResourceAmounts(GameResource type)
        {
            return GetStorage<IStorage>(type).MaxResources;
        }

        /// <summary>
        /// Reinicia los puntos de acción a los máximos.
        /// </summary>
        public void RestartPA()
        {
            _paStorage.Resource = _paStorage.MaxResources;
        }

        public void SetResourceMultiplierAndType(int multi, GameResource typeResource)
        {
            this.multi = multi;
            this.typeResource = typeResource;
        }

        public void ClearResourceMultiplierAndType()
        {
            multi = 1;
            this.typeResource = GameResource.None;
        }

        /// <summary>
        /// Añade una cantidad específica de recurso a un almacenamiento determinado, 
        /// pero si sobrepasa el almacenamiento máximo, lo ajusta al máximo permitido.
        /// </summary>
        /// <param name="quantity">La cantidad de recursos a añadir.</param>
        /// <param name="type">El tipo de recurso.</param>
        public void AddResourceUpToMax(int quantity, GameResource type)
        {
            var storage = GetStorage<IStorage>(type);
            
            if (storage == null)
            {
                Debug.LogError($"No se encontró almacenamiento para el recurso: {type}");
                return;
            }

            // Calcular espacio restante
            int espacioDisponible = storage.MaxResources - storage.Resource;

            if (espacioDisponible <= 0)
            {
                Debug.LogWarning($"El almacenamiento de {type} está lleno.");
                return;
            }

            // Añadir la cantidad permitida sin exceder el máximo
            int cantidadAAgregar = Mathf.Min(quantity, espacioDisponible);
            storage.Resource += cantidadAAgregar;

            Debug.Log($"Añadidos {cantidadAAgregar} {type}. Cantidad actual: {storage.Resource}/{storage.MaxResources}");
        }

        /// <summary>
        /// Quita una cantidad específica de recurso de un almacenamiento determinado,
        /// sin reducir la cantidad por debajo de cero.
        /// </summary>
        /// <param name="quantity">La cantidad de recursos a quitar.</param>
        /// <param name="type">El tipo de recurso.</param>
        public void RemoveResourceDownToMin(int quantity, GameResource type)
        {
            var storage = GetStorage<IStorage>(type);

            if (storage == null)
            {
                Debug.LogError($"No se encontró almacenamiento para el recurso: {type}");
                return;
            }

            if (storage.Resource <= 0)
            {
                Debug.LogWarning($"El almacenamiento de {type} ya está vacío.");
                return;
            }

            // Determinar la cantidad a quitar sin quedar por debajo de 0
            int cantidadAQuitar = Mathf.Min(quantity, storage.Resource);
            storage.Resource -= cantidadAQuitar;

            Debug.Log($"Quitados {cantidadAQuitar} {type}. Cantidad actual: {storage.Resource}/{storage.MaxResources}");
        }

        #endregion

        #region Private

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

        private void RemoveResources(int quantity, IStorage storage)
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

        internal bool CheckMuuney(int cardPrice)
        {
            return CheckStorage(cardPrice, _bankStorage);
        }


        #endregion

        #endregion
    }
}
