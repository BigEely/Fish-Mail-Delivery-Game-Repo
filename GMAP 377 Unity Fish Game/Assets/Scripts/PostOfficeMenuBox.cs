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

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Collider2D hit = Physics2D.OverlapPoint(mousePosition);

            if (hit == GetComponent<Collider2D>())
            {
                postal_deliverer.OrderDelivery(delivery_size);
                Debug.Log("Object clicked!");
            }
        }
    }
}
