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
                //if (MissionsManager.Instance.IsTutorialEnabled)
                //{
                //    return;
                //}
                GameManager.Instance.Tabletop.StorageManager.AddResourceUpToMax(10, GameResource.Muuney, true);
                GameManager.Instance.Tabletop.CardManager.BuyCard("FastCow", 0);
                GameManager.Instance.Tabletop.HUDManager.UpdateResources();

                //Debug.LogWarning("Tutorial reward has been received. 10 Muuneys and 1 Fast Cow");
                MessageManager.Instance.ShowMessage("Tutorial reward has been received. 10 Muuneys and 1 Fast Cow");
                Debug.LogError("Tutorial reward has been received. 10 Muuneys and 1 Fast Cow");
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
                Debug.LogError("Muuney has been added: " + roundedMuuney);

                GameManager.Instance.Tabletop.HUDManager.UpdateResources();
            };

            return new Reward(rewardReceiveDelegate);
        }

        public static Reward CreateGoalReward(int muuney)
        {
            UnityAction rewardReceiveDelegate = () =>
            {
                GameManager.Instance.Tabletop.StorageManager.AddResourceUpToMax(muuney, GameResource.Muuney, true);
                GameManager.Instance.Tabletop.HUDManager.UpdateResources();
                Debug.LogWarning("Muuney has been added: " + muuney);
            };

            return new Reward(rewardReceiveDelegate);
        }

    }
}
