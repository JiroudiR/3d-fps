using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class handles the movement of the player with given input from the input manager
/// </summary>
public class PlayerController : MonoBehaviour
{
    [Header("Settings")]
    [Tooltip("The speed at which the player moves")]
    public float moveSpeed = 2f;
    [Tooltip("The speed at which the player rotates to look left and right (calculated in degrees)")]
    public float lookSpeed = 60f;
    [Tooltip("The power with which the player jumps")]
    public float jumpPower = 8f;
    [Tooltip("The strength of gravity")]
    public float gravity = 9.81f;
    [Header("Required References")]
    [Tooltip("The player shooter script that fires projectiles")]
    public Shooter playerShooter;

    // The character controller component on the player
    private CharacterController controller;
    private InputManager inputManager;

    /// <summary>
    /// Description:
    /// Standard Unity function called once before the first Update call
    /// Input:
    /// none
    /// Return:
    /// void (no return)
    /// </summary>
    void Start()
    {
        SetUpCharacterController();
        SetUpInputManager();
    }

    private void SetUpCharacterController()
    {
        controller = GetComponent<CharacterController>();
        if (controller == null)
        {
            Debug.LogError("The player controller script does not have a character controller on the same game object!");
        }
    }

    void SetUpInputManager()
    {
        inputManager = InputManager.instance;
    }

    /// <summary>
    /// Description:
    /// Standard Unity function called once every frame
    /// Input:
    /// none
    /// Return:
    /// void (no return)
    /// </summary>
    void Update()
    {
        ProcessMovement();
    }

    void ProcessMovement()
    {
        // Get the input from the input manager
        float leftRightInput = inputManager.horizontalMoveAxis;
        Debug.Log("The left right input value is: " + leftRightInput);
    }
}
