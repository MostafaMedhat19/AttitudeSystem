using AttitudeSystem.Application.DTOs.AttitudeRecordDtos;
using AttitudeSystem.Application.DTOs.ManagerDtos;
using AttitudeSystem.Application.DTOs.PrincipalDtos;
using AttitudeSystem.Application.DTOs.RegisterDtos;
using AttitudeSystem.Application.DTOs.ReportDtos;
using AttitudeSystem.Application.DTOs.StudentDtos;
using AttitudeSystem.Application.DTOs.TeacherDtos;
using AttitudeSystem.Application.DTOs.UserDtos;
using AttitudeSystem.Application.DTOs.VicePrincipalDtos;
using AttitudeSystem.Domain.Entities;
using AttitudeSystem.Domain.Enums;
using AutoMapper;

namespace AttitudeSystem.Application.Mappings
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
          
            CreateMap<Student, StudentDto>();
            CreateMap<CreateStudentDto, Student>();
            CreateMap<AttitudeRecord, AttitudeRecordDto>().ReverseMap();

            
            CreateMap<User, UserDto>()
                .Include<Principal, PrincipalDto>()
                .Include<VicePrincipal, VicePrincipalDto>()
                .Include<Manager, ManagerDto>()
                .Include<Teacher, TeacherDto>();

            CreateMap<Principal, PrincipalDto>();
            CreateMap<VicePrincipal, VicePrincipalDto>();
            CreateMap<Manager, ManagerDto>();
            CreateMap<Teacher, TeacherDto>();

            CreateMap<RegisterDto, User>()
                 .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
                 .Include<RegisterDto, Principal>()
                 .Include<RegisterDto, VicePrincipal>()
                  .Include<RegisterDto, Manager>()
                   .Include<RegisterDto, Teacher>();


            CreateMap<RegisterDto, Principal>()
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => UserRole.Principal));
            CreateMap<RegisterDto, VicePrincipal>()
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => UserRole.VicePrincipal));
            CreateMap<RegisterDto, Manager>()
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => UserRole.Manager));
            CreateMap<RegisterDto, Teacher>()
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => UserRole.Teacher));

           
            CreateMap<Report, ReportDto>();
            CreateMap<CreateReportDto, Report>()
                .ForMember(dest => dest.File, opt => opt.Ignore())
                .ForMember(dest => dest.StoredFileName, opt => opt.Ignore())
                .ForMember(dest => dest.IsAccepted, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore());
        }
    }
}
