using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UnitTestMockingExamples.Database.DbModels
{
    public class SimpleDbModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        public string Description { get; set; }

        public List<int> LuckyNumbers { get; set; }
    }
}
