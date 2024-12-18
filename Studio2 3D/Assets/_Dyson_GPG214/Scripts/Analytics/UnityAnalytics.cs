using System.Collections;
using System.Collections.Generic;
using Unity.Services.Analytics;
using Unity.Services.Core;
using UnityEditor;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;
public class UnityAnalytics : MonoBehaviour
{
    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return UnityServices.InitializeAsync();
        
        AnalyticsService.Instance.StartDataCollection();
        
        //AnalyticsService.Instance.StopDataCollection();
        
        //AnalyticsService

        CustomEvent onStartUpEvent = new CustomEvent("StartUpInformation")
        {
            { "Device Type", SystemInfo.deviceType },
            { "Device Platform", Application.platform.ToString() },
            { "Device Name", SystemInfo.deviceName }
        };
        
        //send my event to the service/Unity analytics dashboard.

        CustomEvent OnPlayerKills = new CustomEvent("playerKills")
        {
            { "playerKills", 1 },
            { "playerKillsPosition", transform.position.ToString() },
            { "levelName", SceneManager.GetActiveScene().name }
        };
        AnalyticsService.Instance.RecordEvent(GameEvents.onStartUpEvent);
        Debug.Log("On Start Event Called");
        yield return null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static class GameEvents
    {
        public static CustomEvent onStartUpEvent = new CustomEvent("StartUpInformation")
        {
            { "Device Type", SystemInfo.deviceType.ToString()},
            { "Device Platform", Application.platform.ToString() },
            { "Device Name", SystemInfo.deviceName.ToString()}
        };
    }
}
