using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PuzzleTest : MonoBehaviour {

	void Start() {
		doPuzzle(true,  new int[] {2, 2, 4}, new string[] {"+", "="}); // 2 + 2 = 4: True
		doPuzzle(false, new int[] {2, 1, 4}, new string[] {"+", "="}); // 2 + 1 = 4: False
		doPuzzle(true,  new int[] {2, 2, 0}, new string[] {"-", "="}); // 2 - 2 = 0: True
		doPuzzle(false, new int[] {2, 2, 1}, new string[] {"-", "="}); // 2 - 1 = 4: False
		doPuzzle(true,  new int[] {2, 3, 6}, new string[] {"*", "="}); // 2 * 3 = 6: True
		doPuzzle(false, new int[] {2, 3, -6}, new string[] {"*", "="}); // 2 * 3 = -6: False
		doPuzzle(true,  new int[] {2, 2, 1}, new string[] {"/", "="}); // 2 / 2 = 1: True
		doPuzzle(false, new int[] {2, 2, 2}, new string[] {"/", "="}); // 2 / 2 = 2: False

		doPuzzle(true,  new int[] {1, 2, 3, 4, 11}, new string[] {"+", "*", "+", "="}); // 1 + 2 * 3 + 4 = 11: True
		doPuzzle(false, new int[] {1, 2, 3, 4, 13}, new string[] {"+", "*", "+", "="}); // 1 + 2 * 3 + 4 = 13: False
	}

	void doPuzzle(bool expected, int[] leaves, string[] ops) {
		Puzzle p = gameObject.AddComponent<Puzzle> ();
		p.setNLeaves (leaves.Count());

		for (int i = 0; i < leaves.Count(); ++i) {
			p.setLeaf(i, leaves[i]);
		}
		for (int i = 0; i < ops.Count (); ++i) {
			p.setOp (i, ops [i]);
		}

		if (p.eval() == expected) {
			Debug.Log(p.ToString() + " is " + expected);
		} else {
			Debug.LogError(p.ToString() + " is not " + expected);
		}

		Destroy(p);		
	}
}
