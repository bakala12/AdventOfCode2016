using System.Text;

namespace Day11
{
    public struct State
    {
        public readonly int[] Floors;
        public readonly int Steps;
        public readonly int ElevatorPosition;

        private State(int[] floors, int elevatorPosition, int steps = 0)
        {
            Floors = floors;
            ElevatorPosition = elevatorPosition;
            Steps = steps;
        }

        public static State InitialState(int[] itemFloors)
        {
            var floors = new int[4];
            for(int i = 0; i < itemFloors.Length; i++)
            {
                floors[itemFloors[i]] |= (1 << i);
            }
            return new State(floors, 0, 0);
        }

        public State CreateNext(int newElevatorPosition, int newElevatorFloor, int newDestinationFloor)
        {
            int[] floors = new int[4];
            Floors.CopyTo(floors, 0);
            floors[ElevatorPosition] = newElevatorFloor;
            floors[newElevatorPosition] = newDestinationFloor;
            return new State(floors, newElevatorPosition, Steps + 1);
        }

        public bool IsFinal()
        {
            return Floors[0] == 0 && Floors[1] == 0 && Floors[2] == 0 && Floors[3] != 0;
        }

        public IEnumerable<State> NextStates()
        {
            int currentFloor = Floors[ElevatorPosition];
            var items = currentFloor.GetAllSetBits();
            if(ElevatorPosition < 3)
            {
                bool doubleMovesGenerated = false;
                int nextFloor = Floors[ElevatorPosition+1];
                for(int ind1 = 0; ind1 < items.Count; ind1++)
                {
                    for(int ind2 = ind1+1; ind2 < items.Count; ind2++)
                    {
                        int mask = (1 << items[ind1]) | (1 << items[ind2]);
                        int currentFloorUpdated = currentFloor & ~mask;
                        int nextFloorUpdated = nextFloor | mask;
                        if(CheckFloor(currentFloorUpdated) && CheckFloor(nextFloorUpdated))
                        {
                            doubleMovesGenerated = true;
                            yield return CreateNext(ElevatorPosition+1, currentFloorUpdated, nextFloorUpdated);
                        }
                    }
                }
                if(!doubleMovesGenerated)
                {
                    for(int ind = 0; ind < items.Count; ind++)
                    {
                        int mask = (1 << items[ind]);
                        int currentFloorUpdated = currentFloor & ~mask;
                        int nextFloorUpdated = nextFloor | mask;
                        if(CheckFloor(currentFloorUpdated) && CheckFloor(nextFloorUpdated))
                            yield return CreateNext(ElevatorPosition+1, currentFloorUpdated, nextFloorUpdated);
                    }
                }
            }
            if(ElevatorPosition > 0)
            {
                bool allEmpty = true;
                for(int lower = ElevatorPosition-1; lower >= 0; lower--)
                    if(Floors[lower] != 0)
                    {
                        allEmpty = false;
                        break;
                    }
                if(allEmpty) 
                    yield break;
                bool singleMoveGenerated = false;
                int nextFloor = Floors[ElevatorPosition-1];
                for(int ind = 0; ind < items.Count; ind++)
                {
                    int mask = (1 << items[ind]);
                    int currentFloorUpdated = currentFloor & ~mask;
                    int nextFloorUpdated = nextFloor | mask;
                    if(CheckFloor(currentFloorUpdated) && CheckFloor(nextFloorUpdated))
                    {
                        singleMoveGenerated = true;
                        yield return CreateNext(ElevatorPosition-1, currentFloorUpdated, nextFloorUpdated);
                    }
                }
                if(!singleMoveGenerated)
                {
                    for(int ind1 = 0; ind1 < items.Count; ind1++)
                    {
                        for(int ind2 = ind1+1; ind2 < items.Count; ind2++)
                        {
                            int mask = (1 << items[ind1]) | (1 << items[ind2]);
                            int currentFloorUpdated = currentFloor & ~mask;
                            int nextFloorUpdated = nextFloor | mask;
                            if(CheckFloor(currentFloorUpdated) && CheckFloor(nextFloorUpdated))
                                yield return CreateNext(ElevatorPosition-1, currentFloorUpdated, nextFloorUpdated);
                        }
                    }
                }
            }
        }

        private static bool CheckFloor(int floor)
        {
            int generators = floor & WorldInfo.Generators;
            int microchips = floor & WorldInfo.Microchips;
            int isolatedMicrochips = microchips & ~(generators << 1);
            return isolatedMicrochips == 0 || generators == 0;
        }
    
        public long GetHash()
        {
            return HashFloor(0) | HashFloor(1) | HashFloor(2) | HashFloor(3) | (((long)ElevatorPosition & 0x3) << 48);
        }

        private long HashFloor(int floor)
        {
            int pairs = 0, gens = 0, mics = 0;
            for(int i = 0; i < WorldInfo.ItemsCount; i += 2)
            {
                switch((Floors[floor] >> i) & 0x3)
                {
                    case 3:
                        pairs++;
                        break;
                    case 2:
                        mics++;
                        break;
                    case 1:
                        gens++;
                        break;
                }
            }
            long hash = ((pairs & 0xF) << 8) | ((gens & 0xF) << 4) | (mics & 0xF);
            return hash << (12*floor);
        }
    
        public string Display(string[] itemNames)
        {
            var sb = new StringBuilder();
            for(int f = 3; f >= 0; f--)
            {
                sb.Append($"F{f+1} {(ElevatorPosition == f ? "E": ".")} ");
                for(int i = 0; i < WorldInfo.ItemsCount; i++)
                    sb.Append($"{((Floors[f] & (1 << i)) != 0 ? itemNames[i] : "...")} ");
                sb.AppendLine();
            }
            return sb.ToString();
        }
    }
}