using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class GenericData {

    public int intI;
    public int intII;
    public int intIII;
    public int intIV;
    public int intV;
    public int intVI;
    public int intVII;
    public int intVIII;
    public int intIX;
    public int intX;

    public float floatI;
    public float floatII;
    public float floatIII;
    public float floatIV;
    public float floatV;
    public float floatVI;
    public float floatVII;
    public float floatVIII;
    public float floatIX;
    public float floatX;

    public float[] myVector3I = new float[3];
    public float[] myVector3II = new float[3];
    public float[] myVector3III = new float[3];
    public float[] myVector3IV = new float[3];
    public float[] myVector3V = new float[3];
    public float[] myVector3VI = new float[3];
    public float[] myVector3VII = new float[3];
    public float[] myVector3VIII = new float[3];
    public float[] myVector3IX = new float[3];
    public float[] myVector3X = new float[3];

    public GenericData(Type type)
    {
        //Activator.CreateInstance(type).GetType().GetFields().GetValue(0);
    }
}
