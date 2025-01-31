using System;
using System.Collections.Generic;
using Unity.VisualScripting;
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
        //public TavernZone Taverna;

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
                place.updateEmpty();
            }
        }

        public void FarmsActivateZone()
        {
            foreach(PlaceSpaceBehaviour farm in farms)
            {
                int i = 0;

                if ((farms[i - 1].GetIsActive() || farms[i-1].Equals(null)) && !farms[i].GetIsActive() && (!farms[i + 1].GetIsActive() || farms[i-1].Equals(null)))
                {
                    farms[i].SetIsActive(true);
                    break;
                }
            }
        }

        public void StablesActivateZone()
        {
            foreach(PlaceSpaceBehaviour stable in stables)
            {
                int i = 0;

                if ((stables[i - 1].GetIsActive() || stables[i-1].Equals(null)) && !stables[i].GetIsActive() && (!stables[i + 1].GetIsActive() || stables[i-1].Equals(null)))
                {
                    stables[i].SetIsActive(true);
                    break;
                }
            }
        }

        public void TavernActivateZone()
        {
            foreach(PlaceSpaceBehaviour tavern in taverns)
            {
                int i = 0;

                if ((taverns[i - 1].GetIsActive() || taverns[i-1].Equals(null)) && !taverns[i].GetIsActive() && (!taverns[i + 1].GetIsActive() || taverns[i-1].Equals(null)))
                {
                    taverns[i].SetIsActive(true);
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