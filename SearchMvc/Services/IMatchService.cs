using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchMvc.Services
{
    public interface IMatchService
    {
        int Count(string input, string url);
    }
}
