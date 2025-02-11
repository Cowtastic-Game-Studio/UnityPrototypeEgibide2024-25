using System.Collections.Generic;

using UnityEngine;


namespace CowtasticGameStudio.MuuliciousHarvest
{
    public class Tabletop : MonoBehaviour, ITabletop
    {
        #region Properties

        [Header("Managers")]
        public CardManager CardManager;
        public HUDManager HUDManager;
        public StorageManager StorageManager;
        //public MarketManager marketManager;
        public NewMarketManager NewMarketManager;
        public DiscardManager DiscardManager;

        [Header("Board")]
        [Space(1)]
        public GameObject board;

        //[Header("Zones")]
        //[Space(3)]
        //public FarmZone Farm;
        //public StableZone Stables;
        //public TavernZone Taverna;

        /// <summary>
        /// Listas de zonas para poder activarlas
        /// </summary>
        public List<PlaceSpaceBehaviour> farms = new List<PlaceSpaceBehaviour>();
        public List<PlaceSpaceBehaviour> stables = new List<PlaceSpaceBehaviour>();
        public List<PlaceSpaceBehaviour> taverns = new List<PlaceSpaceBehaviour>();

        private List<PlaceSpaceBehaviour> placeSpaceBehaviours = new List<PlaceSpaceBehaviour>();
        #endregion

        #region Public methods
        public void CleanPlayerCards()
        {

        }

        public void DrawFromDeck()
        {

        }

        public void Mulligan()
        {

        }

        public void PutCard()
        {

        }

        public void ShuffleDiscardDeck()
        {

        }

        public void OnCardUseActionPoints(ICard card)
        {

        }

        public void FindPlaces()
        {
            // Buscar todos los objetos con el tag "Place"
            GameObject[] places = GameObject.FindGameObjectsWithTag("Place");

            // Recorrer todos los objetos encontrados
            foreach (GameObject place in places)
            {
                // Obtener el componente PlaceSpaceBehaviour del objeto
                PlaceSpaceBehaviour placeSpace = place.GetComponent<PlaceSpaceBehaviour>();

                // Verificar si el componente existe y agregarlo a la lista
                if (placeSpace != null)
                {
                    placeSpaceBehaviours.Add(placeSpace);
                }
            }
        }

        public void UpdateEmptyPlaces()
        {
            foreach (PlaceSpaceBehaviour place in placeSpaceBehaviours)
            {
                if (place.transform.childCount == 0)
                    place.updateEmpty();
            }
        }

        /// <summary>
        /// Estos 3 metodos son los que activan la siguiente zona a activar cuando mejoren alguna zona.(Nota: Asegurarse de que las casillas que tienen que empezar activas estan las primeras en la lista , gracias.)
        /// </summary>
        public void FarmsActivateZone()
        {
            if (farms.FindAll(x => !x.GetIsActive()).Count == 0)
            {
                Debug.LogWarning("Max gardens.");
                MessageManager.Instance.ShowMessage($"Te han robado {theft.amount} de leche.");
                return;
            }


            foreach (PlaceSpaceBehaviour farm in farms)
            {
                if (!farm.GetIsActive())
                {
                    farm.SetIsActive(true);
                    MessageManager.Instance.ShowMessage($"Te han robado {theft.amount} de leche.");
                    Debug.LogWarning("Updated garden.");
                    break;
                }
            }
        }

        public void StablesActivateZone()
        {
            if (stables.FindAll(x => !x.GetIsActive()).Count == 0)
            {
                MessageManager.Instance.ShowMessage($"Te han robado {theft.amount} de leche.");
                Debug.LogWarning("Max stables.");
                return;
            }

            foreach (PlaceSpaceBehaviour stable in stables)
            {
                if (!stable.GetIsActive())
                {
                    stable.SetIsActive(true);
                    MessageManager.Instance.ShowMessage($"Te han robado {theft.amount} de leche.");
                    Debug.LogWarning("Updated stable.");
                    break;
                }
            }
        }

        public void TavernActivateZone()
        {
            if (taverns.FindAll(x => !x.GetIsActive()).Count == 0)
            {
                Debug.LogWarning("Max shop.");
                MessageManager.Instance.ShowMessage($"Te han robado {theft.amount} de leche.");
                return;
            }

            foreach (PlaceSpaceBehaviour tavern in taverns)
            {
                if (!tavern.GetIsActive())
                {
                    tavern.SetIsActive(true);
                    Debug.LogWarning("Updated shop.");
                    MessageManager.Instance.ShowMessage($"Te han robado {theft.amount} de leche.");
                    break;
                }
            }
        }

        internal void UpdateAcivePlaces()
        {
            foreach (PlaceSpaceBehaviour place in placeSpaceBehaviours)
            {
                place.updateActive();
            }
        }

        #endregion

        #region Private methods

        #endregion

    }
}