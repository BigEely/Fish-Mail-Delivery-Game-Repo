using UnityEngine;

public class TutorialScript : MonoBehaviour
{
    [SerializeField] GameObject CanvasGroup;

    private void Start()
    {
        if (GameManager.instance.tutorialFinished == false)
        {
            CanvasGroup.SetActive(true);
            GameManager.instance.Pause();
        }
    }

    public void FinishTutorial()
    {
        GameManager.instance.Unpause(GameManager.instance.previousState);
        CanvasGroup.SetActive(false);
        GameManager.instance.tutorialFinished = true;
    }
}
