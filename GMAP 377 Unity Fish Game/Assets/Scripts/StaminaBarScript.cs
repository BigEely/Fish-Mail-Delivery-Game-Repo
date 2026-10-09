using UnityEngine;
using UnityEngine.Events;

public class StaminaBarScript : MonoBehaviour
{
    [SerializeField] RectTransform staminaBar;

    [SerializeField] Transform FullTarget;
    [SerializeField] Transform EmptyTarget;

    private float maxStamina = 100;
    private float currentStamina;

    public UnityEvent<float> IncreaseStamina;
    public UnityEvent<float> DecreaseStamina;

    private void Awake()
    {
        currentStamina = maxStamina;
        IncreaseStamina.AddListener(AddStamina);
        DecreaseStamina.AddListener(RemoveStamina);
    }

    private void AddStamina(float StaminaChange)
    {
        currentStamina = Mathf.Min(currentStamina + StaminaChange, maxStamina);
        UpdateStaminaBar();
    }

    private void RemoveStamina(float StaminaChange)
    { 
        currentStamina = Mathf.Max(currentStamina - StaminaChange, 0f);
        UpdateStaminaBar();
    }

    public float GetStamina()
    {
        return currentStamina;
    }
    private void UpdateStaminaBar()
    {
        float staminaPercentage = currentStamina / maxStamina;
        Vector3 newPOS = Vector3.Lerp(EmptyTarget.position, FullTarget.position, staminaPercentage);
        staminaBar.position = newPOS;
    }

    private void OnDestroy()
    {
        IncreaseStamina.RemoveListener(AddStamina);
        DecreaseStamina.RemoveListener(RemoveStamina);
    }
}
