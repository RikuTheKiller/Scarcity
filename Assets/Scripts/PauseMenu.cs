using UnityEngine;

namespace Scarcity
{
    public class PauseMenu : MonoBehaviour
    {
        public GameObject PauseMenuObject;
       

       public void PauseThing()
        {
            PauseMenuObject.SetActive(true);
            Time.timeScale = 0;
        }
        public void ContinueThing()
        {
            PauseMenuObject.SetActive(false);
            Time.timeScale = 1;
        }
      

    }
}
