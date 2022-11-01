using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class NetworkMangerUI : MonoBehaviour
{
    [SerializeField] private Button hostButton;
    [SerializeField] private Button clientButton;

    private void Awake()
    {
        hostButton.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartHost();
            //OnLaunch();
        });

        clientButton.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartClient();
            //OnLaunch();
        });
    }

    public void OnLaunch()
    {
        NetworkManager.Singleton.SceneManager.LoadScene("test", UnityEngine.SceneManagement.LoadSceneMode.Single);
    }
}
