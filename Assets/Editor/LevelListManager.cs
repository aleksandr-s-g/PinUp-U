using System.IO;
using System.Text;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEditor.Callbacks;



class PreBuildFileNamesSaver : IPreprocessBuildWithReport
{
    public int callbackOrder { get { return 0; } }
    public void OnPreprocessBuild(UnityEditor.Build.Reporting.BuildReport report)
    {
        DirectoryInfo dir = new DirectoryInfo("Assets/Levels/Journey");
        FileInfo[] fileList = dir.GetFiles("*.json");

        File.WriteAllText("Assets/Resources/FileNames.txt", "[journey_levels_list]\n");
        for (int i = 0 ; i<fileList.Length;i++)
        {
            FileInfo targetLevel = fileList[i];
            File.AppendAllText("Assets/Resources/FileNames.txt", "Levels/Journey"+ "/"+ targetLevel.Name.Split(".")[0]+"\n");
        }
        File.AppendAllText("Assets/Resources/FileNames.txt", "\n");
        AssetDatabase.Refresh();
    }
}