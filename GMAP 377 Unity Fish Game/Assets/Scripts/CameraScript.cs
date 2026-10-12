using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] Transform Player;
    private float zAxis;
    private void Awake()
    {
        zAxis = transform.position.z;
    }

    private void Update()
    {
        UpdateCameraPosition();
    }

    private void UpdateCameraPosition()
    {
        Vector3 newPOS = Player.transform.position;
        newPOS.z = zAxis;
        
        transform.position = newPOS;
    }
}
