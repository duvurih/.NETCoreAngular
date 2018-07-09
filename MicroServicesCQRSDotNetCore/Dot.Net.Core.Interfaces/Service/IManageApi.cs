using System.Collections.Generic;

namespace Dot.Net.Core.Interfaces.Service
{
    public interface IManageApi
    {
        T GetSynch<T>(string controller, string action = null, Dictionary<string, string> data = null);

        T PostSynch<T>(string controller, string action, object data);
    }
}
