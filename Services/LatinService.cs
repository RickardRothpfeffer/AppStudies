using Microsoft.EntityFrameworkCore;
using Models;
using System.Linq;
using Seido.Utilities.SeedGenerator;

namespace Services;


public class LatinService
{
    readonly List<string> _latins = new SeedGenerator().LatinSentences(100).ToList();

    #region CRUD 
    public int Count(string filter)
    {
        filter ??= "";
        return _latins.Count(l =>
            l.ToLower().Contains(filter.ToLower()));
    }

    public List<string> ReadSentences(int? pageNumber, int? pageSize, string filter)
    {
        filter ??= "";
        if (pageNumber == null || pageSize == null)
        {
            return _latins.Where(l =>
                    l.ToLower().Contains(filter.ToLower())).ToList();
        }

        return _latins.Where(l =>
                    l.ToLower().Contains(filter.ToLower())).ToList()

            //Adding paging
            .Skip(pageNumber.Value * pageSize.Value)
            .Take(pageSize.Value).ToList();
    }

    #endregion
}



