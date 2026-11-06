using UnityEngine;

[System.Serializable] //Makes script able to be saved in file
public class PlayerDataScript
{
    public int TotalFishFlakes = 1;

    public int BaseSwimSpeedLevel = 1;
    public int FastSwimSpeedLevel = 1;
    public int MaxStaminaLevel = 1;

    public int CurrentDay = 1;


    public PlayerDataScript (GameManager gameManager) //Fetches variables from GameManager script and uses values for saveData
    {
        TotalFishFlakes = gameManager.TotalFishFlakes;

        BaseSwimSpeedLevel = gameManager.BaseSwimSpeedLevel;
        FastSwimSpeedLevel = gameManager .FastSwimSpeedLevel;
        MaxStaminaLevel = gameManager .MaxStaminaLevel;

        CurrentDay = gameManager.CurrentDay;
    }
}
