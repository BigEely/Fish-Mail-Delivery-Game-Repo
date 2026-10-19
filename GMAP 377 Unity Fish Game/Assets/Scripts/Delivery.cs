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

    void Update()
    {
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

    public void Complete()
    {
        // pay reward
        Destroy(gps);
        Destroy(gameObject);
    }
}
