using System.Collections.Generic;

namespace UnitTestMockingExamples.Responses
{
    public class SimpleWithFullSetOfDataResponse
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public List<int> LuckyNumbers { get; set; }

        public bool HasError { get; set; }
    }
}
