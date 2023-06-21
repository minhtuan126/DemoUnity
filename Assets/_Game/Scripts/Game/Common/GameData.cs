using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using GameCore.Models;

public class GameData : SingletonPersistent<GameData>
{
    public GameConfigStruct gameConfig;
    public List<FishDefine> epConfig;
    public int GetLimitFeedOfFish(FishModel fish)
    {
        int limitFeed = 0;
        if(this.gameConfig != null)
        {
            var config = this.epConfig.Find(x=> (x.rarity == (int)fish.rarity) && (x.gen == (int)fish.gen));
            if(config != null)
            {
                limitFeed = config.limit_feed;
            }
        }
        return limitFeed;
    }

    public int GetLimitEggPiecesOfFish(FishModel fish)
    {
        int limitEggPieces = 0;
        if(this.gameConfig != null)
        {
            var config = this.epConfig.Find(x=> (x.rarity == (int)fish.rarity) && (x.gen == (int)fish.gen));
            if(config != null)
            {
                limitEggPieces = config.ep_limit;
            }
        }
        return limitEggPieces;
    }

    public int GetNumberEggPiecesPerFeedOfFish(FishModel fish)
    {
        int epPerFeed = 0;
        if(this.gameConfig != null)
        {
            var config = this.epConfig.Find(x=> (x.rarity == (int)fish.rarity) && (x.gen == (int)fish.gen));
            if(config != null)
            {
                epPerFeed = config.egg_pieces;
            }
        }
        return epPerFeed;
    }

    public bool CheckIsMaxEggPiecesOfFish(FishModel fish)
    {
        return (fish.count_ep >= this.GetLimitEggPiecesOfFish(fish) && fish.gen != Gen.F15);
    }
}
