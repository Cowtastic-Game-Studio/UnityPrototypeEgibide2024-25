using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

namespace CowtasticGameStudio.MuuliciousHarvest.Tabletop
{
    class Tabletop : MonoBehaviour, ITabletop, ICardsManager
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

        public void UseCardActionPoints(ICard card)
        {

        }
        #endregion

        #region Private methods

        #endregion
    }
}