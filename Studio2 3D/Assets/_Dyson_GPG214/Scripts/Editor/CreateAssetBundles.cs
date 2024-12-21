using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using Directory = UnityEngine.Windows.Directory;

namespace SAE.GPG214.Dyson.AssetBundles
{
    public class CreateAssetBundles
    {
        [MenuItem("Assets/Build Assets Bundles")]

        static void BuildAllAssetBundles()
        {
            string assetBundleDirectory = Path.Combine(Application.streamingAssetsPath, "AssetBundles");

            if (!Directory.Exists(assetBundleDirectory))
            {
                Directory.CreateDirectory(assetBundleDirectory);
            }

            BuildPipeline.BuildAssetBundles(assetBundleDirectory, BuildAssetBundleOptions.None,
                BuildTarget.StandaloneWindows);
        }

    }
}
