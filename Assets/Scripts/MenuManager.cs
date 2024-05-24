using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputField;

    private void Start()
    {
        LoadName();
        inputField.text = PersistentData.Instance.userName;
    }

    public void EnterName()
    {
        PersistentData.Instance.userName = inputField.text;
        Debug.Log(PersistentData.Instance.userName);
    }

    [System.Serializable]
    class SaveData
    {
        public string userName;
    }

    private void SaveName()
    {
        SaveData data = new SaveData();
        data.userName = PersistentData.Instance.userName;
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savename.json", json);
    }

    private void LoadName()
    {
        string path = Application.persistentDataPath + "/savename.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            PersistentData.Instance.userName = data.userName;
        }
    }

    public void StartGame()
    {
        SaveName();
        SceneManager.LoadScene("main");
    }

    public void ExitGame()
    {
        SaveName();
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
