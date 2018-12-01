using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractibleKitchenElement : MonoBehaviour
{
    public GameObject interactionButton;
    public TextMesh buttonText;

    public virtual void PlayerReached(Player player)
    {
    }

    public virtual void PlayerLeft(Player player)
    {
    }

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
