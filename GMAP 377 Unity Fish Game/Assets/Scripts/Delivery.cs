using System;
using Unity.VisualScripting;
using UnityEngine;

public class Delivery: MonoBehaviour
{
    private float destination_x;
    private float destination_y;

    private int delivery_index;

    private int delivery_type;

    private int reward;

    private PostalDeliverer player;
    public GameObject gps;

    [SerializeField] private GameObject package_prefab;
    private Package package;
    private bool package_spawned = false;

    void Update()
    {
        if (package_spawned)
        {
            Debug.Log("Package Spawned");
            Vector2 package_location = package.GetLocation();
            SetDestination(package_location.x, package_location.y);
        }
        Vector3 player_pos = player.gameObject.transform.position;

        Vector3 trajectory = new Vector3(destination_x, destination_y, 0f) - player_pos;
        Vector3 trajectory_norm = trajectory.normalized;
        Vector3 gps_pos = player_pos + trajectory_norm*3.0f;

        gps.transform.position = gps_pos;

        float new_rot_rad = Mathf.Atan2(trajectory_norm.y, trajectory_norm.x);
        float new_rot_deg = new_rot_rad * Mathf.Rad2Deg - 90f;
        gps.transform.eulerAngles = new Vector3(0f, 0f, new_rot_deg);
    }

    public float GetDestinationX()
    {
        return destination_x;
    }
    public float GetDestinationY()
    {
        return destination_y;
    }
    public int GetIndex()
    {
        return delivery_index;
    }
    public bool GetPackageSpawned()
    {
        return package_spawned;
    }

    public void SetDelivery(int index, float dest_x, float dest_y, int delivery_size, PostalDeliverer triggering_player)
    {
        delivery_index = index;
        destination_x = dest_x;
        destination_y = dest_y;
        delivery_type = delivery_size;
        reward = 100 + delivery_type*50;
        transform.position = new Vector3(destination_x, destination_y, 0f);
        player = triggering_player;
    }

    public int Complete()
    {
        // pay reward
        Destroy(gps);
        Destroy(gameObject);
        return reward;
    }

    public void SpawnPackage(Vector3 player_position, int delivery_location)
    {
        // Create package object that has this delivery object and the delivery location marker
        GameObject instance = Instantiate(package_prefab);
        package = instance.GetComponent<Package>();
        package.CreatePackage(player_position, delivery_location, this);
        package.gameObject.SetActive(true);
        package_spawned = true;
    }

    public void DestroyPackage(Vector3 destination)
    {
        if (package != null)
        {
            package_spawned = false;
            Destroy(package.gameObject);
        }
        SetDestination(destination.x, destination.y);
    }

    private void SetDestination(float x, float y)
    {
        destination_x = x;
        destination_y = y;
    }

    public void SetIndex(int ind)
    {
        delivery_index = ind;
    }
}
