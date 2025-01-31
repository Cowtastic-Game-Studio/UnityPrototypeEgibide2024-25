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
        //public MarketManager marketManager;
        public NewMarketManager NewMarketManager;

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
                place.updateEmpty();
            }
        }

/// <summary>
/// Estos 3 metodos son los que activan la siguiente zona a activar cuando mejoren alguna zona.(Nota: Asegurarse de que las casillas que tienen que empezar activas estan las primeras en la lista , gracias.)
/// </summary>
        public void FarmsActivateZone()
        {
            foreach(PlaceSpaceBehaviour farm in farms)
            {
                int i = 0;

                if (!farm.GetIsActive())
                {
                    farm.SetIsActive(true);
                    break;
                }
            }
        }

        public void StablesActivateZone()
        {
            foreach(PlaceSpaceBehaviour stable in stables)
            {
                if (!stable.GetIsActive())
                {
                    stable.SetIsActive(true);
                    break;
                }
            }
        }

        public void TavernActivateZone()
        {
            foreach(PlaceSpaceBehaviour tavern in taverns)
            {
                if (!tavern.GetIsActive())
                {
                    tavern.SetIsActive(true);
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