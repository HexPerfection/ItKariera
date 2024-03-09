using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using UnityEngine.Networking;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;

public class FileDataHandler
{
    private string dataDirPath = "";
    private string dataFileName = "";
    //private readonly string backupExtension = ".bak";

    public FileDataHandler(string dataDirPath, string dataFileName)
    {
        this.dataDirPath = dataDirPath;
        this.dataFileName = dataFileName;
    }

    public async Task<GameData> Load(string profileId)
    {
        // base case - if the profileId is null, return right away
        if (profileId == null)
        {
            return null;
        }

        string loadedData = await Get("https://localhost:7111/api/Load");


        GameData data = JsonUtility.FromJson<GameData>(loadedData);

        return data;
        // use Path.Combine to account for different OS's having different path separators
        /*string fullPath = Path.Combine(dataDirPath, profileId, dataFileName);
        Debug.Log(fullPath);
        GameData loadedData = null;
        if (File.Exists(fullPath))
        {
            try
            {
                // load the serialized data from the file
                string dataToLoad = "";
                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }

                // optionally decrypt the data

                // deserialize the data from Json back into the C# object
                loadedData = JsonUtility.FromJson<GameData>(dataToLoad);
            }
            catch (Exception e)
            {
                // since we're calling Load(..) recursively, we need to account for the case where
                // the rollback succeeds, but data is still failing to load for some other reason,
                // which without this check may cause an infinite recursion loop.
                if (allowRestoreFromBackup)
                {
                    Debug.LogWarning("Failed to load data file. Attempting to roll back.\n" + e);
                    bool rollbackSuccess = AttemptRollback(fullPath);
                    if (rollbackSuccess)
                    {
                        // try to load again recursively
                        loadedData = Load(profileId, false);
                    }
                }
                // if we hit this else block, one possibility is that the backup file is also corrupt
                else
                {
                    Debug.LogError("Error occured when trying to load file at path: "
                        + fullPath + " and backup did not work.\n" + e);
                }
            }
        }*/
        
    }

    public async void Save(GameData data, string profileId)
    {    
        
        // base case - if the profileId is null, return right away
        if (profileId == null)
        {
            Debug.Log("No profile id");
            return;
        }

        string dataToStore = JsonUtility.ToJson(data, true);

        await Post("https://localhost:7111/api/Save", dataToStore);
        // use Path.Combine to account for different OS's having different path separators
        /*string fullPath = Path.Combine(dataDirPath, profileId, dataFileName);
        string backupFilePath = fullPath + backupExtension;
        try
        {
            // create the directory the file will be written to if it doesn't already exist
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            Debug.Log(Path.GetDirectoryName(fullPath));

            // serialize the C# game data object into Json
        

            // write the serialized data to the file
            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                    Debug.Log("File created");
                }
            }

            // verify the newly saved file can be loaded successfully
            GameData verifiedGameData = Load(profileId);
            // if the data can be verified, back it up
            if (verifiedGameData != null)
            {
                File.Copy(fullPath, backupFilePath, true);
            }
            // otherwise, something went wrong and we should throw an exception
            else
            {
                throw new Exception("Save file could not be verified and backup could not be created.");
            }

        }
        catch (Exception e)
        {
            Debug.LogError("Error occured when trying to save data to file: " + fullPath + "\n" + e);
        }*/
    }

    private async Task<string> Get(string url)
    {
        var httpClient = new HttpClient();
        return await httpClient.GetStringAsync(url);
    }

    public async Task Post(string url, string jsonData)
    {
        // Create JSON content
        StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

        // Send POST request
        using (HttpClient httpClient = new HttpClient())
        {
            HttpResponseMessage response = await httpClient.PostAsync(url, content);
            // Read response content
            string responseContent = await response.Content.ReadAsStringAsync();
            // Log response content
            Debug.Log($"Post request successful! Received: {responseContent}");
        }
    }

    /*public void Delete(string profileId)
    {
        // base case - if the profileId is null, return right away
        if (profileId == null)
        {
            return;
        }

        string fullPath = Path.Combine(dataDirPath, profileId, dataFileName);
        try
        {
            // ensure the data file exists at this path before deleting the directory
            if (File.Exists(fullPath))
            {
                // delete the profile folder and everything within it
                Directory.Delete(Path.GetDirectoryName(fullPath), true);
            }
            else
            {
                Debug.LogWarning("Tried to delete profile data, but data was not found at path: " + fullPath);
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Failed to delete profile data for profileId: "
                + profileId + " at path: " + fullPath + "\n" + e);
        }
    }

    public Dictionary<string, GameData> LoadAllProfiles()
    {
        Dictionary<string, GameData> profileDictionary = new Dictionary<string, GameData>();

        // loop over all directory names in the data directory path
        IEnumerable<DirectoryInfo> dirInfos = new DirectoryInfo(dataDirPath).EnumerateDirectories();
        foreach (DirectoryInfo dirInfo in dirInfos)
        {
            string profileId = dirInfo.Name;

            // defensive programming - check if the data file exists
            // if it doesn't, then this folder isn't a profile and should be skipped
            string fullPath = Path.Combine(dataDirPath, profileId, dataFileName);
            if (!File.Exists(fullPath))
            {
                Debug.LogWarning("Skipping directory when loading all profiles because it does not contain data: "
                    + profileId);
                continue;
            }

            // load the game data for this profile and put it in the dictionary
            GameData profileData = Load(profileId);
            // defensive programming - ensure the profile data isn't null,
            // because if it is then something went wrong and we should let ourselves know
            if (profileData != null)
            {
                profileDictionary.Add(profileId, profileData);
            }
            else
            {
                Debug.LogError("Tried to load profile but something went wrong. ProfileId: " + profileId);
            }
        }

        return profileDictionary;
    }

    public string GetMostRecentlyUpdatedProfileId()
    {
        string mostRecentProfileId = null;

        Dictionary<string, GameData> profilesGameData = LoadAllProfiles();
        foreach (KeyValuePair<string, GameData> pair in profilesGameData)
        {
            string profileId = pair.Key;
            GameData gameData = pair.Value;

            // skip this entry if the gamedata is null
            if (gameData == null)
            {
                continue;
            }

            // if this is the first data we've come across that exists, it's the most recent so far
            if (mostRecentProfileId == null)
            {
                mostRecentProfileId = profileId;
            }
            // otherwise, compare to see which date is the most recent
            else
            {
                DateTime mostRecentDateTime = DateTime.FromBinary(profilesGameData[mostRecentProfileId].lastUpdated);
                DateTime newDateTime = DateTime.FromBinary(gameData.lastUpdated);
                // the greatest DateTime value is the most recent
                if (newDateTime > mostRecentDateTime)
                {
                    mostRecentProfileId = profileId;
                }
            }
        }
        return mostRecentProfileId;
    }*/
}