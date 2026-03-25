namespace Framework.Common.UI.Utils.Mapper.Profiles
{
	using System.Linq;

	using AutoMapper;

	using Framework.Common.UI.Products.Shared.Enums.Document;
	using Framework.Common.UI.Products.WestLawNext.Models.BusinessObjects;
	using Framework.Common.UI.Products.WestLawNext.Pages.Document;

	/// <summary>
	/// Patent Document Map Profile
	/// </summary>
	public class PatentDocumentMapProfile : Profile
	{
		/// <summary>
		/// Patent Document Map Profile
		/// </summary>
		public PatentDocumentMapProfile()
		{
			this.CreateMap<PatentDocumentPage, PatentDocumentModel>()
			    .ForMember(dst => dst.Title, opt => opt.MapFrom(source => source.Title))
			    .ForMember(
				    dst => dst.Abstract,
				    opt => opt.MapFrom(src => src.GetPatentDocumentSectionTextList(PatentDocumentSection.PatentAbstract).FirstOrDefault()))
			    .ForMember(
				    dst => dst.Citation,
				    opt => opt.MapFrom(src => src.GetPatentDocumentSectionTextList(PatentDocumentSection.PatentCitation).FirstOrDefault()))
			    .ForMember(
				    dst => dst.Title,
				    opt => opt.MapFrom(src => src.GetPatentDocumentSectionTextList(PatentDocumentSection.PatentHeaderTitle).FirstOrDefault()))
				.ForPath(
				    dst => dst.ReferencesCited.PatentsAndApplications,
				    opt => opt.MapFrom(src => src.GetPatentDocumentSectionTextList(PatentDocumentSection.PatentsAndApplications)))
				.ForPath(
				    dst => dst.Claims.ClaimList,
				    opt => opt.MapFrom(src => src.GetPatentDocumentSectionTextList(PatentDocumentSection.PatentClaimList)))
			    .ForPath(
				    dst => dst.PriorityInformationSection.ErliestPriorityDate,
				    opt => opt.MapFrom(src => src.GetPatentDocumentSectionTextList(PatentDocumentSection.PatentErliestPriorityDate).FirstOrDefault()))
			    .ForPath(
				    dst => dst.PriorityInformationSection.PctApplications,
				    opt => opt.MapFrom(src => src.GetPatentDocumentSectionTextList(PatentDocumentSection.PatentPctApplications).FirstOrDefault()))
			    .ForPath(
				    dst => dst.PriorityInformationSection.PriorityPublications,
				    opt => opt.MapFrom(src => src.GetPatentDocumentSectionTextList(PatentDocumentSection.PatentPriorityPublications)))
			    .ForPath(
				    dst => dst.PriorityInformationSection.PctPublications,
				    opt => opt.MapFrom(src => src.GetPatentDocumentSectionTextList(PatentDocumentSection.PatentPctPublications).FirstOrDefault()))
				.ForPath(
				    dst => dst.Claims.NumberOfClaims,
				    opt => opt.MapFrom(src => src.GetPatentDocumentSectionTextList(PatentDocumentSection.PatentNumberOfClaims).FirstOrDefault()))
				.ForPath(
				    dst => dst.PatentSection.Inventors,
				    opt => opt.MapFrom(src => src.GetPatentDocumentSectionTextList(PatentDocumentSection.PatentInventors)))
                .ForPath(
                    dst => dst.PatentSection.Examiners,
                    opt => opt.MapFrom(src => src.GetPatentDocumentSectionTextList(PatentDocumentSection.PatentExaminer)))
                .ForPath(
				    dst => dst.PatentSection.Title,
				    opt => opt.MapFrom(src => src.GetPatentDocumentSectionTextList(PatentDocumentSection.PatentTitle).FirstOrDefault()))
			    .ForPath(
				    dst => dst.PatentSection.Assignees,
				    opt => opt.MapFrom(src => src.GetPatentDocumentSectionTextList(PatentDocumentSection.PatentAssignees)))
			    .ForPath(
				    dst => dst.PatentSection.AttorneyOrAgents,
				    opt => opt.MapFrom(src => src.GetPatentDocumentSectionTextList(PatentDocumentSection.PatentAttorneyOrAgents)))
			    .ForPath(
				    dst => dst.PatentSection.PublishedApplicationNumber,
				    opt => opt.MapFrom(src => src.GetPatentDocumentSectionTextList(PatentDocumentSection.PatentPublishedApplicationNumber).FirstOrDefault()))
			    .ForPath(
			        dst => dst.PatentSection.PublishedApplicationDate,
			        opt => opt.MapFrom(src => src.GetPatentDocumentSectionTextList(PatentDocumentSection.PatentPublishedApplicationDate).FirstOrDefault()))
                .ForPath(
				    dst => dst.PatentSection.ApplicationDate,
				    opt => opt.MapFrom(src => src.GetPatentDocumentSectionTextList(PatentDocumentSection.PatentApplicationDate).FirstOrDefault()))
			    .ForPath(
				    dst => dst.PatentSection.GrantedDate,
				    opt => opt.MapFrom(src => src.GetPatentDocumentSectionTextList(PatentDocumentSection.PatentGrantedDate).FirstOrDefault()))
			    .ForPath(
				    dst => dst.PatentSection.GrantedPatentNumber,
				    opt => opt.MapFrom(src => src.GetPatentDocumentSectionTextList(PatentDocumentSection.PatentGrantedPatentNumber).FirstOrDefault()))
				.ForPath(
				    dst => dst.PatentSection.ApplicationNumber,
				    opt => opt.MapFrom(src => src.GetPatentDocumentSectionTextList(PatentDocumentSection.PatentApplicationNumber).FirstOrDefault()))
			    .ForPath(
				    dst => dst.ClassificationInformation.InternationalClassesIpc1To7,
				    opt => opt.MapFrom(src => src.GetPatentDocumentSectionTextList(PatentDocumentSection.PatentInternationalClassesIpc1To7).FirstOrDefault()))
                .ForPath(
				    dst => dst.ClassificationInformation.InternationalClassesIpc8,
				    opt => opt.MapFrom(src => src.GetPatentDocumentSectionTextList(PatentDocumentSection.PatentInternationalClassesIpc8).FirstOrDefault()))
			    .ForPath(
				    dst => dst.ClassificationInformation.Language,
				    opt => opt.MapFrom(src => src.GetPatentDocumentSectionTextList(PatentDocumentSection.PatentLanguage).FirstOrDefault()))
			    .ForPath(
				    dst => dst.ReferencesCited.PatentsAndApplications,
				    opt => opt.MapFrom(src => src.GetPatentDocumentSectionTextList(PatentDocumentSection.PatentsAndApplications)));
		}
	}
}
