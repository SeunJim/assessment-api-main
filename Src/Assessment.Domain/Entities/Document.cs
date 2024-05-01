using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assessment.Domain.Entities
{
    public class Document:BaseEntity
    {
        public string FileName { get; set; }
        public string FileType { get; set; }
        public string Location { get; set; }
        public string PublicId { get; set; }
    }
}
