using System.ComponentModel;
using UnityEngine;

public class UIBridgeScript : MonoBehaviour
{
    //Script is used to bridge the connection from UI manager in manager scene to its functions from other scenes

    public void LoadScene(string sceneName)
    { 
        UIManager.instance.LoadScene(sceneName);
    }

    public void ToggleObject(GameObject subject)
    {
        UIManager.instance.ToggleObject(subject);
    }

    public void Buy(int price)
    {
        if (GameManager.instance.TotalFishFlakes >= price) //ITEM BOUGHT
        {
            //SoundManager.PlaySound(SoundType.{VALID_PURCHASE})
            GameManager.instance.TotalFishFlakes -= price;
        }
        else //Not Enough Money
        {
            //SoundManager.PlaySound(SoundType.{INVALID_PURCHASE})
        }
    }
}
