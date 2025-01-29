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
        public MarketManager marketManager;

        [Header("Board")]
        [Space(1)]
        public GameObject board;

        //[Header("Zones")]
        //[Space(3)]
        //public FarmZone Farm;
        //public StableZone Stables;
        //public TarvernZone Taverna;


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
                place.updateEmpty();
            }
        }

        internal void UpdateAcivePlaces()
        {
            foreach (PlaceSpaceBehaviour place in placeSpaceBehaviours)
            {
                place.updateActive();
            }
        }

        internal void checkHelperPlayer()
        {
            foreach (PlaceSpaceBehaviour place in placeSpaceBehaviours)
            {
                place.checkContainsHelper();
            }
        }
        #endregion

        #region Private methods

        #endregion

    }
}