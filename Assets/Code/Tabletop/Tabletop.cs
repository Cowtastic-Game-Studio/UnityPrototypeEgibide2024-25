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

        public DeckAndHandManager DeckAndHandManager { get; private set; }

        List<ICard> ICardsManager.Hand => throw new System.NotImplementedException();

        #endregion

        private void Awake()
        {

        }

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