using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scrabble.Core.Stats
{
    public class MatchStatistics
    {
        public int Moves { get; set; }

        public int Passes { get; set; }

        public int Swaps { get; set; }

        public int HighestScoringWordScore { get; set; }

        public string HighestScoringWord { get; set; }

    }
}
