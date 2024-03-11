using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class DataPersistenceManager : MonoBehaviour
{
    [Header("File Storage Config")]
    [SerializeField] private string fileName;

    private GameData gameData;
    private List<ISaveLoad> dataPersistenceObjects;
    private FileDataHandler dataHandler;

    private string selectedProfileId = "test";
    public LevelGeneration levelGeneration;

    public static DataPersistenceManager instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("Found more than one Data Persistence Manager in the scene. Destroying the newest one.");
            Destroy(gameObject);
            return;
        }
        instance = this;
        //DontDestroyOnLoad(gameObject);


        dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);

        //InitializeSelectedProfileId();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        dataPersistenceObjects = FindAllDataPersistenceObjects();
        LoadGame();
    }

    public void ChangeSelectedProfileId(string newProfileId)
    {
        // update the profile to use for saving and loading
        selectedProfileId = newProfileId;
        // load the game, which will use that profile, updating our game data accordingly
        LoadGame();
    }

    /*public void DeleteProfileData(string profileId)
    {
        // delete the data for this profile id
        dataHandler.Delete(profileId);
        // initialize the selected profile id
        InitializeSelectedProfileId();
        // reload the game so that our data matches the newly selected profile id
        LoadGame();
    }*/

    /*private void InitializeSelectedProfileId()
    {
        selectedProfileId = dataHandler.GetMostRecentlyUpdatedProfileId();
    }*/

    public void NewGame()
    {
        gameData = new GameData();
    }

    public async void LoadGame()
    {

        // load any saved data from a file using the data handler only if load button is clicked
        if (LoadSettings.shouldLoadFile)
        {
            gameData = await dataHandler.Load(selectedProfileId);
            Debug.Log("Checking for file data");
            if (gameData != null)
            {
                Debug.Log("Data was found");
                levelGeneration.shouldGenerate = false;
            }
            
        }

        Debug.Log(gameData);
        

        // start a new game if the data is null and we're configured to initialize data for debugging purposes
        if (gameData == null)
        {
            NewGame();
        }

        // if no data can be loaded, don't continue
        if (gameData == null)
        {
            Debug.Log("No data was found. A New Game needs to be started before data can be loaded.");
            return;
        }

        //levelGeneration.shouldGenerate = false;

        // push the loaded data to all other scripts that need it
        foreach (ISaveLoad dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.LoadData(gameData);
        }
    }

    public void SaveGame()
    {
        // if we don't have any data to save, log a warning here
        if (gameData == null)
        {
            Debug.LogWarning("No data was found. A New Game needs to be started before data can be saved.");
            return;
        }

        // pass the data to other scripts so they can update it
        foreach (ISaveLoad dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.SaveData(gameData);
        }

        // timestamp the data so we know when it was last saved
        gameData.lastUpdated = System.DateTime.Now.ToBinary();

        // save that data to a file using the data handler
        dataHandler.Save(gameData, selectedProfileId);

        Debug.Log("DataManger Save");
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }

    private List<ISaveLoad> FindAllDataPersistenceObjects()
    {
        // FindObjectsofType takes in an optional boolean to include inactive gameobjects
        IEnumerable<ISaveLoad> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>(true)
            .OfType<ISaveLoad>();

        return new List<ISaveLoad>(dataPersistenceObjects);
    }

    public bool HasGameData()
    {
        return gameData != null;
    }

    /*public Dictionary<string, GameData> GetAllProfilesGameData()
    {
        return dataHandler.LoadAllProfiles();
    }*/
}