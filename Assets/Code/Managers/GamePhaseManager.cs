namespace CowtasticGameStudio.MuuliciousHarvest
{
    public class GamePhaseManager
    {
        private IGamePhase currentPhase;

        public void SetPhase(IGamePhase newPhase)
        {
            if (currentPhase != null)
            {
                currentPhase.EndPhase();
            }
            currentPhase = newPhase;
            currentPhase.EnterPhase();
        }

        public void NextPhase()
        {
            if (currentPhase is StartDayPhase)
            {
                SetPhase(new PlaceCardsPhase());
            }
            else if (currentPhase is PlaceCardsPhase)
            {
                SetPhase(new ActionPointsPhase());
            }
            else if (currentPhase is ActionPointsPhase)
            {
                SetPhase(new MarketPhase());
            }
            else if (currentPhase is MarketPhase)
            {
                SetPhase(new StartDayPhase()); // Comienza un nuevo d�a
            }
        }

        public void Update()
        {
            if (currentPhase != null)
            {
                currentPhase.ExecutePhase();
            }
        }
    }
}