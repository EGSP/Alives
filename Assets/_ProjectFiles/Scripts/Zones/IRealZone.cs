using System;
using System.Collections.Generic;

namespace Alive
{
    public interface IRealZone
    {
        void CheckEntities(IEnumerable<WorldManager.EntityWrap> entityWraps);
    }
}