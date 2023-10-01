using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadOnActivation : MonoBehaviour
{

    void OnEnable()
    {
        switch (GameController.level)
        {
            case 1:
                SceneManager.LoadScene("Level01", LoadSceneMode.Single);
                break;
            case 2:
                SceneManager.LoadScene("Level02", LoadSceneMode.Single);
                break;
        }
    }
}
