using DG.Tweening.Plugins.Options;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

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
    [SerializeField] StaminaBarScript staminaBarScript;
    private void Update()
    {
        Swimming();

        stamina = staminaBarScript.GetStamina();
    }

    private void Swimming()
    {
        Vector3 mouseScreenPos = Input.mousePosition;

        if (Input.GetKey(KeyCode.Mouse0))
        {
            //Gets positions for movement

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

            isFastSwimming = Input.GetKey(KeyCode.Space) && stamina > 0;
            float speed = isFastSwimming ? fastSwimSpeed : baseSwimSpeed;

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

                //transform.position = Vector3.MoveTowards(currentPOS, mousePOS, speed * Time.deltaTime);
                rigidBody.linearVelocity = direction.normalized * speed;
            }
        }
        else
        {
            isFastSwimming = false;
            staminaBarScript.IncreaseStamina.Invoke(staminaRegenRate * Time.deltaTime); //Increases stamina by 5 when staying still
            rigidBody.linearVelocity = Vector3.Lerp(rigidBody.linearVelocity, Vector3.zero, 3 * Time.deltaTime); //Slows movement to 0;
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
            rigidBody.useGravity = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Water"))
        {
            inWater = false;
            rigidBody.useGravity = true;
        }
    }
}
