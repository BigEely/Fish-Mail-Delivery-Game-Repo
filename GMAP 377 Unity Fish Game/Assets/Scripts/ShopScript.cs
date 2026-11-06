using TMPro;
using UnityEngine;

public class ShopScript : MonoBehaviour
{
    private int BaseSwimSpeedMaxUpgradeLevel = 5;
    private int FastSwimSpeedUpgradeMaxLevel = 5;
    private int MaxStaminaUpgradeLevel = 5;
    private int retirementPrice = 1000;

    [Header("Shop Buttons that need to be updated")]
    [SerializeField] GameObject TotalFishFlakesObj;
    [SerializeField] GameObject swimSpeedObj;
    [SerializeField] GameObject fastSwimSpeedObj;
    [SerializeField] GameObject maxStaminaObj;
    [SerializeField] GameObject retirementObj;

    private void Start()
    {
        UpdateButtons();
    }

    private bool Purchase(int price)
    {
        if (GameManager.instance.TotalFishFlakes >= price) //ITEM BOUGHT
        {
            //SoundManager.PlaySound(SoundType.{VALID_PURCHASE})
            GameManager.instance.TotalFishFlakes -= price;
            return true;
        }
        else //Not Enough Money
        {
            //SoundManager.PlaySound(SoundType.{INVALID_PURCHASE})
            return false;
        }
    }

    public void UpgradeBaseSwimSpeed()
    {
        if (GameManager.instance.BaseSwimSpeedLevel >= BaseSwimSpeedMaxUpgradeLevel)
        {
            return;
        }
        int price = 100 * (GameManager.instance.BaseSwimSpeedLevel * 2);
        if (!Purchase(price))
        {
            return;
        }

        GameManager.instance.BaseSwimSpeedLevel += 1;
        UpdateButtons();
    }

    public void UpgradeFastSwimSpeed()
    {
        if (GameManager.instance.FastSwimSpeedLevel >= FastSwimSpeedUpgradeMaxLevel)
        {
            return;
        }
        int price = 100 * (GameManager.instance.FastSwimSpeedLevel * 2);
        if (!Purchase(price))
        {
            return;
        }

        GameManager.instance.FastSwimSpeedLevel += 1;
        UpdateButtons();
    }

    public void UpgradeMaxStamina()
    {
        if (GameManager.instance.MaxStaminaLevel >= MaxStaminaUpgradeLevel)
        {
            return;
        }
        int price = 100 * (GameManager.instance.MaxStaminaLevel * 2);
        if (!Purchase(price))
        {
            return;
        }

        GameManager.instance.MaxStaminaLevel += 1;
        UpdateButtons();
    }

    public void Retire(GameObject winScreen)
    {
        if (!Purchase(retirementPrice))
        {
            return;
        }

        winScreen.SetActive(true);
    }

    private void UpdateButtons()
    {
        TotalFishFlakesObj.GetComponent<TextMeshProUGUI>().text = $"{GameManager.instance.TotalFishFlakes} FishFlakes";

        int swimSpeedPrice = 100 * (GameManager.instance.BaseSwimSpeedLevel * 2);
        int fastSwimPrice = 100 * (GameManager.instance.FastSwimSpeedLevel * 2);
        int maxStaminaPrice = 100 * (GameManager.instance.MaxStaminaLevel * 2);
        swimSpeedObj.GetComponent<TextMeshProUGUI>().text = $"Swim Speed ({GameManager.instance.BaseSwimSpeedLevel}/{BaseSwimSpeedMaxUpgradeLevel})\n {swimSpeedPrice} Fish Flakes";
        fastSwimSpeedObj.GetComponent<TextMeshProUGUI>().text = $"Fast Swim Speed ({GameManager.instance.FastSwimSpeedLevel}/{FastSwimSpeedUpgradeMaxLevel})\n {fastSwimPrice} Fish Flakes";
        maxStaminaObj.GetComponent<TextMeshProUGUI>().text = $"Max Stamina ({GameManager.instance.MaxStaminaLevel}/{MaxStaminaUpgradeLevel})\n {maxStaminaPrice} Fish Flakes";
    }
}
