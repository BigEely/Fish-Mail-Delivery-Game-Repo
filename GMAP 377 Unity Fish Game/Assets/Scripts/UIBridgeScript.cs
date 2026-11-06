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

    public void WipeSaveData()
    {
        GameManager.instance.WipeData();
    }

}
