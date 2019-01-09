using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;

public static class SaveSystem {


    public static void SavePlayerStats (PlayerStats playerStats)
    {
        string path = Application.persistentDataPath + "/playerStats.knt";

        BinaryFormatter formatter = new BinaryFormatter();

        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerStatsData data = new PlayerStatsData(playerStats);

        formatter.Serialize(stream, data);

        stream.Close();
    }

    public static PlayerStatsData LoadPlayerStats ()
    {
        string path = Application.persistentDataPath + "/playerStats.knt";

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerStatsData data = formatter.Deserialize(stream) as PlayerStatsData;

            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("save file not found in " + path);
            return null;
        }
    }

    //Generic Save
    //dont use this yet
    public static void Save(Type type, string nameOfFile)
    {
        string path = Application.persistentDataPath + "/"+nameOfFile+".knt";

        BinaryFormatter formatter = new BinaryFormatter();

        FileStream stream = new FileStream(path, FileMode.Create);

        GenericData data = Activator.CreateInstance(type) as GenericData;

        formatter.Serialize(stream, data);

        stream.Close();
    }

    //Evoker
    public static void SaveEvoker(EvokerStats evokerStats)
    {
        string path = Application.persistentDataPath + "/evokerStats.knt";

        BinaryFormatter formatter = new BinaryFormatter();

        FileStream stream = new FileStream(path, FileMode.Create);

        EvokerStatsData data = new EvokerStatsData(evokerStats);

        formatter.Serialize(stream, data);

        stream.Close();
    }

    public static EvokerStatsData LoadEvokerStats()
    {
        string path = Application.persistentDataPath + "/evokerStats.knt";

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            EvokerStatsData data = formatter.Deserialize(stream) as EvokerStatsData;

            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("save file not found in " + path);
            return null;
        }
    }

    //knowledge base

    public static void SaveKnowledge(Knowledge knowledge)
    {
        string path = Application.persistentDataPath + "/KnowledgeBase"+knowledge.id.ToString()+".knowledge";

        BinaryFormatter formatter = new BinaryFormatter();

        FileStream stream = new FileStream(path, FileMode.Create);

        KnowledgeData data = new KnowledgeData(knowledge);

        formatter.Serialize(stream, data);

        stream.Close();
    }

    public static KnowledgeData LoadKnowledge(int id)
    {
        string path = Application.persistentDataPath + "/KnowledgeBase" + id.ToString() + ".knowledge";

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            KnowledgeData data = formatter.Deserialize(stream) as KnowledgeData;

            stream.Close();
            return data;
        }
        else
        {
            Debug.Log("save file not found in " + path);
            return null;
        }
    }
}
