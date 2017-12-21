using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interop.ExternalCC.HandlersHelper.Contracts
{
    public interface ICacheHelper
    {
        void Add<T>(T o, string key);
        void Clear(string key);
        void ClearAll();
        bool Exists(string key);
        T Get<T>(string key);
        int GetAllCount();
    }
}
