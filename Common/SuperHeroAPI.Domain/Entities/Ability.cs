using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperHeroAPI.Domain.Entities
{
    public class Ability
    {
        public int Id { get; set; }

        public string Description { get; set; } = string.Empty;

        public ICollection<SuperHero> SuperHeroes { get; set; } = new List<SuperHero>();
    }
}
