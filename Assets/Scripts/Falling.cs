//This script handles the zeplin falling and rising up

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;


public class Falling : MonoBehaviour {
    private float fall_period { get { return manager.timeLimit; } }
    private float rise_period { get { return manager.timeLimit / 5; } }
    public float top;
    public float bottom;
    public bool invert;
    public GameManager manager;

    private float fall_factor { get { return (invert ? -1 : 1) * (top - bottom) / fall_period; } }
    private float rise_factor { get { return (invert ? -1 : 1) * (top - bottom) / rise_period; } }
    
    private void MoveY(float amount) {
        Vector3 pos = transform.position;
        float y = Mathf.Clamp(pos.y + amount, this.bottom, this.top); 
        transform.position = new Vector3(pos.x, y, pos.z);
    }

    private void Fall () {
        MoveY(- Time.deltaTime * this.fall_factor);
    }
    
    private void Rise (){
        MoveY(Time.deltaTime * this.rise_factor);
    }

    // Update is called once per frame
    void Update () {
        if (this.manager.puzzleSolved()){
            Rise();
        } else {
            Fall();
        }
    }
}
