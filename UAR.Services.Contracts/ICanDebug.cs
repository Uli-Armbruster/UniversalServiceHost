using System.Collections.Generic;

namespace UAR.Services.Contracts
{
    public interface ICanDebug
    {
        void This(IList<IAmService> services);
    }
}