...

public struct CellIslandStatus
{
	/// <summary>
	/// Whether this cell is a border cell of an island.
	/// </summary>
	public bool border;
	/// <summary>
	/// The index of the island this cell belongs to.
	/// </summary>
	public int islandIdx;
}


protected CellIslandStatus[] cellIslandStatuses;
protected int highestIslandIdx;
/// <summary>
/// During the search for which cells belong to which islands, sometimes one island gets more than one index. This mapping ensures that the higher indexes of the island get remapped to the lowest index of that island.
/// </summary>
protected int[] islandIdxRemappings;

/// <summary>
/// When there is a group of unsolved cells together that is called an island. The board is one island to start with, but as the game progresses the unsolved cells could be separated from each other by solved cells.
/// </summary>
protected void FindIslands()
{
	int i, len;
	int cellCount = boardSettings.cellCount;

	int currIslandIdx;
	//start with 1 since the struct will initialize with 0 as the default
	int nextIslandIdx = 1;
	highestIslandIdx = -1;

	cellIslandStatuses = new CellIslandStatus[cellCount];
	islandIdxRemappings = new int[cellCount];

	//we need to know if we are on a horizontal or vertical edge of the board as we loop through the cells
	bool xEdge;
	bool yEdge = true;
	for (i = 0; i < cellCount; i++)
	{
		IntVector2 cPos = boardSettings.GetCellPos(i);

		xEdge = cPos.x == 0 || cPos.x == boardSettings.cols - 1;
		if (cPos.x == 0)
			yEdge = cPos.y == 0 || cPos.y == boardSettings.rows - 1;
		//when we are on an edge or in a corner then the number of surrounding cells is different than if we are not touching an edge.
		byte surClosedPossibleCount = (byte)((xEdge && yEdge) ? 3 : (xEdge || yEdge ? 5 : 8));

		//if this cell is closed and touches an open cell or flagged cell then it is an edge of an island. These are the important cells to pay attention to

		currCellStatus = game.GetCellStatus_internal(i);

		//if closed or flagged or closed and not touching a flagged or open cell
		if (currCellStatus.stateValue > CellInfo.VALUE_CLOSED
			|| currCellStatus.stateValue == CellInfo.VALUE_FLAGGED
			|| (currCellStatus.surFlaggedCount == 0 && currCellStatus.surClosedCount == surClosedPossibleCount))
		{
			//This is not an important cell, so continue.
			if (ELogger.verboseLevel >= ELogger.VerboseLevel.all)
				Debug.Log($"DsS.FindIslands skip cell: value {currCellStatus.stateValue} cStatus.surFlaggedCount {currCellStatus.surFlaggedCount} cStatus.surClosedCount {currCellStatus.surClosedCount} surClosedPossibleCount {surClosedPossibleCount}");

			continue;
		}

		cellIslandStatuses[i].border = true;

		if (cellIslandStatuses[i].islandIdx > 0)
		{
			currIslandIdx = cellIslandStatuses[i].islandIdx;
		}
		else
		{
			currIslandIdx = nextIslandIdx;
			islandIdxRemappings[currIslandIdx] = currIslandIdx;
			cellIslandStatuses[i].islandIdx = currIslandIdx;
			nextIslandIdx++;
		}

		//look at touching all cells around this one(closed and open) to see if they have an island idx already and take that one if found
		//if found a touching or close cell that has an island idx and it is different from this one then set all cells of the higher island to the lower island idx

		for (int j = 0, jen = NB_DIST_1_COUNT; j < jen; j++)
		{
			ByteVector2 nbOffset = nbOffsets[j];

			nbPos = new IntVector2(cPos.x + nbOffset.x, cPos.y + nbOffset.y);

			if (boardSettings.CoordOutsideBoard(nbPos)) continue;

			int nbIdx = boardSettings.GetCellIndex(nbPos.x, nbPos.y);
			CellStatus nbCStatus = game.GetCellStatus_internal(nbIdx);
			if (cellIslandStatuses[nbIdx].islandIdx != 0 && cellIslandStatuses[nbIdx].islandIdx < currIslandIdx)
			{
				islandIdxRemappings[currIslandIdx] = cellIslandStatuses[nbIdx].islandIdx;
				currIslandIdx = cellIslandStatuses[nbIdx].islandIdx;
				cellIslandStatuses[i].islandIdx = currIslandIdx;
			}
			else if (nbCStatus.stateValue != CellInfo.VALUE_FLAGGED)
			{
				cellIslandStatuses[nbIdx].islandIdx = currIslandIdx;
			}
		}
	}

	//point all remapped island index to the lowest remapped index of that island
	for (i = 1; i < nextIslandIdx; i++)
	{
		if (ELogger.verboseLevel >= ELogger.VerboseLevel.all)
			Debug.Log($"DsS.FindIslands island idx {i} pointing to island idx {islandIdxRemappings[i]} gettig remapped to {islandIdxRemappings[islandIdxRemappings[i]]}");

		islandIdxRemappings[i] = islandIdxRemappings[islandIdxRemappings[i]];

		highestIslandIdx = Mathf.Max(islandIdxRemappings[i], highestIslandIdx);
	}

	//change all island indexes to the lowest index of the island so the whole island has the same index
	for (i = 0, len = cellIslandStatuses.Length; i < len; i++)
	{
		if (cellIslandStatuses[i].border)
		{
			cellIslandStatuses[i].islandIdx = islandIdxRemappings[cellIslandStatuses[i].islandIdx];
		}
		else
		{
			//for non-border cells they shouldn't have an island index
			cellIslandStatuses[i].islandIdx = 0;
		}
	}

	//all flagged cells touching borders should be set to the island index and set as border
	for (i = 0; i < cellCount; i++)
	{
		if (cellIslandStatuses[i].border)
		{
			IntVector2 cPos = boardSettings.GetCellPos(i);
			int firstX = Mathf.Max(0, cPos.x - 1);
			int lastX = Mathf.Min(boardSettings.cols - 1, cPos.x + 1);
			int firstY = Mathf.Max(0, cPos.y - 1);
			int lastY = Mathf.Min(boardSettings.rows - 1, cPos.y + 1);
			for (int y = firstY; y <= lastY; y++)
			{
				for (int x = firstX; x <= lastX; x++)
				{
					int nbCIdx = boardSettings.GetCellIndex(x, y);
					if (cellIslandStatuses[nbCIdx].islandIdx == 0)
					{
						CellState nbCState = game.GetCellState_internal(nbCIdx);

						if (nbCState == CellState.flag)
							cellIslandStatuses[nbCIdx].islandIdx = cellIslandStatuses[i].islandIdx;
					}
				}
			}
		}
	}
}

...