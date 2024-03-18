using UnityEngine;
using System.IO;

public class PlayerDataHandler : MonoBehaviour
{
    public static PlayerDataHandler instance;
    private GameObject player;
    public int characterModelIndex;
    public int coins;
    public GameObject[] CharacterModels;
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

        if (challengeScore > currentScore)
        {
            modules[moduleIndex].challenges[challengeIndex] = challengeScore;
        }
        else
        {
            Debug.LogError("Score is not higher.");
        }

        SaveModuleChallengesToJson();
    }

    // Save all module_challenge arrays to a JSON file in the persistent data path
    public void SaveModuleChallengesToJson()
    {
        PlayerData data = new PlayerData
        {
            characterModelIndex = this.characterModelIndex,
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

            SetCharacterModelIndex(this.characterModelIndex);
            Debug.Log("Module challenges loaded from: " + dataFilePath);
        }
        else
        {
            CreateNewSaveFile();
        }

       
    }


    private void CreateNewSaveFile()
    {
        // Initialize modules array with default challenges
        modules = new Module[6]; // Change numberOfModules to the actual number of modules
        for (int i = 0; i < modules.Length; i++)
        {
            modules[i] = new Module
            {
                Id = i,
                challenges = new int[3] // Change numberOfChallenges to the actual number of challenges per module
            };
            for (int j = 0; j < modules[i].challenges.Length; j++)
            {
                modules[i].challenges[j] = 0; // Set all challenges to 0 by default
            }
        }

        // Create new PlayerData instance with default values
        PlayerData newData = new PlayerData
        {
            characterModelIndex = 0, // Assuming the default character model index is 0
            coins = 0, // Assuming the default number of coins is 0
            modules = modules
        };

        // Serialize and save the new PlayerData instance to JSON
        string jsonData = JsonUtility.ToJson(newData);
        File.WriteAllText(dataFilePath, jsonData);

        Debug.Log("New save file created with default values at: " + dataFilePath);
    }

    public void CharacterScroll()
    {
        

        if(characterModelIndex <= CharacterModels.Length)
        {
            characterModelIndex++;

            if (characterModelIndex == CharacterModels.Length)
            {
                characterModelIndex = 0;
            }
            SetCharacterModelIndex(characterModelIndex);
            SaveModuleChallengesToJson();
        }
    }
    public void SetCharacterModelIndex(int i)
    {
        foreach(GameObject obj in CharacterModels)
        {
            obj.SetActive(false);
        }

        CharacterModels[i].SetActive(true);
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
