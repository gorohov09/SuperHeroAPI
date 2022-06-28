using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperHeroAPI.Domain.Entities
{
    public class SuperHeroTeam
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public DateTimeOffset DateOfCreation { get; set; }

        public ICollection<SuperHero> Heroes { get; set; } = new List<SuperHero>();
    }
}
