using System;
using System.Collections.Generic;
// using System.Text.Json;

namespace GameCore.Models
{
    public abstract class EntityModel
    {
        public int id;
        public string name;

        public abstract void Init(EntityConfig config);
    }

    public abstract class EntityConfig
    {
        public string name;
    }

}
