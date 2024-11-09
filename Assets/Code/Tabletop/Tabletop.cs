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

        [Header("Board")]
        [Space(1)]
        public GameObject board;

        [Header("Zones")]
        [Space(3)]
        public FarmZone Farm;
        public StableZone Stables;
        public TarvernZone Taverna;

        #endregion

        private void Awake()
        {
            //StorageManager = new StorageManager();
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