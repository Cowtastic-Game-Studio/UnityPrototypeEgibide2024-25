namespace CowtasticGameStudio.MuuliciousHarvest
{
    public class StartDayPhase : IGamePhase
    {
        //TODO: Gestionar el numero de veces que se puede hacer mulligan(Maximo 4 por fase)


        /// <summary>
        /// Acciones a ejecutar al inicio de la fase.
        /// </summary>
        public void EnterPhase()
        {
            // Llama a Robo
            GameManager.Instance.Tabletop.CardManager.DrawFromDeck();
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
            GameManager.Instance.Tabletop.CardManager.Mulligan();
        }

        /// <summary>
        /// Acciones a ejecutar al finalizar la fase
        /// </summary>
        public void EndPhase()
        {

        }
    }
}