using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VegetableBox : MonoBehaviour
{
    public GameObject interactionButton;
    public TextMesh buttonText;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            EnableOrDisableInteractionButton(true, collision.GetComponent<Player>().inputConfig.pickKey.ToString());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        EnableOrDisableInteractionButton(false);
    }

    void EnableOrDisableInteractionButton(bool enable, string buttonName = "")
    {
        interactionButton.SetActive(enable);

        if(enable)
        {
            buttonText.text = buttonName;
        }
    }    
}
