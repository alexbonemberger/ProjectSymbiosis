using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class Knowledge : MonoBehaviour {

    public string[] cardDown;
    public string[] cardUp;

    public int id;
    public new string name;

    public KnowledgeData knowledgeData;

	// Use this for initialization
	void Awake () {
        CreateKnowledge();
        //carregar base de conhecimento
        try
        {
            knowledgeData = SaveSystem.LoadKnowledge(id);
            name = knowledgeData.name;
            cardDown = knowledgeData.cardDown;
            cardUp = knowledgeData.cardUp;
            Debug.Log("base de dados carregada com sucesso");
        }
        catch
        {
            //criar base de dados
            Debug.Log("criando base de dados...");
            id = 0;
            name = "test";
            cardDown = new string[256];
            cardUp = new string[256];
            SaveSystem.SaveKnowledge(this);
            //carregar base de dados
            knowledgeData = SaveSystem.LoadKnowledge(id);
            name = knowledgeData.name;
            cardDown = knowledgeData.cardDown;
            cardUp = knowledgeData.cardUp;
            Debug.Log("base de dados criada com sucesso");
        }
        //knowledgeData.AddKnowledge("1", "um");
        //SaveSystem.SaveKnowledge(this);
        knowledgeData.ShowKnowledge();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void CreateKnowledge()
    {
        string path = EditorUtility.OpenFilePanel("selecione o arquivo", "", "txt");

        StreamReader reader = new StreamReader(path);
        string[] dataToSave = reader.ReadToEnd().Split('\n');
        for (int i = 0; i < dataToSave.Length; i++)
        {
            //dataToSave[i].Replace("axb_NewLine", "\n");
            if (dataToSave[i].Contains("<NewLine>"))
            {
                dataToSave[i] = dataToSave[i].Replace("<NewLine>", "\n");
            }
        }
        Debug.Log(dataToSave[2]);
        reader.Close();
    }
}
