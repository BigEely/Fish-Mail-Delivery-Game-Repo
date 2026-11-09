using UnityEngine;

public class PlayerState: MonoBehaviour
{
    private int player_state = 0; // 0 for normal state; 1 for invincible state immediately after getting hit

    private float timer = 0f;
    private float max_time = 3f;

    [SerializeField] private Renderer render;

    void Update()
    {
        if (timer < 0f)
        {
            timer = 0f;
            player_state = 0;
            render.enabled = true;

            foreach (Transform child in this.gameObject.transform)
            {
                child.gameObject.GetComponent<Renderer>().enabled = true;
            }
        }

        if (player_state == 1)
        {
            timer -= Time.deltaTime;
            float diff = max_time - timer;
            float remainder = 2*diff % 2;
            if (remainder < 1f)
            {
                // Set visible false
                render.enabled = false;
                foreach (Transform child in this.gameObject.transform)
                {
                    child.gameObject.GetComponent<Renderer>().enabled = false;
                }
            }
            else if (remainder >= 1f)
            {
                // Set visible true
                render.enabled = true;
                foreach (Transform child in this.gameObject.transform)
                {
                    child.gameObject.GetComponent<Renderer>().enabled = true;
                }
            }
        }
    }

    public void Hit()
    {
        if (player_state == 0)
        {
            player_state = 1;
            timer = max_time;

            PostalDeliverer postal_deliverer = this.gameObject.GetComponent<PostalDeliverer>();
            postal_deliverer.DropPackage();

            PlayerControllerScript player_controller = this.gameObject.GetComponent<PlayerControllerScript>();
            player_controller.HitStamina();
        }
    }
}
