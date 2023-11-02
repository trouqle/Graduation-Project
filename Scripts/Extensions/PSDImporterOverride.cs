#if UNITY_EDITOR
using UnityEditor.AssetImporters;
using UnityEngine;


namespace UnityEditor.U2D.PSD
{
    [ScriptedImporter(1, new string[0], new[] { "psd" })]
    internal class PSDImporterOverride : PSDImporter
    {
    }
}
#endif
