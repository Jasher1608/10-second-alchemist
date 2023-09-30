using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadOnActivation : MonoBehaviour
{
    void OnEnable()
    {
        SceneManager.LoadScene("Main", LoadSceneMode.Single);
    }
}
