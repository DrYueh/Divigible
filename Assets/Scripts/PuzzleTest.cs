using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PuzzleTest : MonoBehaviour {

	List<Puzzle> puzzles;

	void Start() {
		puzzles = new List<Puzzle> ();
		for (int i = 0; i < 10; ++i) {
			puzzles.Add (gameObject.AddComponent<Puzzle> ());
		}
		setup(puzzles [0], new int[] {2, 2, 4}, new string[] {"+", "="}); // 2 + 2 = 4: True
		setup(puzzles [1], new int[] {2, 1, 4}, new string[] {"+", "="}); // 2 + 1 = 4: False
		setup(puzzles [2], new int[] {2, 2, 0}, new string[] {"-", "="}); // 2 - 2 = 0: True
		setup(puzzles [3], new int[] {2, 2, 1}, new string[] {"-", "="}); // 2 - 1 = 4: False
		setup(puzzles [4], new int[] {2, 3, 6}, new string[] {"*", "="}); // 2 * 3 = 6: True
		setup(puzzles [5], new int[] {2, 3, -6}, new string[] {"*", "="}); // 2 * 3 = -6: False
		setup(puzzles [6], new int[] {2, 2, 1}, new string[] {"/", "="}); // 2 / 2 = 1: True
		setup(puzzles [7], new int[] {2, 2, 2}, new string[] {"/", "="}); // 2 / 2 = 2: False

		setup(puzzles [8], new int[] {1, 2, 3, 4, 11}, new string[] {"+", "*", "+", "="}); // 1 + 2 * 3 + 4 = 11: True
		setup(puzzles [9], new int[] {1, 2, 3, 4, 13}, new string[] {"+", "*", "+", "="}); // 1 + 2 * 3 + 4 = 13: False

		foreach (Puzzle puzzle in puzzles) {
			print (puzzle.ToString() + ": " + puzzle.eval ());
		}
	}

	void setup(Puzzle p, int[] leaves, string[] ops) {
		p.setNLeaves (leaves.Count());

		for (int i = 0; i < leaves.Count(); ++i) {
			p.setLeaf(i, leaves[i]);
		}
		for (int i = 0; i < ops.Count (); ++i) {
			p.setOp (i, ops [i]);
		}
	}
}
