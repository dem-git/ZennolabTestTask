using AutoMapper;
using CaptchaApp.Server.Models;
using CaptchaApp.Shared.Models;

namespace CaptchaApp.Server.Configuration.MapperProfiles
{
    /// <summary>
    /// Профиль маппинга наборов данных распознавания.
    /// </summary>
    public class CaptchaDataSetProfile : Profile
    {
        public CaptchaDataSetProfile()
        {
            CreateMap<CaptchaDataSetPostModel, CaptchaDataSet>()
                .ForMember(m => m.ImagesZipFilename, o=> o.Ignore());

            CreateMap<CaptchaDataSet, CaptchaDataSetViewModel>();

            CreateMap<AnswerPlacementModel, AnswerPlacementEnum>().ReverseMap();
        }
    }
}
