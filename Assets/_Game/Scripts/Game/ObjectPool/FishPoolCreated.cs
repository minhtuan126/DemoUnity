
public class FishPoolCreated : ObjectPoolCreatedBase<FishType>, IPoolCreated
{
    protected override IPoolManager GetPoolManager()
    {
        return transform.root.GetComponent<PoolManager>();
    }
}

public enum FishType
{
    None = -1,
    // Castles
    FishDefaultF1,
    FishDefaultF2,
    FishDefaultF3,
    FishDefaultF4,
    FishDefaultF5,
    FishDefaultF6,
    FishDefaultF7,

    FishCommonF1,
    FishCommonF2,
    FishCommonF3,
    FishCommonF4,
    FishCommonF5,
    FishCommonF6,
    FishCommonF7,

    FishGreatF1,
    FishGreatF2,
    FishGreatF3,
    FishGreatF4,
    FishGreatF5,
    FishGreatF6,
    FishGreatF7,

    FishRateF1,
    FishRateF2,
    FishRateF3,
    FishRateF4,
    FishRateF5,
    FishRateF6,
    FishRateF7,

    FishEpicF1,
    FishEpicF2,
    FishEpicF3,
    FishEpicF4,
    FishEpicF5,
    FishEpicF6,
    FishEpicF7,


    Count
}

