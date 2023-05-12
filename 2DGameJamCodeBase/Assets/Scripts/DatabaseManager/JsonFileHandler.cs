using UnityEngine;
using System;
using System.IO;
using System.Text;

public class JsonFileHandler
{
    private string _dataDirPath = "";
    private string _dataFileName = "";
    private bool _useEncryption = false;
    private readonly string _encryptionKeyword = "malgur";

    private StringBuilder stringBuilder;

    public JsonFileHandler(string dataDirPath, string dataFileName, bool useEncryption)
    {
        _dataDirPath = dataDirPath;
        _dataFileName = dataFileName;
        _useEncryption = useEncryption;

        stringBuilder = new StringBuilder();
    }

    public RecordedData LoadData(RecordedData recordedData)
    {
        string fullPath = Path.Combine(_dataDirPath, _dataFileName);
        
        RecordedData loadedData = null;
        
        if (File.Exists(fullPath))
        {
            try
            {
                string dataToLoad = "";
                
                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }

                // If encryption is checked decrypt the data before converting to GameData.

                if (_useEncryption)
                {
                    dataToLoad = EncryptDecrypt(dataToLoad);
                }

                // Convert from JSON to data.
                // LOAD PROBLEM HERE
                
                loadedData = (RecordedData)JsonUtility.FromJson(dataToLoad, recordedData.GetType());
            }
            catch (Exception e)
            {
                Debug.LogError("Error occured to file: " + fullPath + "\n" + e);
            }
        }
        
        return loadedData;
    }

    public void SaveData(RecordedData data)
    {
        string fullPath = Path.Combine(_dataDirPath, _dataFileName);
        
        try
        {
            // Create the directory if it does not exist.
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            // Convert from data to JSON.
            string dataToSave = JsonUtility.ToJson(data, true);

            // If encryption is checked encrypt the data before saving it to the file.
            if (_useEncryption)
            {
                dataToSave = EncryptDecrypt(dataToSave);
            }

            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToSave);
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Error occured to file: " + fullPath + "\n" + e);
        }
    }

    private string EncryptDecrypt(string data)
    {
        //string modifiedData = "";

        stringBuilder.Clear();
        
        for (int i = 0; i < data.Length; i++)
        {
            stringBuilder.Append((char)(data[i] ^ _encryptionKeyword[i % _encryptionKeyword.Length]));
            //modifiedData += (char)(data[i] ^ _encryptionKeyword[i % _encryptionKeyword.Length]);
        }

        return stringBuilder.ToString();
        //return modifiedData;
    }
}
