using UnityEngine;

public class CowCardClass : MonoBehaviour
{

    public int[] baseCow = new int[3];
    public int[] hatCow = new int[3];
    public int[] blackCow = new int[3];
    public int[] dinnerCow = new int[3];
    //--basecow
    int basetype = 1;
    int baseprice = 1;
    int baseFoodNeed = 1;

    //-- hatcow

    int hattype = 2;
    int hatprice = 2;
    int hatFoodNeed = 1;

    //-blackcow

    int blacktype = 4;
    int blackprice = 4;
    int blackFoodNeed = 1;

    //-dinerbonecow

    int dinnertype = 3;
    int dinnerrice = 3;
    int dinnerFoodNeed = 1;

    void Awake()
    {
        baseCow[0] = basetype;
        baseCow[1] = baseprice;
        baseCow[2] = baseFoodNeed;


        hatCow[0] = hattype;
        hatCow[1] = hatprice;
        hatCow[2] = hatFoodNeed;

        blackCow[0] = blacktype;
        blackCow[1] = blackprice;
        blackCow[2] = blackFoodNeed;

        dinnerCow[0] = dinnertype;
        dinnerCow[1] = dinnerrice;
        dinnerCow[2] = dinnerFoodNeed;





    }



}
