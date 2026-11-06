using UnityEngine;
using UnityEngine.Events;

public class StaminaBarScript : MonoBehaviour
{
    [SerializeField] RectTransform staminaBar;

    [SerializeField] Transform FullTarget;
    [SerializeField] Transform EmptyTarget;

    private float maxStamina;
    private float currentStamina;

    public UnityEvent<float> IncreaseStamina;
    public UnityEvent<float> DecreaseStamina;

    private void Awake()
    {
        UpdateMaxStamina();
        currentStamina = maxStamina;
        IncreaseStamina.AddListener(AddStamina);
        DecreaseStamina.AddListener(RemoveStamina);
    }

    private void AddStamina(float StaminaChange)
    {
        currentStamina = Mathf.Min(currentStamina + StaminaChange, maxStamina);
        UpdateStaminaBar();
    }
    public void AddStaminaFromEnergyBall()
    {
        AddStamina(20.0f);
    }

    private void RemoveStamina(float StaminaChange)
    { 
        currentStamina = Mathf.Max(currentStamina - StaminaChange, 0f);
        UpdateStaminaBar();
    }
    public void HitPlayer()
    {
        this.RemoveStamina(20.0f);
    }

    public float GetStamina()
    {
        return currentStamina;
    }

    public void UpdateMaxStamina()
    { 
        maxStamina = 100f * (GameManager.instance.MaxStaminaLevel * 1.2f);
    }

    private void UpdateStaminaBar()
    {
        UpdateMaxStamina();
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
