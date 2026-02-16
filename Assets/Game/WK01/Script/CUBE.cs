using UnityEngine;
using UnityEngine.InputSystem;

public class CUBE : MonoBehaviour
{
    
    private Vector2 moveInput;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private void Update()
    {
        Move();
    }
    
    private void OnMove(InputValue inputValue)
    {
        moveInput = inputValue.Get<Vector2>();
        
        Debug.Log($"Input détecté : {moveInput}");
    }

    private void Move()
    {
        Vector3 movement = new Vector3(moveInput.x, 0, moveInput.y);
        transform.Translate(movement * Time.deltaTime * 5f);
    }
    
    private void OnJump(InputValue inputValue)
    {
        if (inputValue.isPressed)
        {
            print("test jump");
        }
        else
        {
            print("i'm not jumping");
        }
        
    }
    
}
