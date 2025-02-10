using UnityEngine.UI;


namespace CowtasticGameStudio.MuuliciousHarvest
{
    public class MarketPhase : IGamePhaseWUndo
    {
        #region Properties

        #region Property: Phase

        public GamePhaseTypes Phase { get { return GamePhaseTypes.Market; } }

        #endregion

        #endregion


        public Button cowPage, CustomerPage, WheatPage;
        public ActionManager<ICommand> ActionManager { get; private set; }

        public MarketPhase()
        {
            // Inicializar el ActionManager
            ActionManager = new ActionManager<ICommand>();
        }

        public void EnterPhase()
        {
            // C�digo para entrar en la fase
            int currentDay = GameManager.Instance.GameCalendar.CurrentDay;
            GameManager.Instance.Tabletop.NewMarketManager.CheckDay(currentDay);
            if (currentDay > 1)
            {
                GameManager.Instance.Tabletop.NewMarketManager.RestartMarket();
            }

            GameManager.Instance.Tabletop.DiscardManager.ResetDiscardCount();
        }

        public void ExecutePhase()
        {
            // C�digo que define la l�gica de la fase
            GameManager.Instance.Tabletop.NewMarketManager.UpdateMarket();
        }


        public void EndPhase()
        {
            // C�digo para finalizar la fase

            //pasar al siguiente dia
            GameManager.Instance.GameCalendar.NextDay();

            // Establecemos los PAs en su valor por defecto
            GameManager.Instance.Tabletop.StorageManager.RestartPA();
            GameManager.Instance.Tabletop.HUDManager.UpdateResources();

            //StatisticsManager.Instance.ShowStatistics();

            GameManager.Instance.Tabletop.DiscardManager.CloseMenu();
        }
    }
}

