using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class TEST : ScriptableObject {
    string test;
    GameObject testobject;
}

[CreateAssetMenu]
public class TEST2 : TEST {
    int num;
}
