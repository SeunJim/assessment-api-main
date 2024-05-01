using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assessment.Domain.Dtos
{
    public class DocumentRequestDto
    {
        public string FileName { get; set; }
        public string FileType { get; set; }
        [Required(ErrorMessage = "PDF file is required")]
        public IFormFile File { get; set; }
    }
    public class DocumentRequestIdDto
    {
        public string documentId { get; set; }
    }
}
