using System.Collections.Generic;

namespace Assets.Code.GamePhases
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
            return false;
        }

        private void AddLevel()
        {

        }

        private void UpgradeStorage()
        {

        }


        private int Add()
        {
            return 0;
        }

        private int Remove()
        {
            return 0;
        }

    }
}
