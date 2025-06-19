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
            CreateMap<Teacher, TeacherDTO>()
                .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => src.StartTime != null ? src.StartTime.Value.ToString("hh\\:mm") : null))
                .ForMember(dest => dest.EndTime, opt => opt.MapFrom(src => src.EndTime != null ? src.EndTime.Value.ToString("hh\\:mm") : null));

            // Mapping từ DTO → Entity khi tạo mới
            CreateMap<CreateTeacherDTO, Teacher>()
                .ForMember(dest => dest.StartTime, opt => opt.MapFrom((src, dest) =>
                    !string.IsNullOrEmpty(src.StartTime) ? TimeOnly.Parse(src.StartTime) : (TimeOnly?)null))
                .ForMember(dest => dest.EndTime, opt => opt.MapFrom((src, dest) =>
                    !string.IsNullOrEmpty(src.EndTime) ? TimeOnly.Parse(src.EndTime) : (TimeOnly?)null));

            // Mapping từ DTO → Entity khi cập nhật
            CreateMap<UpdateTeacherDTO, Teacher>()
                .ForMember(dest => dest.StartTime, opt => opt.MapFrom((src, dest) =>
                    !string.IsNullOrEmpty(src.StartTime) ? TimeOnly.Parse(src.StartTime) : (TimeOnly?)null))
                .ForMember(dest => dest.EndTime, opt => opt.MapFrom((src, dest) =>
                    !string.IsNullOrEmpty(src.EndTime) ? TimeOnly.Parse(src.EndTime) : (TimeOnly?)null));
        }
    }

}
