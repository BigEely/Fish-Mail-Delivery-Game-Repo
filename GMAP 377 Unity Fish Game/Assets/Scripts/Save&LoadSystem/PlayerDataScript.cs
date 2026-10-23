using UnityEngine;

[System.Serializable] //Makes script able to be saved in file
public class PlayerDataScript
{
    public int TotalFishFlakes = 1;

    public PlayerDataScript (GameManager gameManager) //Fetches levelsComplete from GameManager script and uses int for saveData
    {
        TotalFishFlakes = gameManager.TotalFishFlakes;
    }
}
