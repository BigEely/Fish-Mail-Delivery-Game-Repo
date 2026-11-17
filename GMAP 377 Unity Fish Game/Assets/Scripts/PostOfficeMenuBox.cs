using UnityEngine;
using UnityEngine.InputSystem;

public class PostOfficeMenuBox: MonoBehaviour
{
    [SerializeField] private int delivery_size;
    [SerializeField] private PostalDeliverer postal_deliverer;

    void Start()
    {
        this.gameObject.SetActive(false);
        Physics.queriesHitTriggers = true;
        Physics2D.queriesHitTriggers = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            postal_deliverer.OrderDelivery(delivery_size);
        }
    }
}
