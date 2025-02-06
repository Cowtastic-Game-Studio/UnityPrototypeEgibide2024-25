using System.Collections.Generic;

using UnityEngine;

namespace CowtasticGameStudio.MuuliciousHarvest
{
    public class SlotList : MonoBehaviour
    {
        public List<GameObject> slotsList;
        [SerializeField] private GameObject nextPage;
        [SerializeField] private GameObject previousPage;

        public int totalPage;
        public int currentPage;

        private void Update()
        {
            if (currentPage == 1)
            {
                previousPage.SetActive(false);
            }
            else
            {
                previousPage.SetActive(true);
            }

            if (totalPage > 1)
            {
                nextPage.SetActive(true);
            }
            else
            {
                nextPage.SetActive(false);
            }

            if (totalPage == currentPage)
            {
                nextPage.SetActive(false);
            }
        }

        public void NextPage()
        {
            if (currentPage < totalPage)
                currentPage++;
        }

        public void PreviousPage()
        {
            if (currentPage > 1)
                currentPage--;
        }
    }
}
