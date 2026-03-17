using System;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class PlayerController : NetworkBehaviour
{
    [SerializeField] private CharacterController characterController;
    [SerializeField] private float speed = 1f;
    
    private Vector3 _movement;

    private Material _playerMaterial;

    NetworkVariable<Color> characterColor = new NetworkVariable<Color>(new Color(255,0,0));

    private void Awake()
    {
        _playerMaterial = GetComponent<Renderer>().material;
    }

    private void Start()
    {
        //_movement = new Vector3(1, 0, 1);

        
    }

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        characterColor.OnValueChanged += OnColorChange;
        if (IsServer)
        {
            characterColor.Value = RandomColor();
        }
        _playerMaterial.color = characterColor.Value;
        /*
        if (IsOwner)
        {
            ColorServerRPC();
        }
        */
    }

    void OnColorChange(Color oldColor, Color newColor)
    {
        print("Old " + oldColor + " New " + newColor);
        ColorClientRPC(newColor);
    }

    [ClientRpc]
    void ColorClientRPC(Color newColor)
    {
        if (IsOwner)
        {
            _playerMaterial.color = newColor;
        }
    }

    [ServerRpc]
    void ColorServerRPC()
    {
        if (IsServer)
        {
            characterColor.Value = RandomColor();
        }
        //ColorClientRPC();
    }

    private void Update()
    {
        if (!IsOwner) { return; }
        characterController.Move(new Vector3(_movement.x, 0f, _movement.y) * (Time.deltaTime * speed));
    }

    void OnMovements(InputValue value)
    {
        _movement = value.Get<Vector2>();
        if (IsOwner)
        {
            ColorServerRPC();
        }
    }
    

    Color RandomColor()
    {
        float r = Random.Range(0.0f, 1.0f); 
        float g = Random.Range(0.0f, 1.0f); 
        float b = Random.Range(0.0f, 1.0f);
        return new Color(r,g,b);
    }
}
