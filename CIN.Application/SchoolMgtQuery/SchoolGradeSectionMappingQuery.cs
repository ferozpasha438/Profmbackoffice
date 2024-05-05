using AutoMapper;
using CIN.Application;
using CIN.Application.Common;
using CIN.Application.SchoolMgtDto;
using CIN.DB;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using System.Linq.Dynamic.Core;
using CIN.Domain.SchoolMgt;
namespace CIN.Application.SchoolMgtQuery
{
    #region GetAllSectionsByGradeMapping 
    public class GetAllSectionsByGradeMapping : IRequest<List<TblSysSchoolGradeSectionMapping1Dto>>
    {
        public UserIdentityDto User { get; set; }
        public string GradeCode { get; set; }
    }

    public class GetAllSectionsByGradeMappingHandler : IRequestHandler<GetAllSectionsByGradeMapping, List<TblSysSchoolGradeSectionMapping1Dto>>
    {
        private readonly CINDBOneContext _context;
        private readonly IMapper _mapper;
        public GetAllSectionsByGradeMappingHandler(CINDBOneContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<TblSysSchoolGradeSectionMapping1Dto>> Handle(GetAllSectionsByGradeMapping request, CancellationToken cancellationToken)
        {

            var SectionsMapping = await _context.SchoolGradeSectionMapping.AsNoTracking().ProjectTo<TblSysSchoolGradeSectionMapping1Dto>(_mapper.ConfigurationProvider).Where(e => e.GradeCode == request.GradeCode).ToListAsync();

            return SectionsMapping;
        }
    }



    #endregion


    #region Create_And_Update


    public class CreateUpdateSectionsGradeMapping : IRequest<int>
    {
        public UserIdentityDto User { get; set; }
        public GradeSectionMappingDto SchoolGradeSectionMapDto { get; set; }
    }

    public class CreateUpdateSectionsGradeMappingHandler : IRequestHandler<CreateUpdateSectionsGradeMapping, int>
    {
        private readonly CINDBOneContext _context;
        private readonly IMapper _mapper;

        public CreateUpdateSectionsGradeMappingHandler(CINDBOneContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateUpdateSectionsGradeMapping request, CancellationToken cancellationToken)
        {
            try
            {
                Log.Info("----Info CreateUpdateSchoolGradeSectionMapping method start----");

                var obj = request.SchoolGradeSectionMapDto;
                var data = await _context.SchoolGradeSectionMapping.FirstOrDefaultAsync(e => e.GradeCode == obj.GradeCode && e.SectionCode == obj.SectionCode);
                if (data != null && string.IsNullOrEmpty(obj.UploadFileName))
                    obj.UploadFileName = data.FileName;
                TblSysSchoolGradeSectionMapping item = new()
                {
                    GradeCode = obj.GradeCode,
                    SectionCode = obj.SectionCode,
                    MaxStrength = obj.MaxStrength,
                    MinStrength = obj.MinStrength,
                    AvgStrength = obj.AvgStrength,
                    FileName = obj.UploadFileName
                };
                if (data != null)
                    _context.SchoolGradeSectionMapping.Remove(data);
                await _context.SchoolGradeSectionMapping.AddAsync(item);
                await _context.SaveChangesAsync();
                if (obj.Page == 0 && !string.IsNullOrEmpty(obj.SectionCodes))
                {
                    foreach (var itemData in obj.SectionCodes.Split(','))
                    {
                        var deleteData = await _context.SchoolGradeSectionMapping.FirstOrDefaultAsync(e => e.GradeCode == obj.GradeCode && e.SectionCode == itemData);
                        if (deleteData != null)
                        {
                            _context.SchoolGradeSectionMapping.Remove(deleteData);
                            await _context.SaveChangesAsync();
                        }
                    }
                }

                //TblSysSchoolGradeSectionMapping GradeSectionMapp = new();
                //if (request.SchoolGradeSectionMapDto.SchoolGradeSectionlist.Count > 0)
                //{
                //    List<TblSysSchoolGradeSectionMapping> listItems = new();
                //    foreach (var GradeSectiondetail in request.SchoolGradeSectionMapDto.SchoolGradeSectionlist)
                //    {


                //        listItems.Add(detail);
                //        //if (detail.Id > 0)
                //        //{
                //        //    _context.SchoolGradeSectionMapping.Update(detail);
                //        //}
                //        //else
                //        //{

                //        //    await _context.SchoolGradeSectionMapping.AddAsync(detail);
                //        //}

                //    }
                //    if (listItems.Count > 0)
                //    {
                //        var list = _context.SchoolGradeSectionMapping.Where(e => e.GradeCode == obj.GradeCode);
                //        if (await list.AnyAsync())
                //        {
                //            _context.RemoveRange(list);
                //        }

                //        await _context.SchoolGradeSectionMapping.AddRangeAsync(listItems);
                //        await _context.SaveChangesAsync();

                //        Log.Info("----Info CreateUpdateSchoolGradeSectionMapping  method Exit----");
                //        return 1;
                //    }
                //}
                return item.Id;
            }
            catch (Exception ex)
            {
                Log.Error("Error in CreateUpdateSchoolGradeSectionMapping  Method");
                Log.Error("Error occured time : " + DateTime.UtcNow);
                Log.Error("Error message : " + ex.Message);
                Log.Error("Error StackTrace : " + ex.StackTrace);
                return 0;
            }
        }


    }



    #endregion


    #region GetAllSectionsByGradeCode
    public class GetAllSectionsByGradeCode : IRequest<List<TblSysSchoolGradeSectionListDto>>
    {
        public UserIdentityDto User { get; set; }
        public string GradeCode { get; set; }
    }

    public class GetAllSectionsByGradeCodeHandler : IRequestHandler<GetAllSectionsByGradeCode, List<TblSysSchoolGradeSectionListDto>>
    {
        private readonly CINDBOneContext _context;
        private readonly IMapper _mapper;
        public GetAllSectionsByGradeCodeHandler(CINDBOneContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<TblSysSchoolGradeSectionListDto>> Handle(GetAllSectionsByGradeCode request, CancellationToken cancellationToken)
        {
            List<TblSysSchoolGradeSectionListDto> tblSysSchoolGradeSectionListDtos = new List<TblSysSchoolGradeSectionListDto>();
            var sectionsMapping = await _context.SchoolGradeSectionMapping.AsNoTracking().ProjectTo<TblSysSchoolGradeSectionMapping1Dto>(_mapper.ConfigurationProvider).Where(e => e.GradeCode == request.GradeCode).ToListAsync();
            if (sectionsMapping is not null)
            {
                List<string> _sectionCodeList = new List<string>();
                _sectionCodeList = sectionsMapping.Select(x => x.SectionCode).ToList();
                tblSysSchoolGradeSectionListDtos = await _context.SchoolSectionsSection.AsNoTracking().ProjectTo<TblSysSchoolSectionsSectionDto>(_mapper.ConfigurationProvider).
                      Where(x => _sectionCodeList.Contains(x.SectionCode)).
                      Select(x => new TblSysSchoolGradeSectionListDto
                      {
                          GradeCode = request.GradeCode,
                          SectionCode = x.SectionCode,
                          SectionName = x.SectionName,
                          SectionName2 = x.SectionName2
                      }).
                      ToListAsync();
            }
            return tblSysSchoolGradeSectionListDtos;
        }
    }



    #endregion

}