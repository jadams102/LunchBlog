using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LunchBlog.Models
{
    [Table("People")]
    public class People
    {
        [Key]
        public int PeopleId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int ExperienceId { get; set; }
        public virtual Experience Experience { get; set; }
    }
}
