using UnityEngine;

[CreateAssetMenu(fileName = "New Cow", menuName = "Cards/Seed")]
public class BaseSeed : CardTemplate
{
    public BaseSeed()
    {
        name = "Seed";
        lifeCycleDays = 1;
    }

    private void Awake()
    {
        // Cargar los sprites directamente desde la carpeta Resources
        artwork = Resources.Load<Sprite>("Cards/Seed");
        baseCard = Resources.Load<Sprite>("Base");
    }

}