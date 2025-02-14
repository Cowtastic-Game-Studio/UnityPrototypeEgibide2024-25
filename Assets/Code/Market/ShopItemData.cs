namespace CowtasticGameStudio.MuuliciousHarvest
{
    public class ShopItemData
    {
        public bool isActive;
        public CardTemplate cardTemplate;
        public int dayToUnlock;



        public ShopItemData(bool isActive, CardTemplate cardTemplate)
        {
            this.isActive = isActive;
            this.cardTemplate = cardTemplate;
            this.dayToUnlock = cardTemplate.dayToUnlock;
        }

        public void CheckUnlock(int currentDay)
        {
            if (dayToUnlock <= currentDay)
            {
                if (!isActive)
                {
                    MessageManager.Instance.ShowMessage("New item unlocked: " + cardTemplate.name, 2);
                }
                isActive = true;

            }
        }
    }
}
