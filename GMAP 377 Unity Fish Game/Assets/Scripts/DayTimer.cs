using TMPro;
using UnityEngine;

public class DayTimer : MonoBehaviour
{
    private TMP_Text text_mesh_pro;
    private float timer = 10f;
    private bool timer_ended = false;
    void Start()
    {
        text_mesh_pro = this.gameObject.GetComponent<TMP_Text>();
    }

    void Update()
    {
        if (!timer_ended)
        {
            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                timer = 0f;
                timer_ended = true;
            }
            text_mesh_pro.text = Mathf.Floor(timer).ToString();
        }
    }
}
