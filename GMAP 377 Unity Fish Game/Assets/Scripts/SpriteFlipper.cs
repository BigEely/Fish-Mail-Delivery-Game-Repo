using UnityEngine;

public class SpriteFlipper: MonoBehaviour
{
    private SpriteRenderer sprite_renderer;

    void Awake()
    {
        sprite_renderer = this.gameObject.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Get the current Z rotation in degrees
        float zRotation = transform.eulerAngles.z;

        // Normalize to range [-180, 180] for easier comparison
        if (zRotation > 180f)
        {
            zRotation -= 360f;
        }

        // Flip vertically if upside down
        sprite_renderer.flipY = (zRotation > 90 || zRotation < -90);
    }
}
