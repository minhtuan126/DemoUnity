
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCore.Models
{
    public class EggPieceModel : EntityModel, IItemBase
    {
        public override void Init(EntityConfig config)
        {
            EggPieceConfig eggPieceConfig = config as EggPieceConfig;
        }
    }

    public class EggPieceConfig : EntityConfig
    {

    }
}
