using System;
using System.Collections.Generic;
using _Sources.Model;

public class TrapData
{
    private List<PositionInMap> _traps;

    public bool IsPlayerInTrap { get; private set; } = false;

    public TrapData()
    {
        _traps = new List<PositionInMap>();
    }

    public void AddTraps(List<PositionInMap> traps)
    {
        if(traps == null)
            throw new ArgumentNullException(nameof(traps));
        
        _traps.AddRange(traps);
    }

    public void TryShowTrap(PositionInMap trapPosition)
    {
        foreach (var position in _traps)
        {
            if (position.X == trapPosition.X & position.Y == trapPosition.Y)
                IsPlayerInTrap = true;
        }
    }

    public void Revert()
    {
        IsPlayerInTrap = false;
    }
}