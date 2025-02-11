using TMPro;

using UnityEngine;

namespace CowtasticGameStudio.MuuliciousHarvest
{
    public class BuyButtonBehaviour : MonoBehaviour
    {
        [SerializeField] private TMP_Text Text;

        public int Price { get; private set; }


        #region Public methods

        /// <summary>
        /// Activa o desactiva el boton
        /// </summary>
        /// <param name="active"></param>
        public void SetActive(bool active)
        {
            gameObject.SetActive(active);
        }

        /// <summary>
        /// Asigna el texto al boton
        /// </summary>
        /// <param name="text"></param>
        public void SetPrice(int price)
        {
            this.Price = price;
            this.Text.text = this.Price.ToString();
        }

        public void SetNormalColor()
        {
            this.Text.color = Color.white;
        }

        public void SetVacFridayColor()
        {
            this.Text.color = Color.black;
        }

        public void SetDiscountColor()
        {
            this.Text.color = Color.magenta;
        }


        #endregion



    }
}
