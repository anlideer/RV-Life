using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenStory : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        MyDialogManager.Show(new List<string>{
            "Tired of everyday's work, you decided to travel now!",
            "With this money, go to Hongkong and Mohe.",
            "(Hongkong is somewhere south near the sea, and Mohe is the northest.)",
            "Click the button to start your journey."
        });

    }

    public void GotoMap()
    {
        SceneManager.LoadScene("Map");
    }

}
