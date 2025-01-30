using System;
using UnityEngine.Events;

namespace CowtasticGameStudio.MuuliciousHarvest.Assets.Code.Missions
{
    /// <summary>
    /// Recompensa
    /// </summary>
    public class Reward
    {

        #region Properties

        #region Property: ReceiveDelegate

        /// <summary>
        /// Delegado de la funcion de recibir
        /// </summary>
        private UnityAction ReceiveDelegate { get; set; }

        #endregion

        #region Property: HasBeenReceived

        /// <summary>
        /// Flag que indica si se ha recibido la recompensa
        /// </summary>
        public bool HasBeenReceived { get; set; }

        #endregion

        #endregion

        #region Constructor

        public Reward(UnityAction receiveDelegate)
        {
            if (receiveDelegate == null)
                throw new ArgumentNullException(nameof(receiveDelegate));

            this.HasBeenReceived = false;
            this.ReceiveDelegate = receiveDelegate;
        }

        #endregion

        #region Pulic methods

        /// <summary>
        /// Recibe la recompensa
        /// </summary>
        public void Receive()
        {
            //Comprueba que no haya sido recibido
            if (!this.HasBeenReceived)
            {
                this.ReceiveDelegate.Invoke();
                this.HasBeenReceived = true;
            }
                
        }

        #endregion

    }
}
