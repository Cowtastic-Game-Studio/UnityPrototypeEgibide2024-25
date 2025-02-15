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

        private int multiEvent = 0;
        private int multiCard = 0;
        private GameResource typeResource = GameResource.None;
        private List<ResourceAmount> _requiredResources;
        private List<ResourceAmount> _producedResources;
        private int _paCost;
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

        #region Checks
        /// <summary>
        /// Comprueba si hay suficientes puntos de acción para realizar la acción.
        /// </summary>
        /// <param name="nAP">Cantidad de puntos de acción necesarios.</param>
        /// <returns>Devuelve true si hay PA. False si no hay.</returns>
        public bool CheckActionPoints(int nAP)
        {
            if (_paStorage.Resource < nAP)
            {
                MessageManager.Instance.ShowMessage("Not enough action points.", 1);
                Debug.LogWarning($"Not enough action points."); // No estoy muy segura
                return false;
            }
            _paCost = nAP;
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

            /*   foreach (ResourceAmount resource in producedResources)
               {
                   int producedQuantity = resource.resourceQuantity;
                   GameResource producedType = resource.resourceType;

                   IStorage storage = GetStorage<IStorage>(producedType);

                   if (storage == _paStorage)
                   {
                       if (!CheckMaxStorage(producedQuantity, storage))
                       {
                           return false;
                       }
                   }
               }*/

            _requiredResources = requiredResources;
            _producedResources = producedResources;

            return true;
        }

        public bool CheckMuuney(int cardPrice)
        {
            return CheckStorage(cardPrice, _bankStorage);
        }
        #endregion

        #region Getters
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

        public int GetStorageMaxLevel(GameResource resource)
        {
            var storage = GetStorage<IStorage>(resource);
            return storage.MaxLevel;
        }

        public int GetBankPlusPrice()
        {
            return Utils.RoundMuuney(_bankStorage.MaxResources * 0.3);
        }

        public int GetActionPointPlusPrice()
        {
            return Utils.RoundMuuney(_paStorage.MaxResources * 1.3);
        }

        #endregion

        #region Setters
        public void SetResourceMultiplierEventAndType(int multi, GameResource typeResource)
        {
            multiEvent = multi;
            this.typeResource = typeResource;
        }

        public void SetResourceMultiplierCardAndType(int multi, GameResource typeResource)
        {
            multiCard = multi;
            this.typeResource = typeResource;
        }
        #endregion

        #region Economy

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

                if (!RemoveResources(requireQuantity, storage))
                {
                    return false;
                }

                StatisticsManager.Instance.UpdateByResource(requireType, requireQuantity, true);
            }

            foreach (ResourceAmount resource in _producedResources)
            {
                int producedQuantity;

                GameResource producedType = resource.resourceType;

                // Comprobamos el tipo de recurso para aplicar el multiplicador en solo ese producto
                if (typeResource == producedType)
                {
                    int multi = multiEvent + multiCard;
                    producedQuantity = resource.resourceQuantity * multi;
                }
                else
                {
                    producedQuantity = resource.resourceQuantity;
                }

                var storage = GetStorage<IStorage>(producedType);
                if (!AddResources(producedQuantity, storage, true))
                {
                    return false;
                }
                StatisticsManager.Instance.UpdateByResource(producedType, producedQuantity, false);
            }

            if (!RemoveResources(_paCost, _paStorage))
            {
                return false;
            }
            StatisticsManager.Instance.UpdateByResource(GameResource.ActionPoints, _paCost, true);

            return true;
        }

        public void WasteMuuney(int quantity)
        {
            int leftMuuney = _bankStorage.Resource - quantity;
            if (leftMuuney >= 0)
            {
                _bankStorage.Resource -= quantity;
            }
            GameManager.Instance.Tabletop.HUDManager.UpdateResources();
        }

        /// <summary>
        /// Añade una cantidad específica de recurso a un almacenamiento determinado, 
        /// pero si sobrepasa el almacenamiento máximo, lo ajusta al máximo permitido.
        /// </summary>
        /// <param name="quantity">La cantidad de recursos a añadir.</param>
        /// <param name="type">El tipo de recurso.</param>
        public void AddResourceUpToMax(int quantity, GameResource type, bool isUpToMax)
        {
            IStorage storage = GetStorage<IStorage>(type);

            if (storage == null)
            {

                Debug.LogError($"No se encontró almacenamiento para el recurso: {type}");
                return;
            }

            AddResources(quantity, storage, isUpToMax);
        }

        /// <summary>
        /// Quita una cantidad específica de recurso de un almacenamiento determinado,
        /// sin reducir la cantidad por debajo de cero.
        /// </summary>
        /// <param name="quantity">La cantidad de recursos a quitar.</param>
        /// <param name="type">El tipo de recurso.</param>
        public void RemoveResourceDownToMin(int quantity, GameResource type)
        {
            IStorage storage = GetStorage<IStorage>(type);

            if (storage == null)
            {
                Debug.LogError($"No se encontró almacenamiento para el recurso: {type}");
                return;
            }

            RemoveResources(quantity, storage);
        }

        #endregion

        #region Upgrades
        public void UpgradeStorage(GameResource resource)
        {
            switch (resource)
            {
                case GameResource.ActionPoints:
                    UpgradeStorage(_paStorage);
                    break;
                case GameResource.Milk:
                    UpgradeStorage(_fridgeStorage);
                    break;
                case GameResource.Muuney:
                    UpgradeStorage(_bankStorage);
                    break;
                case GameResource.Cereal:
                    UpgradeStorage(_silo);
                    break;
            }
        }

        #endregion

        #region Restart
        /// <summary>
        /// Reinicia los puntos de acción a los máximos.
        /// </summary>
        public void RestartPA()
        {
            _paStorage.Resource = _paStorage.MaxResources;
        }

        public void ClearResourceMultiplierEventAndType()
        {
            multiEvent = 0;
            typeResource = GameResource.None;
        }

        public void ClearResourceMultiplierCardAndType()
        {
            multiCard = 0;
            typeResource = GameResource.None;
        }

        #endregion

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

        #region Checks
        /// <summary>
        /// Comprueba si hay suficientes recursos para realizar la acción.
        /// </summary>
        /// <param name="quantity">La cantidad que pide</param>
        /// <param name="storage">El almacen</param>
        /// <returns>Devuelve true si hay recursos suficientes.</returns>
        private bool CheckStorage(int quantity, IStorage storage)
        {
            int leftResources = storage.Resource - quantity;
            if (/*quantity >= storage.Resource ||*/ leftResources < 0)
            {
                MessageManager.Instance.ShowMessage("Not enough resources", 1);
                Debug.LogWarning("Not enough resources");
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

            if (newResources > storage.MaxResources && storage != _bankStorage)
            {
                MessageManager.Instance.ShowMessage("There is not enough space to store the resource.", 1);
                Debug.LogWarning("There is not enough space to store the resource.");
                return false;
            }

            return true;
        }

        #endregion

        #region Economy
        private bool AddResources(int quantity, IStorage storage, bool isUpToMax)
        {
            int leftSpace = storage.Resource;

            if (isUpToMax)
            {
                leftSpace = storage.MaxResources - storage.Resource;
                if (leftSpace <= 0)
                {
                    Debug.LogWarning("The storage is full.");
                    MessageManager.Instance.ShowMessage("The storage is full.", 1);
                    return false;
                }
            }

            int quantityToAdd = Mathf.Min(quantity, leftSpace);
            storage.Resource += quantityToAdd;

            return true;
        }

        private bool RemoveResources(int quantity, IStorage storage)
        {
            //storage.Resource -= quantity;

            if (storage.Resource <= 0)
            {
                Debug.LogWarning("The storage is already empty.");
                return false;
            }

            int quantityToRemove = Mathf.Min(quantity, storage.Resource);
            storage.Resource -= quantityToRemove;

            return true;
        }

        /// <summary>
        /// Sube en uno el nivel del almacen, pero no lo mejora
        /// </summary>
        /// <param name="storage">El tipo de almacen</param>
        private void AddLevel(IStorage storage)
        {
            storage.Level += 1;
        }
        #endregion

        #region UpgradeStorage

        /// <summary>
        /// Llama a la funcion de mejora del almacen pasado como parametro
        /// </summary>
        /// <param name="storage">El tipo de almacen</param>
        private void UpgradeStorage(IStorage storage)
        {
            if (storage == _paStorage)
            {
                UpgradePAStorage();
            }
            if (storage == _bankStorage)
            {
                UpgradeBankStorage();
            }
            if (storage == _fridgeStorage)
            {
                UpgradeFridgeStorage();
            }
            if (storage == _silo)
            {
                UpgradeSiloStorage();
            }

            GameManager.Instance.Tabletop.HUDManager.UpdateResources();
        }

        /// <summary>
        /// Mejora del almacen de APs
        /// </summary>
        private void UpgradePAStorage()
        {
            if (_paStorage.Level > _paStorage.MaxLevel)
            {
                MessageManager.Instance.ShowMessage("Reached AP storage max level.", 1);
                //Debug.LogWarning("Reached AP storage max level.");
                return;
            }

            int upgradeCost = GetActionPointPlusPrice();
            if (CheckMuuney(upgradeCost))
            {
                _paStorage.MaxResources += 2;
                AddLevel(_paStorage);
                WasteMuuney(upgradeCost);
                Debug.LogWarning("Upgraded AP storage.");
            }
        }

        /// <summary>
        /// Mejora del banco
        /// </summary>
        private void UpgradeBankStorage()
        {
            int upgradeCost = GetBankPlusPrice();
            if (CheckMuuney(upgradeCost))
            {
                _bankStorage.MaxResources += 5;
                AddLevel(_bankStorage);
                WasteMuuney(upgradeCost);
                Debug.LogWarning("Upgraded bank.");
                StatisticsManager.Instance.UpdateByBuyedZone(GameResource.ActionPoints);
            }
        }

        /// <summary>
        /// Mejora de la nevera
        /// </summary>
        private void UpgradeFridgeStorage()
        {
            if (_fridgeStorage.Level >= _fridgeStorage.MaxLevel)
            {
                //Debug.LogWarning("Reached Fridge max level.");
                MessageManager.Instance.ShowMessage("Reached Fridge max level.", 1);
                return;
            }

            if (CheckMuuney(15))
            {
                _fridgeStorage.MaxResources += 2;
                AddLevel(_fridgeStorage);
                WasteMuuney(15);
                Debug.LogWarning("Upgraded fridge.");
                StatisticsManager.Instance.UpdateByBuyedZone(GameResource.Milk);
            }
        }

        /// <summary>
        /// Mejora del silo
        /// </summary>
        private void UpgradeSiloStorage()
        {
            if (_silo.Level >= _silo.MaxLevel)
            {
                // Debug.LogWarning("Reached Silo max level.");
                MessageManager.Instance.ShowMessage("Reached Silo max level.", 1);
                return;
            }

            if (CheckMuuney(10))
            {
                _silo.MaxResources += 4;
                AddLevel(_silo);
                WasteMuuney(10);
                Debug.LogWarning("Upgraded silo.");
                StatisticsManager.Instance.UpdateByBuyedZone(GameResource.Cereal);
            }
        }

        #endregion

        #endregion

        #endregion


        // Este update se puede borrar, solo es para pruebas.
        public void Update()
        {
            if (GameManager.Instance.isActivatedCheatCodes)
            {
                if (Input.GetKeyDown(KeyCode.Y))
                {
                    UpgradeStorage(_bankStorage);
                }
                if (Input.GetKeyDown(KeyCode.U))
                {
                    UpgradeStorage(_paStorage);
                }
                if (Input.GetKeyDown(KeyCode.I))
                {
                    UpgradeStorage(_fridgeStorage);
                }
                if (Input.GetKeyDown(KeyCode.O))
                {
                    UpgradeStorage(_silo);
                }
                if (Input.GetKeyDown(KeyCode.L))
                {
                    _bankStorage.Resource = _bankStorage.MaxResources;
                    GameManager.Instance.Tabletop.HUDManager.UpdateResources();
                }
            }
        }

    }
}
