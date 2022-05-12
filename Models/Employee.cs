using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace DotNetCoreMySQL.Models
{
    public partial class Employee
    {
        
        public Employee()
        {
            Batteries = new HashSet<Battery>();
        }

        
        public long id { get; set; }
        public string? title { get; set; }
        public long? user_id { get; set; }
        [JsonIgnore]
        public DateTime created_at { get; set; }
        [JsonIgnore]
        public DateTime updated_at { get; set; }

        [ForeignKey("user_id")]
        public virtual User? User { get; set; }
        public virtual ICollection<Battery> Batteries { get; set; }
    }
}
