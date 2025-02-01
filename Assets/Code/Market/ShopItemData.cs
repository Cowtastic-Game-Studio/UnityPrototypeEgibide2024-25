namespace CowtasticGameStudio.MuuliciousHarvest
{
    struct ShopItemData
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
            if (currentDay >= dayToUnlock)
                isActive = true;
        }
    }
}
