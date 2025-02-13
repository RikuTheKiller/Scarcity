using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace Scarcity
{
    public class CutsceneManager : MonoBehaviour
    {
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.Enter)
            {
                SceneManager.LoadScene("Plains");
            }
        }
    }
}
