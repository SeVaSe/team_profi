using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace team_profi.Classes
{
    public class StudentRatingViewModel
    {
        public int Rank { get; set; }
        public string FullName { get; set; }
        public int TotalPoints { get; set; }

        public StudentRatingViewModel(int rank, string fullName, int totalPoints)
        {
            Rank = rank;
            FullName = fullName;
            TotalPoints = totalPoints;
        }
    }
}
