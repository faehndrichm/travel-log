using System;

namespace journey_service.Entities
{
    public class Journey
    {
        public long Id { get; set; }
        public required string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}