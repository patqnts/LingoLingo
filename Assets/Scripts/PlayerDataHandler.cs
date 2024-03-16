using UnityEngine;
using System.IO;

public class PlayerDataHandler : MonoBehaviour
{
    public static PlayerDataHandler instance;
    private GameObject player;

    [Header("Module1")]
    public int[] module1_challenge;

    [Header("Module2")]
    public int[] module2_challenge;

    [Header("Module3")]
    public int[] module3_challenge1;

    [Header("Module4")]
    public int[] module4_challenge1;

    [Header("Module5")]
    public int[] module5_challenge1;

    [Header("Module6")]
    public int[] module6_challenge1;

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
    }

    public void SetModule1Value(int challenge, int value)
    {
        if (challenge >= 0 && challenge < module1_challenge.Length)
        {
            module1_challenge[challenge] = value;
        }
        else
        {
            Debug.LogError("Invalid challenge index for Module 1.");
        }
    }

    // Save all module_challenge arrays to a JSON file in the persistent data path
    public void SaveModuleChallengesToJson()
    {
        PlayerData data = new PlayerData
        {
            module1_challenge = this.module1_challenge,
            module2_challenge = this.module2_challenge,
            module3_challenge1 = this.module3_challenge1,
            module4_challenge1 = this.module4_challenge1,
            module5_challenge1 = this.module5_challenge1,
            module6_challenge1 = this.module6_challenge1
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
            this.module1_challenge = data.module1_challenge;
            this.module2_challenge = data.module2_challenge;
            this.module3_challenge1 = data.module3_challenge1;
            this.module4_challenge1 = data.module4_challenge1;
            this.module5_challenge1 = data.module5_challenge1;
            this.module6_challenge1 = data.module6_challenge1;

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
        public int[] module1_challenge;
        public int[] module2_challenge;
        public int[] module3_challenge1;
        public int[] module4_challenge1;
        public int[] module5_challenge1;
        public int[] module6_challenge1;
    }
}
