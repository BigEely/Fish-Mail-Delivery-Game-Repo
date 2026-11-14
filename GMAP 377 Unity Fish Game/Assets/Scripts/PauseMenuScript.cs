using DG.Tweening.Core.Easing;
using UnityEngine;

public class PauseMenuScript : MonoBehaviour
{
    [SerializeField] KeyCode Button = KeyCode.Escape;
    [SerializeField] GameObject PauseCanvasGroup;

    private void Start()
    {
        PauseCanvasGroup.SetActive(false);
    }

    void Update()
    {
        GameManager.GameState state = GameManager.instance.getCurrentGameState();
        
        if (Input.GetKeyDown(Button)) //Checks to see if player paused via pause button (not through UI)
        {
            if (state == GameManager.GameState.pause) //If the game is already pause then unpause
            {
                GameManager.instance.Unpause(GameManager.instance.previousState);
            }
            else //Else pause the game
            {
                GameManager.instance.Pause();
            }
        }
        UpdatePauseUI();
    }

    private void UpdatePauseUI()
    { 
        bool isPaused = GameManager.instance.getCurrentGameState() == GameManager.GameState.pause;
        PauseCanvasGroup.SetActive(isPaused);
    }
}
