using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Firebase;
using Firebase.Extensions;
using Firebase.Storage;
using UnityEngine.Networking;

namespace SAE.GPG214.Dyson.Storage
{
    public class UsingFireBase : MonoBehaviour
    {
        /*
        private FirebaseStorage storage;

        public AudioSource source;
        // Start is called before the first frame update
        void Start()
        {
            
            Debug.Log("okok");
            storage = FirebaseStorage.GetInstance("gs://gpg214-dyson.firebasestorage.app");

            var audioReference = storage.GetReference("Shadowfell Combat Dark Combat Music.mp3");
            audioReference.GetDownloadUrlAsync().ContinueWithOnMainThread(task =>
            {
                Debug.Log("hello hello");
                var path = task.Result.ToString();
                //StartCoroutine(DownloadAudio(path));
                DownloadAudio(path);
            });
            // var audio = UnityWebRequestMultimedia.GetAudioClip(audioReference.);
        }

        private IEnumerator DownloadAudio(string path)
        {
            UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(path, AudioType.OGGVORBIS);

            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log(www.error);
            }
            else
            {
                AudioClip myClip = DownloadHandlerAudioClip.GetContent(www);
                source.PlayOneShot(myClip);
            }
            Debug.Log("path:" + path);
        }

        // Update is called once per frame
        void Update()
        {
        
        } */
        [SerializeField] private List<string> allFilesInBucket = new List<string>();
        [SerializeField] private string destinationFolderPath = Application.streamingAssetsPath;

        private FirebaseStorage storageLocation;
        private StorageReference storageBucket;
        private string storageBucketURL = "gs://gpg214-dyson.firebasestorage.app";

        [SerializeField] bool simulateNoInternet;

        List<FileData> storageBucketFileMetaDeta = new List<FileData>();

        IEnumerator Start()
        {
            if (Application.internetReachability == NetworkReachability.NotReachable || simulateNoInternet)
            {
                Debug.LogError("No internet");
            }

            GetFirebaseInstance();
            
            GetFirebaseStorageReference();

            if (storageBucket != null)
            {
                //grab all the files in bucket
                
                yield return StartCoroutine(GetAllFilesInBucket());
            }

            if (storageBucketFileMetaDeta.Count > 0)
            {
                yield return StartCoroutine(DownloadFiles());
            }
            
            yield return null;
        }

        void GetFirebaseInstance()
        {
            storageLocation = FirebaseStorage.DefaultInstance;

            if (storageLocation == null)
            {
                Debug.Log("Storage location not found");
            }
        }
        
        void GetFirebaseStorageReference()
        {
            if (storageLocation == null)
            {
                return;
            }
            storageBucket = storageLocation.GetReferenceFromUrl(storageBucketURL);
            
            if (storageBucket == null)
            {
                Debug.Log("Storage bucket not found");
            }
        }

        IEnumerator DownloadFiles()
        {
            yield return null;
        }

        private bool IsFileUpToDate(FileInfo localFile, FileData metaData)
        {
            return true;
        }

        IEnumerator GetAllFilesInBucket()
        {
            for (int i = 0; i < allFilesInBucket.Count; i++)
            {
                StorageReference fileData = storageBucket.Child(allFilesInBucket[i]);

                if (fileData == null)
                {
                    Debug.Log("File not found");
                    continue;
                }
                Debug.Log("File found: " + fileData.Name);

                yield return StartCoroutine(GetFileMetaData(fileData));
            }
            yield return null;
        }

        IEnumerator GetFileMetaData(StorageReference fileToCheck)
        {
            yield return null;
        }

        IEnumerator DownloadFile(StorageReference fileToDownload)
        {
            yield return null;  
        }
    }    
}
