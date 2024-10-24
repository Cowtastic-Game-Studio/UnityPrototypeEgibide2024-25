using UnityEngine;

[CreateAssetMenu(fileName = "New Client Card", menuName = "Cards/Client")]
public class BaseClient : CardTemplate
{
    public BaseClient()
    {
        name = "Client";
        //artwork = Resources.Load<Sprite>("Base");
        lifeCycleDays = 1;
    }

}
