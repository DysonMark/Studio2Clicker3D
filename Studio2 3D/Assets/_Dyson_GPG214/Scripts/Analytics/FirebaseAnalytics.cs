using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FirebaseAnalytics : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Firebase.Analytics.FirebaseAnalytics.LogEvent(Application.platform.ToString());

        Firebase.Analytics.FirebaseAnalytics.LogEvent("Deaths", SceneManager.GetActiveScene().name, 10);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
