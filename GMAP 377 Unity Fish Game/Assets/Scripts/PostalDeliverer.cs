using UnityEngine;

public class PostalDeliverer: MonoBehaviour
{
    [SerializeField] private GameObject delivery_prefab;
    private Vector3[] destinations = new Vector3[4];
    private Delivery[] deliveries = new Delivery[2];
    private int[] delivery_destinations = new int[2];

    private int fish_flakes = 0;

    void Start()
    {
        destinations[0] = new Vector3(62f, -40f, 0f);
        destinations[1] = new Vector3(42f, 0f, 0f);
        destinations[2] = new Vector3(-5f, -36f, 0f);
        destinations[3] = new Vector3(-42f, -3f, 0f);

        delivery_destinations[0] = -1;
        delivery_destinations[1] = -1;
    }

    public void OrderDelivery(int delivery_size)
    {
        int random_number = Random.Range(0, 3);
        if (delivery_destinations[0] == -1)
        {
            GameObject instance = Instantiate(delivery_prefab);
            Delivery delivery = instance.GetComponent<Delivery>();
            if (delivery_destinations[1] == -1)
            {
                random_number = Random.Range(0, 4);
            }
            else
            {
                if (random_number == delivery_destinations[1])
                {
                    random_number = 3;
                }
            }
            delivery.SetDelivery(0, destinations[random_number].x, destinations[random_number].y, delivery_size, this);
        }
        else if (delivery_destinations[1] == -1)
        {
            GameObject instance = Instantiate(delivery_prefab);
            Delivery delivery = instance.GetComponent<Delivery>();
            if (random_number == delivery_destinations[0])
            {
                random_number = 3;
            }
            delivery.SetDelivery(1, destinations[random_number].x, destinations[random_number].y, delivery_size, this);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Delivery"))
        {
            Delivery delivery = other.gameObject.GetComponent<Delivery>();
            int index = delivery.GetIndex();
            deliveries[index] = null;
            delivery_destinations[index] = -1;
            fish_flakes += delivery.Complete();
        }
    }
}
