using CowtasticGameStudio.MuuliciousHarvest;
using UnityEngine.XR;

public class StartDayPhase : IGamePhase
    {
    	public DeckAndHandManager hand;
        /// <summary>
        /// Acciones a ejecutar al inicio de la fase.
        /// </summary>
        public void EnterPhase()
        {
            // Llama a Robo
            hand.Draw();
        }
        /// <summary>
        /// Acciones a Realizar durante la fase
        /// </summary>
                            
        public void ExecutePhase()
        {
            
        }
        /// <summary>
        /// Funcion que llama al mulligan
        /// </summary>
        public void Mulligan()
        {
            //Llama a Mulligan
            hand.Mulligan();
        }
        /// <summary>
        /// Acciones a ejecutar al finalizar la fase
        /// </summary>
        public void EndPhase()
        {
            
        }
    }