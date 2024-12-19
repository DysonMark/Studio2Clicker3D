using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Firebase;
using Firebase.Extensions;
using Firebase.Storage;
using UnityEngine.Networking;
using System.Threading;
using System.Threading.Tasks;
using Application = UnityEngine.Application;

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

        [SerializeField] List<FileData> storageBucketFileMetaDeta = new List<FileData>();

        IEnumerator Start()
        {
            if (Application.internetReachability == NetworkReachability.NotReachable || simulateNoInternet)
            {
                Debug.LogError("No internet");
                yield break;
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileToCheck"></param>
        /// <returns></returns>
        IEnumerator GetFileMetaData(StorageReference fileToCheck)
        {
            Task<StorageMetadata> fileToCheckMetaData = fileToCheck.GetMetadataAsync();

            while (fileToCheckMetaData != null && !fileToCheckMetaData.IsCompleted)
            {
                Debug.Log("Getting file meta data " + fileToCheck.Name);
                yield return null;
            }

            StorageMetadata metadata = fileToCheckMetaData.Result;

            if (metadata != null)
            {
                FileData newFile = new FileData();

                newFile.fileName = metadata.Name;
                newFile.dateCreated = metadata.CreationTimeMillis;
                newFile.dateLastModified = metadata.UpdatedTimeMillis;
                newFile.dateCreatedString = metadata.CreationTimeMillis.ToUniversalTime().ToString();
                newFile.dateModifiedString = metadata.UpdatedTimeMillis.ToUniversalTime().ToString();
                newFile.fileSize = metadata.SizeBytes;
                //bool folderExists = File.Exists(destinationFolderPath);
                if (File.Exists(destinationFolderPath))
                    newFile.fileDestination = Path.Combine(destinationFolderPath, metadata.Name);
                storageBucketFileMetaDeta.Add(newFile);
            }
            yield return null;
        }
        IEnumerator DownloadFiles()
        {
            for (int i = 0; i < storageBucketFileMetaDeta.Count; i++)
            {
                // check to see if there's already a local file
                bool fileExists = File.Exists(storageBucketFileMetaDeta[i].fileDestination);

                if (fileExists)
                {
                    //check to see if it's up to date

                    if (!IsFileUpToDate(new FileInfo(storageBucketFileMetaDeta[i].fileDestination),
                            storageBucketFileMetaDeta[i]))
                    {
                        //if it's not, delete the local file
                        File.Delete(storageBucketFileMetaDeta[i].fileDestination);
                        Debug.Log("Deleting file" + storageBucketFileMetaDeta[i].fileName);
                        fileExists = false;
                    }
                    // if it is don't do anything
                }

                if (!fileExists)
                { 
                    //download the file and wait for it to happen
                    yield return DownloadFile(storageBucket.Child(storageBucketFileMetaDeta[i].fileName));
                }
            }
            yield return null;
        }

        private bool IsFileUpToDate(FileInfo localFile, FileData metaData)
        {
            bool isUpToDate = true;
            bool dateIsNewer = false;
            bool fileSizeIsDifferent = false;

            DateTime metaDataTimeUTC = metaData.dateLastModified.ToUniversalTime();
            DateTime localFileTimeUTC = localFile.LastWriteTime.ToUniversalTime();

            metaDataTimeUTC = new DateTime(metaDataTimeUTC.Year, metaDataTimeUTC.Month, metaDataTimeUTC.Day,
                metaDataTimeUTC.Hour, metaDataTimeUTC.Minute, metaDataTimeUTC.Second);
            localFileTimeUTC = new DateTime(localFileTimeUTC.Year, localFileTimeUTC.Month, localFileTimeUTC.Day,
                localFileTimeUTC.Hour, localFileTimeUTC.Minute, localFileTimeUTC.Second);

            dateIsNewer = DateTime.Compare(metaDataTimeUTC, localFileTimeUTC) > 0;
            
            fileSizeIsDifferent = localFile.Length != metaData.fileSize;

            isUpToDate = !(dateIsNewer || fileSizeIsDifferent);
            return isUpToDate;
        }

        IEnumerator DownloadFile(StorageReference fileToDownload)
        {
            Task<Uri> uri = fileToDownload.GetDownloadUrlAsync();

            while (!uri.IsCompleted)
            {
                Debug.Log("Getting URI Data " + fileToDownload.Name);
                yield return null;
            }
            // once we get to here we have the info on where it's located online

            UnityWebRequest www = new UnityWebRequest(uri.Result);
            www.downloadHandler = new DownloadHandlerBuffer();

            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Request successful");
                byte[] resultData = www.downloadHandler.data;

                while (www.downloadProgress < 1)
                {
                    Debug.Log("Downloading File " + fileToDownload.Name + (www.downloadProgress * 100) + "%");
                    yield return null;
                }

                string destinationPath = Path.Combine(destinationFolderPath, fileToDownload.Name);

                Task writeFile = File.WriteAllBytesAsync(destinationPath, resultData);

                while (!writeFile.IsCompleted)
                {
                    Debug.Log("Writing file data please" + fileToDownload.Name);
                    yield return null;
                }
                Debug.Log("Download completed!");
            }
            yield return null;  
        }
    }    
}
