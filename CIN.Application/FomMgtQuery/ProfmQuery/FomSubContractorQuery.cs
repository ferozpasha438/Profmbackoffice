using AutoMapper;
using CIN.Application;
using CIN.Application.Common;
using CIN.Application.FomMgtDtos;
//using CIN.Application.SalesSetupDtos;
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
using CIN.Domain.FomMgt;
using CIN.Domain.InventorySetup;

namespace CIN.Application.FomMgtQuery.ProfmQuery
{
    #region GetAll
    public class GetFomSubContactorList : IRequest<PaginatedList<TblErpFomSubContractorDto>>
    {
        public UserIdentityDto User { get; set; }
        public PaginationFilterDto Input { get; set; }

    }

    public class GetFomSubContactorListHandler : IRequestHandler<GetFomSubContactorList, PaginatedList<TblErpFomSubContractorDto>>
    {
        private readonly CINDBOneContext _context;
        // private readonly DBContext _context;
        private readonly IMapper _mapper;
        public GetFomSubContactorListHandler(CINDBOneContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<PaginatedList<TblErpFomSubContractorDto>> Handle(GetFomSubContactorList request, CancellationToken cancellationToken)
        {


            var list = await _context.ErpFomSubContractors.AsNoTracking().ProjectTo<TblErpFomSubContractorDto>(_mapper.ConfigurationProvider)
                                    .PaginationListAsync(request.Input.Page, request.Input.PageCount, cancellationToken);

            return list;
        }

          
    }


    #endregion

    #region GetById

    public class GetFomSubContactorById : IRequest<TblErpFomSubContractorDto>
    {
        public UserIdentityDto User { get; set; }
        public int Id { get; set; }
    }

    public class GetFomSubContactorByIdIdHandler : IRequestHandler<GetFomSubContactorById, TblErpFomSubContractorDto>
    {
        private readonly CINDBOneContext _context;
        // private readonly DBContext _context;
        private readonly IMapper _mapper;
        public GetFomSubContactorByIdIdHandler(CINDBOneContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TblErpFomSubContractorDto> Handle(GetFomSubContactorById request, CancellationToken cancellationToken)
        {

            TblErpFomSubContractor obj = new();
            var subContractor = await _context.ErpFomSubContractors.AsNoTracking().ProjectTo<TblErpFomSubContractorDto>(_mapper.ConfigurationProvider).FirstOrDefaultAsync(e => e.Id == request.Id);
            return subContractor;
            // throw new NotImplementedException();
        }
    }
    #endregion

    #region Create_And_Update

    public class CreateUpdateFomSubContractors : IRequest<int>
    {
        public UserIdentityDto User { get; set; }
        public ErpFomSubContractorDto SubContractorDto { get; set; }
    }

    public class CreateUpdateFomSubContractorsHandler : IRequestHandler<CreateUpdateFomSubContractors, int>
    {
        private readonly CINDBOneContext _context;
        private readonly IMapper _mapper;

        public CreateUpdateFomSubContractorsHandler(CINDBOneContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateUpdateFomSubContractors request, CancellationToken cancellationToken)
        {
            try
            {
                Log.Info("----Info Create Update  Fom Item Sub Category method start----");

                var obj = request.SubContractorDto;


                TblErpFomSubContractor FomSubContractor = new();
                if (obj.Id > 0)
                    FomSubContractor = await _context.ErpFomSubContractors.AsNoTracking().FirstOrDefaultAsync(e => e.Id == obj.Id);


                FomSubContractor.Id = obj.Id;
                FomSubContractor.SubContCode = obj.SubContCode;
                FomSubContractor.DeptCodes = string.Join(",", obj.DeptCodes);
                //FomSubContractor.DeptCodes = obj.DeptCodes;
                FomSubContractor.NameEng = obj.NameEng;
                FomSubContractor.NameArabic = obj.NameArabic;
                // FomItemSubCategory.ItemSubCatNameAr = obj.ItemSubCatNameAr;
                FomSubContractor.Address = obj.Address;
                FomSubContractor.City = obj.City;
                FomSubContractor.Phone = obj.Phone;
                FomSubContractor.Mobile = obj.Mobile;
                FomSubContractor.ContactPerson1 = obj.ContactPerson1;
                FomSubContractor.DesgContactPerson1 = obj.DesgContactPerson1;
                FomSubContractor.ContactPerson1Phone = obj.ContactPerson1Phone;
                FomSubContractor.ContactPerson2 = obj.ContactPerson2;
                FomSubContractor.DesgContactPerson2 = obj.DesgContactPerson2;
                FomSubContractor.ContactPerson2Phone = obj.ContactPerson2Phone;
                FomSubContractor.IsActive = obj.IsActive;
                FomSubContractor.Website = obj.Website;
               




                if (obj.Id > 0)
                {
                     FomSubContractor.CreatedDate = DateTime.Now;
                    _context.ErpFomSubContractors.Update(FomSubContractor);
                 //   ErpFomSubContractors.ModifiedOn = obj.CreatedOn;
                }
                else
                {

                    FomSubContractor.CreatedDate = DateTime.Now;
                    FomSubContractor.CreatedBy = obj.CreatedBy;
                    await _context.ErpFomSubContractors.AddAsync(FomSubContractor);
                }
                await _context.SaveChangesAsync();
                Log.Info("----Info Create Update Fom Sub Contractor Method Exit----");
                return FomSubContractor.Id;
            }
            catch (Exception ex)
            {
                Log.Error("Error in Create Update Fom Sub Contractor Method");
                Log.Error("Error occured time : " + DateTime.UtcNow);
                Log.Error("Error message : " + ex.Message);
                Log.Error("Error StackTrace : " + ex.StackTrace);
                return 0;
            }
        }


    }



    #endregion
}
