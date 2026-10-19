using UnityEngine;

public class PostOffice: MonoBehaviour
{
    [SerializeField] private GameObject box0;
    [SerializeField] private GameObject box1;
    [SerializeField] private GameObject box2;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            box0.SetActive(true);
            box1.SetActive(true);
            box2.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            box0.SetActive(false);
            box1.SetActive(false);
            box2.SetActive(false);
        }
    }
}
