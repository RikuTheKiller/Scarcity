using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scarcity
{
    public class GameMenu : MonoBehaviour
    {

        // Start is called once before the first execution of Update after the MonoBehaviour is created
       

        // Update is called once per frame
        void Update()
        {
        
        }

       public void PlayGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); 
        }

       public void QuitGame()
        {
            Debug.Log("GET OUT");
            Application.Quit();

        }
    }
}
