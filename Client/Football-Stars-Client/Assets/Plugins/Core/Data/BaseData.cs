using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data
{
    [Serializable]
    public abstract class BaseDataElement<TKey>
    {
        TKey ID = default(TKey);

        public abstract bool Validate();
        public abstract void Load();
    }
}
