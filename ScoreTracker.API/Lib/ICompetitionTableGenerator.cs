using System.Collections.Generic;
using ScoreTracker.API.Model;

namespace ScoreTracker.API.Lib
{
    public interface ICompetitionTableGenerator
    {
        List<TableItem> GenerateTable(Group group);
    }
}