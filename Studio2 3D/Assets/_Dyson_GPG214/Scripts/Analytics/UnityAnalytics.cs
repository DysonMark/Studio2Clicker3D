using System.Collections;
using System.Collections.Generic;
using Unity.Services.Analytics;
using Unity.Services.Core;
using UnityEditor;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;

namespace SAE.GPG214.Dyson.Analytics
{
    public class UnityAnalytics : MonoBehaviour
    {
        // Start is called before the first frame update
        IEnumerator Start()
        {
            yield return UnityServices.InitializeAsync();

            AnalyticsService.Instance.StartDataCollection();

            //send my event to the service/Unity analytics dashboard.

            CustomEvent OnPlayerKills = new CustomEvent("playerKills")
            {
                { "playerKills", 1 },
                { "playerKillsPosition", transform.position.ToString() },
                { "levelName", SceneManager.GetActiveScene().name }
            };

            CustomEvent onStartUpEvent = new CustomEvent("StartUpInformation")
            {
                { "deviceType", Application.platform.ToString() },
                { "deviceBrand", Application.platform.ToString() },
                { "deviceName", Application.platform.ToString() }
            };
            yield return null;
        }
    }
}
