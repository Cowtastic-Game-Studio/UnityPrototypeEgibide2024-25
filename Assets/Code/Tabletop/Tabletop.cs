using System.Collections.Generic;
using UnityEngine;

namespace CowtasticGameStudio.MuuliciousHarvest
{
    public class Tabletop : MonoBehaviour, ITabletop, ICardsManager
    {
        #region Properties
        public IDeck Deck { get; }

        public List<ICard> Hand { get; }

        public IDeck DiscardPile { get; }

        public SODeck StartDeck;

        public FarmZone Farm;

        public StableZone Stables;

        public TarvernZone Taverna;


        #endregion

        //public static Tabletop Instance { get; private set; }

        //private void Awake()
        //{
        //    if (Instance != null && Instance != this)
        //    {
        //        Destroy(gameObject);
        //        return;
        //    }

        //    Instance = this;
        //    // Para mantenerlo entre escenas, creo que no sera necesario !!!
        //    DontDestroyOnLoad(gameObject);
        //}

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
        #endregion

        #region Private methods

        #endregion
    }
}