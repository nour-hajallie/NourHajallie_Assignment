using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NourHajallie_AutomationProject.DataEntities
{
    public class PetResponse
    {
        public long id { get; set; }
        public CategoryResponse category { get; set; }
        public string name { get; set; }
        public List<string> photoUrls { get; set; }
        public List<TagResponse> tags { get; set; }
        public string status { get; set; }
    }
}
