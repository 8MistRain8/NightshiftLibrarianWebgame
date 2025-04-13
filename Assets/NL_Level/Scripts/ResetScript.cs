using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetScript : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        // Listen for the ESC key press
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ResetLevel();
        }
    }

    void ResetLevel()
    {
        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
