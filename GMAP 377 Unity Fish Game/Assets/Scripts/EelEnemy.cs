using UnityEngine;

public class EelEnemy: MonoBehaviour
{
    private Vector3 home_base;

    void Start()
    {
        home_base = transform.position;
    }

    void Update()
    {
        transform.Translate(new Vector3(-8f, 0f, 0f) * Time.deltaTime);
        if (transform.position.x <= -100)
        {
            transform.position = new Vector3(100f, home_base.y, home_base.z);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerState player = other.gameObject.GetComponent<PlayerState>();
            player.Hit();
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
}
