using EduConnect.DTO;
using EduConnect.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;
using AutoMapper;

namespace EduConnect.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Mapping từ Entity → DTO
            CreateMap<Teacher, Teacher>()
                .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => src.StartTime != null ? src.StartTime.Value.ToString("hh\\:mm") : null))
                .ForMember(dest => dest.EndTime, opt => opt.MapFrom(src => src.EndTime != null ? src.EndTime.Value.ToString("hh\\:mm") : null));

            // Mapping từ DTO → Entity khi tạo mới
            CreateMap<CreateTeacher, Entities.Teacher>()
                .ForMember(dest => dest.StartTime, opt => opt.MapFrom((CreateTeacher src, Entities.Teacher dest) =>
                    !string.IsNullOrEmpty(src.StartTime) ? TimeOnly.Parse(src.StartTime) : (TimeOnly?)null))
                .ForMember(dest => dest.EndTime, opt => opt.MapFrom((CreateTeacher src, Entities.Teacher dest) =>
                    !string.IsNullOrEmpty(src.EndTime) ? TimeOnly.Parse(src.EndTime) : (TimeOnly?)null));

            // Mapping từ DTO → Entity khi cập nhật
            CreateMap<UpdateTeacher, Entities.Teacher>()
                .ForMember(dest => dest.StartTime, opt => opt.MapFrom((UpdateTeacher src, Entities.Teacher dest) =>
                    !string.IsNullOrEmpty(src.StartTime) ? TimeOnly.Parse(src.StartTime) : (TimeOnly?)null))
                .ForMember(dest => dest.EndTime, opt => opt.MapFrom((UpdateTeacher src, Entities.Teacher dest) =>
                    !string.IsNullOrEmpty(src.EndTime) ? TimeOnly.Parse(src.EndTime) : (TimeOnly?)null));
        }
    }

}
