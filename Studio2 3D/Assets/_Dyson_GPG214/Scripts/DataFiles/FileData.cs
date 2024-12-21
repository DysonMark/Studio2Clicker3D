using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SAE.GPG214.Dyson.Data
{
    [System.Serializable]
    public struct FileData
    {
        public string fileName;
        public string fileDestination;
        public long fileSize;
        public DateTime dateLastModified;
        public DateTime dateCreated;
        public string dateModifiedString;
        public string dateCreatedString;

    }
}
