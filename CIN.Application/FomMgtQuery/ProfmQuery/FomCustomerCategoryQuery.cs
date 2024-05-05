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
using CIN.Domain.SalesSetup;

namespace CIN.Application.FomMgtQuery
{
    #region GetAll
    public class GetFomCustomerCategoriesList : IRequest<PaginatedList<TblSndDefCustomerCategoryDto>>
    {
        public UserIdentityDto User { get; set; }
        public PaginationFilterDto Input { get; set; }

    }

    public class GetFomCustomerCategoriesListHandler : IRequestHandler<GetFomCustomerCategoriesList, PaginatedList<TblSndDefCustomerCategoryDto>>
    {
        private readonly CINDBOneContext _context;
        // private readonly DBContext _context;
        private readonly IMapper _mapper;
        public GetFomCustomerCategoriesListHandler(CINDBOneContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<PaginatedList<TblSndDefCustomerCategoryDto>> Handle(GetFomCustomerCategoriesList request, CancellationToken cancellationToken)
        {


            var list = await _context.SndCustomerCategories.AsNoTracking().ProjectTo<TblSndDefCustomerCategoryDto>(_mapper.ConfigurationProvider)
                       .PaginationListAsync(request.Input.Page, request.Input.PageCount, cancellationToken);


            return list;
        }


    }


    #endregion

    #region GetById

    public class GetFomCustomerCategoriesById : IRequest<TblSndDefCustomerCategoryDto>
    {
        public UserIdentityDto User { get; set; }
        public int Id { get; set; }
    }

    public class GetFomCustomerCategoriesByIdHandler : IRequestHandler<GetFomCustomerCategoriesById, TblSndDefCustomerCategoryDto>
    {
        private readonly CINDBOneContext _context;
        // private readonly DBContext _context;
        private readonly IMapper _mapper;
        public GetFomCustomerCategoriesByIdHandler(CINDBOneContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TblSndDefCustomerCategoryDto> Handle(GetFomCustomerCategoriesById request, CancellationToken cancellationToken)
        {

            TblSndDefCustomerCategory obj = new();
            var customerCategory = await _context.SndCustomerCategories.AsNoTracking().ProjectTo<TblSndDefCustomerCategoryDto>(_mapper.ConfigurationProvider).FirstOrDefaultAsync(e => e.Id == request.Id);
            return customerCategory;
            // throw new NotImplementedException();
        }
    }
    #endregion

    #region Create_And_Update

    public class CreateUpdateFomCustomerCategory : IRequest<int>
    {
        public UserIdentityDto User { get; set; }
        public TblSndDefCustomerCategoryDto CustomerCategoryDto { get; set; }
    }

    public class CreateUpdateFomCustomerCategoryHandler : IRequestHandler<CreateUpdateFomCustomerCategory, int>
    {
        private readonly CINDBOneContext _context;
        private readonly IMapper _mapper;

        public CreateUpdateFomCustomerCategoryHandler(CINDBOneContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateUpdateFomCustomerCategory request, CancellationToken cancellationToken)
        {
            try
            {
                Log.Info("----Info Create Update  Fom Item Category method start----");

                var obj = request.CustomerCategoryDto;


                TblSndDefCustomerCategory CustomerCategory = new();
                if (obj.Id > 0)
                    CustomerCategory = await _context.SndCustomerCategories.AsNoTracking().FirstOrDefaultAsync(e => e.Id == obj.Id);


                CustomerCategory.Id = obj.Id;
                CustomerCategory.CustCatCode = obj.CustCatCode;
                CustomerCategory.CustCatDesc = obj.CustCatDesc;
                CustomerCategory.CustCatName = obj.CustCatName;
                CustomerCategory.CatPrefix = obj.CatPrefix;
                CustomerCategory.LastSeq = obj.LastSeq;




                if (obj.Id > 0)
                {

                    _context.SndCustomerCategories.Update(CustomerCategory);
                    CustomerCategory.ModifiedOn = obj.ModifiedOn;
                }
                else
                {

                    await _context.SndCustomerCategories.AddAsync(CustomerCategory);
                    CustomerCategory.CreatedOn = obj.CreatedOn;
                    CustomerCategory.IsActive = obj.IsActive;
                }
                await _context.SaveChangesAsync();
                Log.Info("----Info Create Update Fom Item Category method Exit----");
                return CustomerCategory.Id;
            }
            catch (Exception ex)
            {
                Log.Error("Error in Create Update Fom Item Master Method");
                Log.Error("Error occured time : " + DateTime.UtcNow);
                Log.Error("Error message : " + ex.Message);
                Log.Error("Error StackTrace : " + ex.StackTrace);
                return 0;
            }
        }


    }



    #endregion
}
