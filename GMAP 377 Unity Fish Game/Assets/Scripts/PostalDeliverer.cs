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
            delivery_destinations[0] = random_number;
            delivery.SetDelivery(0, destinations[random_number].x, destinations[random_number].y, delivery_size, this);
            deliveries[0] = delivery;
        }
        else if (delivery_destinations[1] == -1)
        {
            GameObject instance = Instantiate(delivery_prefab);
            Delivery delivery = instance.GetComponent<Delivery>();
            if (random_number == delivery_destinations[0])
            {
                random_number = 3;
            }
            delivery_destinations[1] = random_number;
            delivery.SetDelivery(1, destinations[random_number].x, destinations[random_number].y, delivery_size, this);
            deliveries[1] = delivery;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Delivery"))
        {
            Delivery delivery = other.gameObject.GetComponent<Delivery>();
            bool package_mode = delivery.GetPackageSpawned();
            if (!package_mode)
            {
                int index = delivery.GetIndex();
                deliveries[index] = null;
                delivery_destinations[index] = -1;
                fish_flakes += delivery.Complete();
            }
        }
        else if (other.gameObject.CompareTag("Package"))
        {
            Package package = other.gameObject.GetComponent<Package>();
            float package_cooldown = package.GetCooldown();
            if (package_cooldown <= 0f)
            {
                Delivery delivery = package.GetDelivery();
                int delivery_label = package.GetLabel();
                if (delivery_destinations[0] == -1)
                {
                    delivery_destinations[0] = delivery_label;
                    delivery.DestroyPackage(destinations[delivery_label]);
                    delivery.SetIndex(0);
                    deliveries[0] = delivery;
                }
                else if (delivery_destinations[1] == -1)
                {
                    delivery_destinations[1] = delivery_label;
                    delivery.DestroyPackage(destinations[delivery_label]);
                    delivery.SetIndex(1);
                    deliveries[1] = delivery;
                }
            }
        }
    }

    public void DropPackage()
    {
        int package_destination;
        Delivery package;
        // Remove first available delivery from inventory
        if (delivery_destinations[0] != -1)
        {
            // Remove first delivery
            package_destination = delivery_destinations[0];
            delivery_destinations[0] = -1;
            package = deliveries[0];
            deliveries[0] = null;
        }
        else if (delivery_destinations[1] != -1)
        {
            // Remove second delivery
            package_destination = delivery_destinations[1];
            delivery_destinations[1] = -1;
            package = deliveries[1];
            deliveries[1] = null;
        }
        else
        {
            return;
        }
        // Tell delivery to spawn a pickup package object
        package.SpawnPackage(this.transform.position, package_destination);
    }
}
