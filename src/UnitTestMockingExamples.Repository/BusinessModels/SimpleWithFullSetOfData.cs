using System.Collections.Generic;

namespace UnitTestMockingExamples.Repository.BusinessModels
{
    public class SimpleWithFullSetOfData
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public List<int> LuckyNumbers { get; set; }
    }
}
