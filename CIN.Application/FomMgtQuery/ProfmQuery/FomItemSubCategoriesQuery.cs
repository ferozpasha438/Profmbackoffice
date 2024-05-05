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



namespace CIN.Application.FomMgtQuery
{
    #region GetAll
    public class GetFomItemSubCategoriesList : IRequest<PaginatedList<TblInvDefSubCategoryDto>>
    {
        public UserIdentityDto User { get; set; }
        public PaginationFilterDto Input { get; set; }

    }

    public class GetFomItemSubCategoriesListHandler : IRequestHandler<GetFomItemSubCategoriesList, PaginatedList<TblInvDefSubCategoryDto>>
    {
        private readonly CINDBOneContext _context;
        // private readonly DBContext _context;
        private readonly IMapper _mapper;
        public GetFomItemSubCategoriesListHandler(CINDBOneContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<PaginatedList<TblInvDefSubCategoryDto>> Handle(GetFomItemSubCategoriesList request, CancellationToken cancellationToken)
        {


            var list = await _context.InvSubCategories.AsNoTracking().ProjectTo<TblInvDefSubCategoryDto>(_mapper.ConfigurationProvider)
                                    .PaginationListAsync(request.Input.Page, request.Input.PageCount, cancellationToken);

            return list;
        }


    }


    #endregion

    #region GetById

    public class GetFomItemSubCategoryById : IRequest<TblInvDefSubCategoryDto>
    {
        public UserIdentityDto User { get; set; }
        public int Id { get; set; }
    }

    public class GetFomItemSubCategoryByIdHandler : IRequestHandler<GetFomItemSubCategoryById, TblInvDefSubCategoryDto>
    {
        private readonly CINDBOneContext _context;
        // private readonly DBContext _context;
        private readonly IMapper _mapper;
        public GetFomItemSubCategoryByIdHandler(CINDBOneContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TblInvDefSubCategoryDto> Handle(GetFomItemSubCategoryById request, CancellationToken cancellationToken)
        {

            TblInvDefSubCategory obj = new();
            var itemSubCategory = await _context.InvSubCategories.AsNoTracking().ProjectTo<TblInvDefSubCategoryDto>(_mapper.ConfigurationProvider).FirstOrDefaultAsync(e => e.Id == request.Id);
            return itemSubCategory;
            // throw new NotImplementedException();
        }
    }
    #endregion

    #region Create_And_Update

    public class CreateUpdateFomItemSubCategory : IRequest<int>
    {
        public UserIdentityDto User { get; set; }
        public InvDefSubCategoryDto FomItemSubCategoryDto { get; set; }
    }

    public class CreateUpdateFomItemSubCategoryHandler : IRequestHandler<CreateUpdateFomItemSubCategory, int>
    {
        private readonly CINDBOneContext _context;
        private readonly IMapper _mapper;

        public CreateUpdateFomItemSubCategoryHandler(CINDBOneContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateUpdateFomItemSubCategory request, CancellationToken cancellationToken)
        {
            try
            {
                Log.Info("----Info Create Update  Fom Item Sub Category method start----");

                var obj = request.FomItemSubCategoryDto;


                TblInvDefSubCategory FomItemSubCategory = new();
                if (obj.Id > 0)
                    FomItemSubCategory = await _context.InvSubCategories.AsNoTracking().FirstOrDefaultAsync(e => e.Id == obj.Id);


                FomItemSubCategory.Id = obj.Id;
                FomItemSubCategory.ItemCatCode = obj.ItemCatCode;
                FomItemSubCategory.SubCatKey = obj.SubCatKey;
                FomItemSubCategory.ItemSubCatCode = obj.ItemSubCatCode;
                FomItemSubCategory.ItemSubCatDesc = obj.ItemSubCatDesc;
                FomItemSubCategory.ItemSubCatName = obj.ItemSubCatName;
                FomItemSubCategory.ItemSubCatNameAr = obj.ItemSubCatNameAr;
                FomItemSubCategory.IsActive = obj.IsActive;




                if (obj.Id > 0)
                {
                    FomItemSubCategory.CreatedOn = DateTime.Now;
                    _context.InvSubCategories.Update(FomItemSubCategory);
                    
                }
                else
                {
                   
                    FomItemSubCategory.CreatedOn = DateTime.Now;
                    await _context.InvSubCategories.AddAsync(FomItemSubCategory);
                }
                await _context.SaveChangesAsync();
                Log.Info("----Info Create Update Fom Item Sub Category Method Exit----");
                return FomItemSubCategory.Id;
            }
            catch (Exception ex)
            {
                Log.Error("Error in Create Update Fom Item Sub Category  Method");
                Log.Error("Error occured time : " + DateTime.UtcNow);
                Log.Error("Error message : " + ex.Message);
                Log.Error("Error StackTrace : " + ex.StackTrace);
                return 0;
            }
        }


    }



    #endregion
}
