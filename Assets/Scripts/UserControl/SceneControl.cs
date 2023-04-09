using UnityEngine;
using UnityEngine.SceneManagement;
namespace UserControl
{
    public class SceneControl: MonoBehaviour
    {
        // public void Update()
        // {
        //     if (Input.GetKey(KeyCode.R))
        //         ResetScene();
        // }
        
        public void ResetScene()
        {
            int sceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(sceneIndex, LoadSceneMode.Single);
        }
    }
}