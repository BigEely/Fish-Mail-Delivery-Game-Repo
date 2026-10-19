using UnityEngine;
using UnityEngine.Rendering;

public class EnergyBall: MonoBehaviour
{
    public StaminaBarScript stamina_bar;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            stamina_bar.AddStaminaFromEnergyBall();
            Destroy(gameObject);
        }
    }
}
