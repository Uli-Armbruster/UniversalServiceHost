using System.Linq;

namespace UAR.Services.Contracts
{
    public interface IAmService
    {
        string DisplayName { get; }
        string Description { get; }
        string ServiceName { get; }

        void Start();
        void Stop();
    }
}
