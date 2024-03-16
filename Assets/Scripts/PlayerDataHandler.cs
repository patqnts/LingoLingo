using UnityEngine;
using System.IO;

public class PlayerDataHandler : MonoBehaviour
{
    public static PlayerDataHandler instance;
    private GameObject player;
    public int characterModelIndex;
    public int coins;

    public Module[] modules;
    private string dataFilePath;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            SaveModuleChallengesToJson();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            LoadModuleChallengesFromJson();
        }
    }

    private void Awake()
    {
        instance = this;
        dataFilePath = Path.Combine(Application.persistentDataPath, "player_data.json");
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.LogError("Player GameObject not found in the scene.");
        }

        LoadModuleChallengesFromJson();
    }

    public void SetModuleValue(int moduleIndex, int challengeIndex, int challengeScore)
    {
        int currentScore = modules[moduleIndex].challenges[challengeIndex];

        if (challengeScore >= 0 || challengeScore > currentScore)
        {
            modules[moduleIndex].challenges[challengeIndex] = challengeScore;
        }
        else
        {
            Debug.LogError("Invalid challenge index for Module 1.");
        }

        SaveModuleChallengesToJson();
    }

    // Save all module_challenge arrays to a JSON file in the persistent data path
    public void SaveModuleChallengesToJson()
    {
        PlayerData data = new PlayerData
        {
            characterModelIndex = this.coins,
            coins = this.coins,
            modules = this.modules
        };

        string jsonData = JsonUtility.ToJson(data);
        File.WriteAllText(dataFilePath, jsonData);
        Debug.Log("Module challenges saved to: " + dataFilePath);
    }

    // Load module_challenge arrays from a JSON file in the persistent data path
    public void LoadModuleChallengesFromJson()
    {
        if (File.Exists(dataFilePath))
        {
            string jsonData = File.ReadAllText(dataFilePath);
            PlayerData data = JsonUtility.FromJson<PlayerData>(jsonData);

            // Assign loaded values to module_challenge arrays
            this.characterModelIndex = data.characterModelIndex;
            this.coins = data.coins;
            this.modules = data.modules;

            Debug.Log("Module challenges loaded from: " + dataFilePath);
        }
        else
        {
            Debug.LogError("File not found: " + dataFilePath);
        }
    }

    [System.Serializable]
    public class PlayerData
    {
        public int characterModelIndex;
        public int coins;

        public Module[] modules;
    }

    [System.Serializable]
    public class Module
    {
        public int Id;

        public int[] challenges;
    }
}
