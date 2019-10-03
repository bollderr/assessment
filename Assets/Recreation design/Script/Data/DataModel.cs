using UnityEngine;
using System.Collections;
using System.IO;

public static class DataModel  {
    public static void SaveData() {
        string content = GameManager.instance.highScore +"\n"+GameManager.instance.numbersGame+ "\n" + GameManager.instance.Sound;
        File.WriteAllText(Tools.dataPath, content);
    }
    public static void LoadData() {
        if (File.Exists(Tools.dataPath))
        {
            string content = File.ReadAllText(Tools.dataPath);
            GameManager.instance.highScore = int.Parse(content.Split('\n')[0]);
            GameManager.instance.numbersGame = int.Parse(content.Split('\n')[1]);
            GameManager.instance.Sound = content.Split('\n')[2]=="true"?true:false;
        }
        else
        {
            GameManager.instance.highScore = 0;
            GameManager.instance.numbersGame = 0;
            GameManager.instance.Sound = false;
        }
        
    }
}
public class Tools
{
    /// <summary>
    /// Get the full path to the data storage file
    /// </summary>
    public static readonly string dataPath = Application.persistentDataPath + @"\data.bin";
}