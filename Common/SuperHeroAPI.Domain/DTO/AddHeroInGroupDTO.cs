using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperHeroAPI.Domain.DTO
{
    public class AddHeroInGroupDTO
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public DateTimeOffset? DateOfCreation { get; set; }
    }
}
