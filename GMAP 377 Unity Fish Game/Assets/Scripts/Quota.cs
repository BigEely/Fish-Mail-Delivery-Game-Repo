using UnityEngine;

public class Quota: MonoBehaviour
{
    private bool quota_done = false;

    public int[] goal = new int[3];
    private int[] completed = new int[3];

    public int packagesDelivered = 0;

    void Start()
    {
        goal[0] = 1;
        goal[1] = 0;
        goal[2] = 1;

        completed[0] = 0;
        completed[1] = 0;
        completed[2] = 0;
    }

    void Update()
    {
        if (!quota_done)
        {
            if ((completed[0] >= goal[0]) && (completed[1] >= goal[1]) && (completed[2] >= goal[2]))
            {
                quota_done = true;
            }
        }
    }

    public void CompleteDelivery(int delivery_type)
    {
        completed[delivery_type] += 1;
        packagesDelivered++;
    }

    public void ResetQuota()
    {
        completed[0] = 0;
        completed[1] = 0;
        completed[2] = 0;
        packagesDelivered = 0;
    }

    public bool GetQuotaDone()
    {
        return quota_done;
    }
}
