using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndOfDaySummaryScript : MonoBehaviour
{
    [SerializeField] GameObject FishFlakesCollected;
    [SerializeField] GameObject TimeReward;
    [SerializeField] GameObject Packages;
    [SerializeField] GameObject PackagesLost;
    [SerializeField] GameObject Earned;
    [SerializeField] GameObject Total;

    public void CalculateSummary(int collectedFlakes, int timeReward, int packageRewards, int totalPackageFlakes)
    {
        int timeRewardValue = GameManager.instance.timeRemaining * 15; //15 fishflakes for each second
        int packagesLostValue = packageRewards - totalPackageFlakes;
        int earnedValue = collectedFlakes + timeRewardValue + packageRewards + packagesLostValue;
        int total = GameManager.instance.TotalFishFlakes + earnedValue;
        if (total < 0)
        {
            total = 0;
        }
        GameManager.instance.TotalFishFlakes = total;

        FishFlakesCollected.GetComponent<TextMeshProUGUI>().text = $"Fish Flakes Collected: {collectedFlakes}";
        TimeReward.GetComponent<TextMeshProUGUI>().text = $"Time Reward: {timeRewardValue}";
        Packages.GetComponent<TextMeshProUGUI>().text = $"Packages: {packageRewards}";
        PackagesLost.GetComponent<TextMeshProUGUI>().text = $"Packages Lost: {packagesLostValue}";
        Earned.GetComponent<TextMeshProUGUI>().text = $"Earned: {earnedValue}";
        Total.GetComponent<TextMeshProUGUI>().text = $"Total: {total}";
    }
}
