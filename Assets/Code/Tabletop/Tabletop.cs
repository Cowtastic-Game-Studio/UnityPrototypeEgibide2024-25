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

        public StorageManager StorageManager;

        //List<ICard> ICardsManager.Hand => throw new System.NotImplementedException();

        #endregion

        private void Awake()
        {
            StorageManager = new StorageManager();
            //CardManager = new CardManager();

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