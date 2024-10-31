using UnityEngine;

namespace CowtasticGameStudio.MuuliciousHarvest
{
    public class Tabletop : MonoBehaviour, ITabletop
    {
        #region Properties

        public CardManager CardManager;

        public FarmZone Farm;

        public StableZone Stables;

        public TarvernZone Taverna;

        //public DeckAndHandManager DeckAndHandManager;

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