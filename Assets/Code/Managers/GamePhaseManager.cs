using UnityEngine;

namespace CowtasticGameStudio.MuuliciousHarvest
{
    public enum GamePhaseTypes
    {
        SetUp = 0,
        StartDay = 1,        
        PlaceCards = 2,
        Action = 3,
        Market = 4
    }


    public class GamePhaseManager
    {
        //TODO: Añadir contador de turnos(dias)

        public IGamePhase CurrentPhase { get; private set; }

        public GamePhaseTypes CurrentPhaseType {  get  {  return CurrentPhase.Phase; } }


        public GamePhaseManager()
        {
            SetPhase(new SetUpPhase());
        }

        public void SetPhase(IGamePhase newPhase)
        {
            if (CurrentPhase != null)
            {
                CurrentPhase.EndPhase();
            }

            // Debug log para obtener el nombre de la clase de newPhase
            Debug.Log("Estableciendo nueva fase: " + newPhase.GetType().Name);

            CurrentPhase = newPhase;
            CurrentPhase.EnterPhase();
        }

        public void NextPhase()
        {
            //TODO: Sprint2 Check antes de pasar a la siguiente fase
            if (CurrentPhase is SetUpPhase)
            {
                SetPhase(new StartDayPhase());
            }
            else if (CurrentPhase is StartDayPhase)
            {
                SetPhase(new PlaceCardsPhase());
            }
            else if (CurrentPhase is PlaceCardsPhase)
            {
                SetPhase(new ActionPointsPhase());
            }
            else if (CurrentPhase is ActionPointsPhase)
            {
                SetPhase(new MarketPhase());
            }
            else if (CurrentPhase is MarketPhase)
            {
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