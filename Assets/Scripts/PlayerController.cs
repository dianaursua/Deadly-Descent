using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Required for Button reference

[RequireComponent(typeof(Rigidbody), typeof(BoxCollider))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private FixedJoystick _joystick;
    [SerializeField] private Animator _animator;

    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _runSpeed; // Speed when running
    [SerializeField] private Button _runButton; // Button to trigger running

    private bool _isRunning = false;

    private void Start()
    {
        // Add listener for when the run button is pressed and released
        _runButton.onClick.AddListener(OnRunButtonPressed);
        _runButton.onClick.AddListener(OnRunButtonReleased); // Adjust this for your specific use-case
    }

    private void OnRunButtonPressed()
    {
        Debug.Log("The Player is Running");
        _isRunning = true; // Set running state to true when the button is pressed
    }

    private void OnRunButtonReleased()
    {
        _isRunning = false; // Set running state to false when the button is released
    }

    private void FixedUpdate()
    {
        float currentSpeed = _isRunning ? _runSpeed : _moveSpeed; // Use run speed if button is pressed

        // Move the player using joystick input
        _rigidbody.velocity = new Vector3(_joystick.Horizontal * currentSpeed, _rigidbody.velocity.y, _joystick.Vertical * currentSpeed);

        // Check if the player is moving
        if (_joystick.Horizontal != 0 || _joystick.Vertical != 0)
        {
            
            // Rotate player towards movement direction
            transform.rotation = Quaternion.LookRotation(_rigidbody.velocity);
            _animator.SetBool("isRunning", true);
        }
        else
        {
            _animator.SetBool("isRunning", false);
        }
    }
}
