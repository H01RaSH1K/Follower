using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _walkSpeed = 5f;
    [SerializeField] private float _groundStickVelocity = -2f;
    [SerializeField] private float _gravity = -10f;

    private CharacterController _characterController;
    private PlayerInputReader _inputReader;

    private float _verticalVelocity;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _inputReader = new PlayerInputReader();
    }

    private void FixedUpdate()
    {
        if (_characterController.isGrounded)
            _verticalVelocity = _groundStickVelocity;
        else
            _verticalVelocity += _gravity * Time.fixedDeltaTime;

        Vector3 walkInput = new Vector3(_inputReader.Move.x, 0, _inputReader.Move.y);
        Vector3 velocity = walkInput * _walkSpeed;
        velocity.y = _verticalVelocity;

        _characterController.Move(velocity * Time.fixedDeltaTime);
    }

    private void OnDestroy()
    {
        _inputReader.Dispose();
    }
}
