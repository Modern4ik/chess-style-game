using System;
using System.Collections;
using System.Collections.Generic;

public class MovePattern
{
	/*
	 * В чём идея: тут будут записан порядок ходов, которые собирается сделать юнит.
	 * Значения в List-e упорядочены.
	 */
	public List<Coordinate> moveSequence;

	public MovePattern(List<Coordinate> moveSequence)
	{
		this.moveSequence = moveSequence;
	}
}

