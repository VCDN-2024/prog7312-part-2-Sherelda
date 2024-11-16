using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace st10083869.prog7312.poe
{ // Service Request class to store request information
    public class ServiceRequest : IComparable<ServiceRequest>
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public int Priority { get; set; }
        public string Location { get; set; }
        public DateTime DateSubmitted { get; set; }

        public ServiceRequest(int id, string description, string status, int priority, string location, DateTime dateSubmitted)
        {
            Id = id;
            Description = description;
            Status = status;
            Priority = priority;
            Location = location;
            DateSubmitted = dateSubmitted;
        }

        public int CompareTo(ServiceRequest other)
        {
            return Id.CompareTo(other.Id);
        }
    }
}