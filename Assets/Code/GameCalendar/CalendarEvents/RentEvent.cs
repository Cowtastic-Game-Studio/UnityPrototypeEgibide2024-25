using UnityEngine;

namespace CowtasticGameStudio.MuuliciousHarvest
{
    public class RentEvent : CalendarEvent
    {
        int rentAmount = 10;

        /// <summary>
        /// Constructor para inicializar el evento generico.
        /// </summary>
        public RentEvent()
            : base("¡Dia del alquiler!", "Paga o sin cabeza te quedas jajaja", 1)
        {

        }

        public override void ApplyEffects()
        {
            GameManager.Instance.Tabletop.HUDManager.UpdateResources();
        }

        public override void EndEvent()
        {
            int currentMuuney = GameManager.Instance.Tabletop.StorageManager.GetResourceAmounts(GameResource.Muuney);

            if (rentAmount > currentMuuney)
            {
                //TODO poner aqui la fncion de GameOver cuando exista
                Debug.LogWarning("piu piu GAME OVER piu piu");
                MenuManager.Instance.GameOverScene();
            }
            else
            {
                GameManager.Instance.Tabletop.StorageManager.WasteMuuney(rentAmount);
                GameManager.Instance.Tabletop.HUDManager.UpdateResources();

                //rentAmount = rentAmount + 3;
                rentAmount = rentAmount + GameManager.Instance.rentAdd;
            }
        }

        public override void InitEvent()
        {

        }
    }
}
