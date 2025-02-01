using System.Collections.Generic;

using UnityEngine;

namespace CowtasticGameStudio.MuuliciousHarvest
{
    public class ShopSlotList : MonoBehaviour
    {
        public List<GameObject> slotsList;
        [SerializeField] private GameObject nextPage;
        [SerializeField] private GameObject previousPage;

        private int currentPage = 0;

        public void NextPage()
        {
            if (currentPage < slotsList.Count - 1)
            {

            }
        }

        public void PreviousPage()
        {
            if (currentPage > 0)
            {
            }
        }
    }
}
