using System;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;
using UnityEngine.UIElements;

public class MultiUI : MonoBehaviour
{
    [SerializeField] private UIDocument uiDocument;
    private Button disconnectButton;
    private TextField ipTextField;

    private void Start()
    {
        Button hostbutton = uiDocument.rootVisualElement.Q<Button>("HostButton");
        Button clientbutton = uiDocument.rootVisualElement.Q<Button>("ClientButton");
        Button serverbutton = uiDocument.rootVisualElement.Q<Button>("ServerButton");
        disconnectButton = uiDocument.rootVisualElement.Q<Button>("DisconnectButton");
        ipTextField = uiDocument.rootVisualElement.Q<TextField>("IpTextField");
        
        hostbutton.clicked += OnHostClicked;
        serverbutton.clicked += OnServerClicked;
        clientbutton.clicked += OnClientCLicked;
        disconnectButton.clicked += OnDisconnectClicked;
        
        disconnectButton.SetEnabled(false);
    }

    void OnHostClicked()
    {
        ipTextField.focusable = false;
        NetworkManager.Singleton.GetComponent<UnityTransport>().SetConnectionData(ipTextField.value, 7777);
        NetworkManager.Singleton.StartHost();
        disconnectButton.SetEnabled(true);
    }

    void OnServerClicked()
    {
        ipTextField.focusable = false;
        NetworkManager.Singleton.StartServer();
        disconnectButton.SetEnabled(true);
    }

    void OnClientCLicked()
    {
        ipTextField.focusable = false;
        NetworkManager.Singleton.StartClient();
        disconnectButton.SetEnabled(true);
    }

    void OnDisconnectClicked()
    {
        ipTextField.focusable = true;
        NetworkManager.Singleton.Shutdown();
    }
}
