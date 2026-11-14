using TMPro;
using UnityEngine;

public class PackageUIScript : MonoBehaviour
{
    [SerializeField] GameObject WeightUI;
    [SerializeField] PlayerControllerScript playerControllerScript;

    void Update()
    {
        UpdateWeightUI();
    }

    private void UpdateWeightUI()
    {
        float weight = playerControllerScript.weight;
        WeightUI.GetComponent<TextMeshProUGUI>().text = $"Weight: {weight}";
    }
}
