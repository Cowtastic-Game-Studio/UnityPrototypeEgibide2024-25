using CowtasticGameStudio.MuuliciousHarvest;
using UnityEngine.XR;

public class StartDayPhase : IGamePhase
    {
    	public DeckAndHandManager hand;
        public void EnterPhase()
        {
            // Llama a Robo
            hand.Draw();
        }

        public void ExecutePhase()
        {
            
        }

        public void Mulligan()
        {
            //Llama a Mulligan
            hand.Mulligan();
        }
        public void EndPhase()
        {
            
        }
    }