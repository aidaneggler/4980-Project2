using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    [Header("Next Level")]
    public string nextSceneName = "Level_2";

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CharacterController>() == null)
            return;

        SceneManager.LoadScene(nextSceneName);
    }
}