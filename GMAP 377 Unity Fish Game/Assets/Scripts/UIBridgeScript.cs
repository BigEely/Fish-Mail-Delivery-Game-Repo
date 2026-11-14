using System.ComponentModel;
using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public void PlayGame()
    {
        GameManager.instance.switchState(GameManager.GameState.game);
        LoadScene("GamePlayScene");
    }

    public void ToTitleScene()
    {
        GameManager.instance.switchState(GameManager.GameState.mainMenu);
        LoadScene("TitleScene");
    }

    public void RestartDay()
    {
        GameManager.instance.ResetGameData();
        GameManager.instance.Unpause(GameManager.GameState.game);
        UIManager.instance.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void TogglePause()
    {
        if (GameManager.instance.getCurrentGameState() == GameManager.GameState.pause) //If the game is already pause then unpause
        {
            GameManager.instance.Unpause(GameManager.instance.previousState);
        }
        else //Else pause the game
        {
            GameManager.instance.Pause();
        }
    }

    public void WipeSaveData()
    {
        GameManager.instance.WipeData();
    }

}
