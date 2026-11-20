using System.Linq;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;

public class QuotaUIScript : MonoBehaviour
{
    [SerializeField] GameObject QuotaUI;
    [SerializeField] Quota quotaScript;

    void Update()
    {
        UpdateQuotaUI();
    }

    private void UpdateQuotaUI()
    {
        float packagesDelievered = quotaScript.packagesDelivered;
        QuotaUI.GetComponent<TextMeshProUGUI>().text = $"Quota: {packagesDelievered}/{quotaScript.goal.Length}";
    }
}
