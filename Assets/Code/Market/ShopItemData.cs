namespace CowtasticGameStudio.MuuliciousHarvest
{
    struct ShopItemData
    {
        public bool isActive;
        public CardTemplate cardTemplate;
        public int dayToUnlock;

        public ShopItemData(bool isActive, CardTemplate cardTemplate, int dayToUnlock)
        {
            this.isActive = isActive;
            this.cardTemplate = cardTemplate;
            this.dayToUnlock = dayToUnlock;
        }
    }
}
