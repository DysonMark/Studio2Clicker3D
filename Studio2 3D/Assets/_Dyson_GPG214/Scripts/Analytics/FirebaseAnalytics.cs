using UnityEngine;
using UnityEngine.SceneManagement;

namespace SAE.GPG214.Dyson.Analytics
{

    public class FirebaseAnalytics : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            Firebase.Analytics.FirebaseAnalytics.LogEvent(Application.platform.ToString());

            Firebase.Analytics.FirebaseAnalytics.LogEvent("Deaths", SceneManager.GetActiveScene().name, 10);
        }
    }
}
