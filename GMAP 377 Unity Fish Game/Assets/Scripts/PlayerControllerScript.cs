using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class PlayerControllerScript : MonoBehaviour
{
    [SerializeField] float baseSwimSpeed;
    [SerializeField] float fastSwimSpeed;
    private float stamina;
    [SerializeField] float staminaRegenRate = 5f;
    [SerializeField] float fastSwimStaminaDecreaseRate = 5f;

    [SerializeField] float shakeDistance = 10f; //Distance (in pixels) to trigger fast swimming
    [SerializeField] float shakeWindow = .5f; //Distance over time (in seconds) to track for fast swimming
    [SerializeField] int shakesRequired = 3;

    private float shakeWindowExtender = 0;
    private float fastSwimTimer = 0f;
    private Vector3 lastMousePosition;
    private float shakeTimer;
    private int shakeCount;
    private int lastDirectionX;
    private int lastDirectionY;
    private bool isFastSwimming = false;


    private bool isFacingRight = true;

    private bool inWater = true;
    [SerializeField] StaminaBarScript staminaBarScript;
    private void Update()
    {
        if (inWater)
        {
            Swimming();
        }
        else
        {
            OutOfWater();
        }

            stamina = staminaBarScript.GetStamina();

        if (isFastSwimming)
        {
            fastSwimTimer -= Time.deltaTime;
            Debug.Log(fastSwimTimer);

            if (fastSwimTimer <= 0f)
            {
                isFastSwimming = false;
                shakeWindowExtender = 0f;
            }
        }

    }

    private void Swimming()
    {
        Vector3 mouseScreenPos = Input.mousePosition;

        if (Input.GetKey(KeyCode.Mouse0))
        {
            //Gets positions for movement
            CalculateFastSwimming(mouseScreenPos);

            Vector3 currentPOS = transform.position;

            Vector3 mousePOS = mouseScreenPos;
            mousePOS.z = -Camera.main.transform.position.z;
            mousePOS = Camera.main.ScreenToWorldPoint(mousePOS);
            mousePOS.z = 0;

            //Look towards Cursor segment

            Vector3 direction = mousePOS - currentPOS;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            if (direction.x < 0 && isFacingRight == true)
            {
                FlipSprite();
            }
            else if (direction.x > 0 && isFacingRight == false)
            {
                FlipSprite();
            }
            transform.rotation = Quaternion.Euler(0, 0, angle - 90f); //Look towards cursor


            //Move towards Cursor
            if (Vector3.Distance(currentPOS, mousePOS) > 1f)
            {
                float speed = isFastSwimming ? fastSwimSpeed : baseSwimSpeed;

                if (speed == fastSwimSpeed)
                {
                    staminaBarScript.DecreaseStamina.Invoke(fastSwimStaminaDecreaseRate * Time.deltaTime);
                    if (stamina <= 0)
                    {
                        speed = baseSwimSpeed;
                    }
                }
                else
                {
                    staminaBarScript.IncreaseStamina.Invoke(staminaRegenRate * Time.deltaTime);
                }

                transform.position = Vector3.MoveTowards(currentPOS, mousePOS, speed * Time.deltaTime);
            }
        }
        else
        {
            shakeTimer = 0f;
            shakeCount = 0;
            lastDirectionX = 0;
            lastDirectionY = 0;
            isFastSwimming = false;
            staminaBarScript.IncreaseStamina.Invoke(staminaRegenRate * Time.deltaTime); //Increases stamina by 5 when staying still
        }
        lastMousePosition = mouseScreenPos;
    }

    private void FlipSprite()
    {
        isFacingRight = !isFacingRight;
        Vector3 currentScale = transform.localScale;
        currentScale.x *= -1;
        transform.localScale = currentScale;
    }

    private bool CalculateFastSwimming(Vector3 mousePOS)
    {
        shakeTimer += Time.deltaTime;
        

        Vector3 mouseDelta = mousePOS - lastMousePosition;

        int directionX = 0;
        int directionY = 0;

        if (Mathf.Abs(mouseDelta.x) >= shakeDistance)
            directionX = mouseDelta.x > 0 ? 1 : -1;

        if (Mathf.Abs(mouseDelta.y) >= shakeDistance)
            directionY = mouseDelta.y > 0 ? 1 : -1;

        // Count horizontal direction reversals
        if (directionX != 0)
        {
            if (lastDirectionX != 0 && directionX != lastDirectionX)
            {
                shakeCount++;
                shakeWindowExtender += .2f; //Increases fast swim time by x each shake
            }
            lastDirectionX = directionX;
        }

        // Count vertical direction reversals
        if (directionY != 0)
        {
            if (lastDirectionY != 0 && directionY != lastDirectionY)
            {
                shakeCount++;
                shakeWindowExtender += .2f; //Increases fast swim time by x each shake
            }
            lastDirectionY = directionY;
        }

        // Activate fast swimming when enough reversals occur
        if (shakeCount >= shakesRequired)
        {
            isFastSwimming = true;
            fastSwimTimer = shakeWindow + shakeWindowExtender;
            if (fastSwimTimer > 1f) //Hard Cap on extended swim time
            {
                fastSwimTimer = 1f;
            }

            shakeCount = 0;
            shakeTimer = 0f;
            lastDirectionX = 0;
            lastDirectionY = 0;
            shakeWindowExtender = 0f;
        }

        // Reset if the time window expires
        if (shakeTimer >= shakeWindow)
        {
            shakeWindowExtender = 0f;
            shakeTimer = 0f;
            shakeCount = 0;
            lastDirectionX = 0;
            lastDirectionY = 0;
        }

        return isFastSwimming;
    }

    private void OutOfWater()
    {
        float gravity = 9.81f;
        transform.position += Vector3.down * gravity * Time.deltaTime;
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
            shakeTimer = 0f;
            shakeCount = 0;
            lastDirectionX = 0;
            lastDirectionY = 0;
            isFastSwimming = false;
        }
    }
}
