using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NeumeierJob.Util
{
    public interface IFileLoader
    {
        public Task<string> LoadAsync(string name);
    }
}
