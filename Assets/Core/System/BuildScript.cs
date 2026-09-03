using UnityEngine;
using UnityEditor;
using System.IO;

public class BuildScript: EditorWindow
{
    static string PEth;

    [MenuItem("Build/BuildWIndow ")]
    static void Init()
    {
        BuildScript window = CreateInstance<BuildScript>();
        window.position = new Rect(Screen.width / 2, Screen.height / 2, 250, 150);
        window.ShowUtility();
    }
    void OnGUI()
    {
        GUILayout.Space(10);
        EditorGUILayout.LabelField("Build your game here, without the build window for some strange reason.", EditorStyles.wordWrappedLabel);
        GUILayout.Space(10);
        PEth = EditorGUILayout.TextField("Path To Build", EditorStyles.textField);
        GUILayout.Space(60);
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Build for windows."))
        {
            BuildPipeline.BuildPlayer(EditorBuildSettings.scenes, PEth, BuildTarget.StandaloneWindows64, BuildOptions.None);
            var window = GetWindow<BuildScript>();
            window.Close();
        } 
        else if (GUILayout.Button("Build for android"))
        {
            BuildPipeline.BuildPlayer(EditorBuildSettings.scenes, PEth, BuildTarget.Android, BuildOptions.None);
            var window = GetWindow<BuildScript>();
            window.Close();
        } else if (GUILayout.Button("Build for WEBGL"))
        {
            BuildPipeline.BuildPlayer(EditorBuildSettings.scenes, PEth, BuildTarget.WebGL, BuildOptions.None);
            var window = GetWindow<BuildScript>();
            window.Close();
        }
        else if (GUILayout.Button("View Other Build Types"))
        {
            var buildwindow = GetWindow<BuildPlayerWindow>();
            buildwindow.Show();
            var window = GetWindow<BuildScript>();
            window.Close();
        }
        EditorGUILayout.EndHorizontal();
    }
}