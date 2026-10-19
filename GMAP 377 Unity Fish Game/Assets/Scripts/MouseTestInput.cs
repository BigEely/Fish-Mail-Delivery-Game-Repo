using UnityEngine;
using UnityEngine.InputSystem;

public class MouseTestInput : MonoBehaviour
{
    void Update()
    {
        if (Mouse.current == null)
        {
            Debug.LogWarning("New Input System: no mouse detected");
            return;
        }

        Debug.Log(
            $"Position: {Mouse.current.position.ReadValue()} | " +
            $"Left held: {Mouse.current.leftButton.isPressed}"
        );
    }
}