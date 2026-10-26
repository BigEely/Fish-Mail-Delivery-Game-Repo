using UnityEngine;
using UnityEngine.InputSystem;

public class PostOfficeMenuBox: MonoBehaviour
{
    [SerializeField] private int delivery_size;
    [SerializeField] private PostalDeliverer postal_deliverer;

    void Start()
    {
        this.gameObject.SetActive(false);
        postal_deliverer.OrderDelivery(delivery_size);
    }

    void Update()
    {

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Ray ray = Camera.main.ScreenPointToRay(
                Mouse.current.position.ReadValue()
            );

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.gameObject == gameObject)
                {
                    postal_deliverer.OrderDelivery(delivery_size);
                    Debug.Log("hit");
                }
            }
        }
    }
}
