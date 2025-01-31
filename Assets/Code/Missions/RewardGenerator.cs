using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

                //TODO: Añadir 1 carta de vaca veloz
                //CardManager.
            };

            return new Reward(rewardReceiveDelegate);
        }

    }
}
