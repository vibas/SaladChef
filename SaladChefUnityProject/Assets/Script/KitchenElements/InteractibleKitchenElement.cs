using UnityEngine;

/// <summary>
/// Base Class for all interactible items in kitchen/// 
/// </summary>
public class InteractibleKitchenElement : MonoBehaviour
{
    // Interaction key hint button
    public GameObject interactionButton;
    public TextMesh buttonText;

    /// <summary>
    /// When player triggers the interactible
    /// </summary>
    /// <param name="player"></param>
    public virtual void PlayerReached(Player player){}

    /// <summary>
    /// When player exits from trigger of interactible item
    /// </summary>
    /// <param name="player"></param>
    public virtual void PlayerLeft(Player player){}

    public void EnableOrDisableInteractionButton(bool enable, string buttonName = "")
    {
        interactionButton.SetActive(enable);

        if (enable)
        {
            buttonText.text = buttonName;
        }
    }

    public void EnableOrDisableInteractionButton(GameObject button, bool enable, string buttonName = "")
    {
        button.SetActive(enable);

        if (enable)
        {
            button.transform.Find("Text").GetComponent<TextMesh>().text = buttonName;
        }
    }
}
