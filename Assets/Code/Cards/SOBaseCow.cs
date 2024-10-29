using UnityEngine;

[CreateAssetMenu(fileName = "New Cow", menuName = "Cards/Cow")]
public class BaseCow : CardTemplate
{
    public BaseCow()
    {
        name = "Cow";
        lifeCycleDays = 2;
    }

    private void Awake()
    {
        // Cargar los sprites directamente desde la carpeta Resources
        artwork = Resources.Load<Sprite>("Cards/Cow");
        baseCard = Resources.Load<Sprite>("Base");
    }

}