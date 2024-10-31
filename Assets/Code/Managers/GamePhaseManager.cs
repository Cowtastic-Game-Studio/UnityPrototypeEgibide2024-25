using UnityEngine;

namespace CowtasticGameStudio.MuuliciousHarvest
{
    public class GamePhaseManager
    {
        public IGamePhase CurrentPhase { get; private set; }


        public GamePhaseManager()
        {
            SetPhase(new StartDayPhase());
        }

        public void SetPhase(IGamePhase newPhase)
        {
            if (CurrentPhase != null)
            {
                CurrentPhase.EndPhase();
            }
            CurrentPhase = newPhase;
            CurrentPhase.EnterPhase();
        }

        public void NextPhase()
        {
            //TODO: Sprint2 Check antes de pasar a la siguiente fase


            Debug.Log("enter next phase");
            if (CurrentPhase is StartDayPhase)
            {
                Debug.Log("estas en la fase de PlaceCardsPhase");
                SetPhase(new PlaceCardsPhase());
            }
            else if (CurrentPhase is PlaceCardsPhase)
            {
                Debug.Log("estas en la fase de ActionPointsPhase");
                SetPhase(new ActionPointsPhase());
            }
            else if (CurrentPhase is ActionPointsPhase)
            {
                Debug.Log("estas en la fase de MarketPhase");
                SetPhase(new MarketPhase());
            }
            else if (CurrentPhase is MarketPhase)
            {
                Debug.Log("estas en la fase de StartDayPhase");
                SetPhase(new StartDayPhase());
            }
        }

        public void Update()
        {
            if (CurrentPhase != null)
            {
                CurrentPhase.ExecutePhase();
            }
        }


    }
}