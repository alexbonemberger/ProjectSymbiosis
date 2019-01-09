using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class KnowledgeData {

    public int id;
    public string name;

    public string[] cardDown;
    public string[] cardUp;

    public KnowledgeData(Knowledge knowledge)
    {
        id = knowledge.id;
        name = knowledge.name;

        cardDown = knowledge.cardDown;
        cardUp = knowledge.cardUp;
    }

    public void AddKnowledge(string cardDownValue, string cardUpValue)
    {
        int index = 256; //out of range
        for (int i = 0; i < cardDown.Length; i++)
        {
            if (cardDown[i] == null)
            {
                index = i;
                break;
            }
        }

        if (index < 256)
        {
            cardDown[index] = cardDownValue;
            cardUp[index] = cardUpValue;
        }
    }

    public void ShowKnowledge()
    {
        for (int i = 0; i < cardDown.Length; i++)
        {
            if(cardDown[i] != null)
                Debug.Log(i.ToString()+" is "+cardDown[i] + " = " + cardUp[i]);
        }
    }
}
