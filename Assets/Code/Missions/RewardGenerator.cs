using UnityEngine;
using UnityEngine.Events;

namespace CowtasticGameStudio.MuuliciousHarvest.Assets.Code.Missions
{
    internal static class RewardGenerator
    {

        /// <summary>
        /// Genera la recompensa del tutorial
        /// Añade 10 de muuney y 1 añade una carta de vaca veloz
        /// </summary>
        /// <returns></returns>
        public static Reward CreateTutorialReward()
        {
            UnityAction rewardReceiveDelegate = () =>
            {
                //TODO: Añadir 10 de oro
                GameManager.Instance.Tabletop.StorageManager.AddResourceUpToMax(10, GameResource.Muuney, true);

                //TODO: Añadir 1 carta de vaca veloz
                GameManager.Instance.Tabletop.CardManager.BuyCard("FastCow");

                GameManager.Instance.Tabletop.HUDManager.UpdateResources();
            };

            return new Reward(rewardReceiveDelegate);
        }

        /// <summary>
        /// Genera la recompensa semanal
        /// Añade el 20% de muuuney
        /// </summary>
        /// <returns></returns>
        public static Reward CreateWeekReward()
        {
            UnityAction rewardReceiveDelegate = () =>
            {
                // Calculo del 20 porciento del dinero total que puede tener el jugador
                int currentMaxMuuney = GameManager.Instance.Tabletop.StorageManager.GetMaxResourceAmounts(GameResource.Muuney);
                double tenPercentMuuney = currentMaxMuuney * 0.20;
                int roundedMuuney = Utils.RoundMuuney((int) tenPercentMuuney);

                // Añadir dinero y actualizar hud
                GameManager.Instance.Tabletop.StorageManager.AddResourceUpToMax(roundedMuuney, GameResource.Muuney, true);
                GameManager.Instance.Tabletop.HUDManager.UpdateResources();
                Debug.Log("Se ha añadido Muuney: " + roundedMuuney);

                GameManager.Instance.Tabletop.HUDManager.UpdateResources();
            };

            return new Reward(rewardReceiveDelegate);
        }

        // TODO hablar con Mikel para ver si esto le sirve para los rewards
        public static Reward CreateGoalReward(int muuney)
        {
            UnityAction rewardReceiveDelegate = () =>
            {
                GameManager.Instance.Tabletop.StorageManager.AddResourceUpToMax(muuney, GameResource.Muuney, true);
                GameManager.Instance.Tabletop.HUDManager.UpdateResources();
            };

            return new Reward(rewardReceiveDelegate);
        }

    }
}
