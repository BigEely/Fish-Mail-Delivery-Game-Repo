using TMPro;
using UnityEngine;

public class NotificationScript : MonoBehaviour
{
    public static NotificationScript instance { get; private set; }
    [SerializeField] GameObject NotificationPrefab;

    private void Awake()
    {
        instance = this;
    }
    public void FishFlakesObtained(int amount)
    {
        GameObject Notification = Instantiate(NotificationPrefab, transform.position, Quaternion.identity);
        string text = $"+{amount}";
        Notification.GetComponentInChildren<TMP_Text>().text = text;
        Disappear(Notification);
    }

    public void Delivered(int amount)
    {
        GameObject Notification = Instantiate(NotificationPrefab, transform.position, Quaternion.identity);
        string text = $"Delivered\n+{amount}";
        Notification.GetComponentInChildren<TMP_Text>().text = text;
        Disappear(Notification);
    }

    private void Disappear(GameObject Notificaton)
    {
        Destroy(Notificaton, 1f);
    }
}
