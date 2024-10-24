using UnityEngine;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour
{
    public CardTemplate card;

    public Text nameText;
    public Text descriptionText;

    public Image artworkImage;
    public Image baseCardImage;

    public Text actionPointsText;

    public Text requieredTypeText;
    public Text requieredQuantityText;

    public Text producedTypeText;
    public Text producedQuantityText;

    // Use this for initialization
    void Start()
    {
        nameText.text = card.name;
        descriptionText.text = card.description;

        artworkImage.sprite = card.artwork;
        baseCardImage.sprite = card.baseCard;

        actionPointsText.text = card.actionPointsCost.ToString();

        //requieredTypeText.text = card.requieredType.ToString();
        //requieredQuantityText.text = card.requieredQuantity.ToString();

        //producedTypeText.text = card.producedType.ToString();
        //producedQuantityText.text = card.producedQuantity.ToString();
    }

}
