using DG.Tweening.Plugins.Options;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControllerScript : MonoBehaviour
{
    [SerializeField] Rigidbody rigidBody;

    [SerializeField] float baseSwimSpeed;
    [SerializeField] float fastSwimSpeed;
    private float stamina;
    [SerializeField] float staminaRegenRate = 5f;
    [SerializeField] float fastSwimStaminaDecreaseRate = 5f;
    private bool isFastSwimming = false;

    private bool isFacingRight = true;

    private bool inWater = true;
    private Vector3 Gravity;
    public float weight = 0f;
    [SerializeField] StaminaBarScript staminaBarScript;
    private void Update()
    {
        Swimming();
        Weight();
        stamina = staminaBarScript.GetStamina();
    }

    private void Swimming()
    {
        //Gets positions for movement

        Vector3 mouseScreenPos = Mouse.current.position.ReadValue();

        Vector3 currentPOS = transform.position;

        Vector3 mousePOS = mouseScreenPos;
        mousePOS.z = -Camera.main.transform.position.z;
        mousePOS = Camera.main.ScreenToWorldPoint(mousePOS);
        mousePOS.z = 0;

        //Look towards Cursor segment

        Vector3 direction = mousePOS - currentPOS;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if ((direction.x < 0 && isFacingRight) || (direction.x > 0 && !isFacingRight)) //Handles sprite flipping
        {
            FlipSprite();
        }

        transform.rotation = Quaternion.Euler(0, 0, angle - 90f); //Look towards cursor

        if (Mouse.current != null && Mouse.current.leftButton.isPressed && inWater)
        {
            isFastSwimming = Input.GetKey(KeyCode.Space) && stamina > 0;
            float speed = isFastSwimming ? Mathf.Clamp(fastSwimSpeed - weight, baseSwimSpeed + 2f, fastSwimSpeed): baseSwimSpeed;

            //Move towards Cursor
            if (Vector3.Distance(currentPOS, mousePOS) > 1f)
            {
                if (isFastSwimming)
                {
                    staminaBarScript.DecreaseStamina.Invoke(fastSwimStaminaDecreaseRate * Time.deltaTime);
                }
                else
                {
                    staminaBarScript.IncreaseStamina.Invoke(staminaRegenRate * Time.deltaTime);
                }

                rigidBody.linearVelocity = direction.normalized  * speed + Gravity;
            }
        }
        else
        {
            isFastSwimming = false;
            staminaBarScript.IncreaseStamina.Invoke(staminaRegenRate * Time.deltaTime); //Increases stamina by 5 when staying still
            rigidBody.linearVelocity = Vector3.Lerp(rigidBody.linearVelocity, Vector3.zero, 3 * Time.deltaTime); //Slows movement to 0;

            rigidBody.AddForce(Gravity, ForceMode.Acceleration);
        }
    }

    private void Weight()
    {
        float gravityPower = 0.5f;

        if (weight < 0)
        {
            weight = 0;
        }

        if (!inWater)
        {
            Gravity = Physics.gravity * gravityPower * 7;
        }
        else
        {
            Gravity = Physics.gravity * (weight/8);
        }
    }

    private void FlipSprite()
    {
        isFacingRight = !isFacingRight;
        Vector3 currentScale = transform.localScale;
        currentScale.x *= -1;
        transform.localScale = currentScale;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Water"))
        {
            inWater = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Water"))
        {
            inWater = false;
        }
    }
}
