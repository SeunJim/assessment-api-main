using Assessment.Domain.Dtos;
using Assessment.Domain.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assessment.Application.Helpers
{
    public class AutoMapperProfiles:Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<DocumentResponseDto, Document>();
            CreateMap<Document, DocumentResponseDto>();
        }
    }
}
