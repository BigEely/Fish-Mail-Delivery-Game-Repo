using UnityEngine;

public class SharkEnemy: MonoBehaviour
{
    private int state = 0; // 0 - Roaming State, 1 - Charging State, 2 - Returning State

    private float roaming_speed = 7f;
    private float charging_speed = 13f;

    private Vector3 home_base;
    private float base_radius = 21f;
    private float notice_radius = 13f;

    [SerializeField] private GameObject player;

    private bool inWater = true;

    private Vector3 target;

    private float timer = 3f;
    private float cooldown = 0f;

    void Start()
    {
        home_base = this.gameObject.transform.position;
        FindNewTarget();
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (cooldown <= 0f)
        {
            cooldown = 0f;
        }
        else
        {
            cooldown -= Time.deltaTime;
        }

        Vector3 cur_pos = this.gameObject.transform.position;
        Vector3 to_base = home_base - cur_pos;
        float distance_from_base = to_base.magnitude;

        Vector3 player_pos = player.transform.position;
        Vector3 to_player = player_pos - cur_pos;
        float distance_from_player = to_player.magnitude;

        if (state == 0)
        {
            // Roam
            Vector3 trajectory = target - cur_pos;
            if (inWater)
            {
                this.gameObject.transform.Translate(trajectory.normalized * roaming_speed * Time.deltaTime);
            }
            else
            {
                this.gameObject.transform.Translate(new Vector3(0f, -1f, 0f) * roaming_speed * Time.deltaTime);
                FindNewTarget();
            }

            if ((trajectory.magnitude <= 1f) || (timer < 0f))
            {
                FindNewTarget();
            }
            if ((distance_from_base > base_radius) || (!inWater))
            {
                state = 2;
            }
            else if ((distance_from_player <= notice_radius) && (cooldown <= 0f))
            {
                target = player_pos;
                state = 1;
            }
        }
        else if (state == 1)
        {
            // Charge
            timer = 3f;
            Vector3 trajectory = target - cur_pos;
            this.gameObject.transform.Translate(trajectory.normalized * charging_speed * Time.deltaTime);
            if ((trajectory.magnitude <= 1f) || !inWater)
            {
                state = 2;
                cooldown = 1.2f;
            }
        }
        else if (state == 2)
        {
            // Return
            this.gameObject.transform.Translate(to_base.normalized * roaming_speed * Time.deltaTime);
            if ((distance_from_base <= base_radius) && (inWater))
            {
                FindNewTarget();
                state = 0;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerState player = other.gameObject.GetComponent<PlayerState>();
            player.Hit();
        }
        if (other.gameObject.CompareTag("Water"))
        {
            inWater = true;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerState player = other.gameObject.GetComponent<PlayerState>();
            player.Hit();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Water"))
        {
            inWater = false;
        }
    }

    void FindNewTarget()
    {
        float random_0_to_2pi = Random.Range(0, 2*Mathf.PI);
        float sin = Mathf.Sin(random_0_to_2pi);
        float cos = Mathf.Cos(random_0_to_2pi);
        Vector3 new_target = home_base + new Vector3(cos * base_radius * 0.9f, sin * base_radius * 0.9f, 0f);
        target = new_target;
        timer = 3f;
    }
}
