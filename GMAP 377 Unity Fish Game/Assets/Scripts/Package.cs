using UnityEngine;

public class Package : MonoBehaviour
{
    private int destination_label;
    private Delivery delivery;
    private float cooldown = 3f;

    void Update()
    {
        cooldown -= Time.deltaTime;
    }

    public void CreatePackage(Vector3 position, int destination_label, Delivery delivery)
    {
        this.gameObject.transform.position = position;
        this.destination_label = destination_label;
        this.delivery = delivery;
    }

    public float GetCooldown()
    {
        return cooldown;
    }

    public Vector2 GetLocation()
    {
        return new Vector2(this.gameObject.transform.position.x, this.gameObject.transform.position.y);
    }

    public Delivery GetDelivery()
    {
        return delivery;
    }

    public int GetLabel()
    {
        return destination_label;
    }
}