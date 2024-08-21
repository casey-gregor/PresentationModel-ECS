using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

[Serializable]
public class TestZenject
{
    public string name;
    private AnotherTestZenject _anotherTestZenject;
    
    [Inject]
    public void Construct(AnotherTestZenject anotherTestZenject)
    {
        // Debug.Log("TestZenject");
        _anotherTestZenject = anotherTestZenject;
        // Debug.Log("_anotherTestZenject : " + _anotherTestZenject);
    }
}
