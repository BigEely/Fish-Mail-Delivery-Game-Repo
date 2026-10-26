using UnityEngine;

public class FishFlakeScript : MonoBehaviour
{
    [SerializeField] int FishFlakeValue = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GameManager.instance.fishFlakesCollected += FishFlakeValue;
            Destroy(gameObject);
        }
    }
}
