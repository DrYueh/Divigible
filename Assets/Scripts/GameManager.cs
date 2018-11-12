using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using System.Security.AccessControl;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public const int TEST_TOP    = 1;
	public const int TEST_MIDDLE = 2;
	public const int TEST_BOTTOM = 3;
    
    public Puzzle puzzleTop;
    public Puzzle puzzleMid;
    public Puzzle puzzleBot;
    
    private bool puzzleTopSolved;
    private bool puzzleMidSolved;
    private bool puzzleBotSolved;

	private int _currentTest;

	private Dictionary<int, string> testMap = new Dictionary<int, string>();
    
    public bool puzzleSolved() {
        return puzzleTopSolved && puzzleMidSolved && puzzleBotSolved;
    }

	// Use this for initialization
	void Start ()
	{
		defineTestDictionary();
		setCurrentTest(TEST_MIDDLE);
	}
	
	// Update is called once per frame
	void Update () {
		Transform currentTestTree = GameObject.Find(testMap[_currentTest]).transform;
		for (int i = 0; i < currentTestTree.childCount; i++)
		{
			GameObject child = currentTestTree.GetChild(i).gameObject;
			child.layer = child.name.Contains("Operator") ? 20 : 15;
		}
        
        puzzleTopSolved = puzzleTop.eval();
        puzzleMidSolved = puzzleMid.eval();
        puzzleBotSolved = puzzleBot.eval();
	}

	private void defineTestDictionary()
	{
		testMap.Add(TEST_TOP, "TopTest");
		testMap.Add(TEST_MIDDLE, "MiddleTest");
		testMap.Add(TEST_BOTTOM, "BottomTest");
	}

	public void setCurrentTest(int test)
	{
		if ((test < 0) || (test > 3))
		{
			throw new IndexOutOfRangeException("test must be either 1, 2, or 3");
		}
		_currentTest = test;
		invalidateTargets();
	}

	private void invalidateTargets()
	{
		GameObject[] targets = GameObject.FindGameObjectsWithTag("target");
		foreach (GameObject target in targets)
		{
			target.layer = 25;
		}
	}
    
    public Puzzle currentPuzzle() {
        switch (_currentTest) {
            case TEST_TOP: return puzzleTop;
            case TEST_MIDDLE: return puzzleMid;
            case TEST_BOTTOM: return puzzleBot;
            default: return null;
        }
    }
}
