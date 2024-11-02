namespace CowtasticGameStudio.MuuliciousHarvest
{
    public class StartDayPhase : IGamePhase
    {
        /// <summary>
        /// Acciones a ejecutar al inicio de la fase.
        /// </summary>
        public void EnterPhase()
        {
            // Llama a Robo
            GameManager.Instance.Tabletop.DrawFromDeck();
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
            GameManager.Instance.Tabletop.Mulligan();
        }

        /// <summary>
        /// Acciones a ejecutar al finalizar la fase
        /// </summary>
        public void EndPhase()
        {

        }
    }
}