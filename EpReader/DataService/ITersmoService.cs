using System.Collections.Generic;
using EpReader.Model;

namespace EpReader.DataService
{
    public interface ITersmoService
    {
        IEnumerable<TersmoModel> LoadDictionary();
    }
}