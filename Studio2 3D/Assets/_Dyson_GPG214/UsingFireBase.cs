using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Extensions;
using Firebase.Storage;

namespace SAE.GPG214.Dyson.Storage
{
    public class UsingFireBase : MonoBehaviour
    {
        private FirebaseStorage storage;
        // Start is called before the first frame update
        void Start()
        {
            
            storage = FirebaseStorage.GetInstance("gs://gpg214-dyson.firebasestorage.app");

            var audioReference = storage.GetReference("Shadowfell Combat Dark Combat Music.mp3");
            audioReference.GetDownloadUrlAsync().ContinueWithOnMainThread(task =>
            {
                var path = task.Result.ToString();
                StartCoroutine(DownloadAudio(path));
            });
            // var audio = UnityWebRequestMultimedia.GetAudioClip(audioReference.);
        }

        private string DownloadAudio(string path)
        {
            Debug.Log("path:" + path);
            return path;
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }    
}
