using System.Collections.Generic;
using UnityEngine;

namespace CowtasticGameStudio.MuuliciousHarvest
{
    [CreateAssetMenu(fileName = "New Deck", menuName = "Cards/Deck")]
    public class SODeck : ScriptableObject
    {
        public List<CardTemplate> Cards;
        //public List<GameObject> Cards;
    }
}