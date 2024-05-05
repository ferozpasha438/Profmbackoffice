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
    public class GetFomItemCategoriesList : IRequest<PaginatedList<TblInvDefCategoryDto>>
    {
        public UserIdentityDto User { get; set; }
        public PaginationFilterDto Input { get; set; }

    }

    public class GetFomItemCategoriesListHandler : IRequestHandler<GetFomItemCategoriesList, PaginatedList<TblInvDefCategoryDto>>
    {
        private readonly CINDBOneContext _context;
        // private readonly DBContext _context;
        private readonly IMapper _mapper;
        public GetFomItemCategoriesListHandler(CINDBOneContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<PaginatedList<TblInvDefCategoryDto>> Handle(GetFomItemCategoriesList request, CancellationToken cancellationToken)
        {


            var list = await _context.InvCategories.AsNoTracking().ProjectTo<TblInvDefCategoryDto>(_mapper.ConfigurationProvider)
                                    .PaginationListAsync(request.Input.Page, request.Input.PageCount, cancellationToken);

            return list;
        }


    }


    #endregion

    #region GetById

    public class GetFomItemCategoryById : IRequest<TblInvDefCategoryDto>
    {
        public UserIdentityDto User { get; set; }
        public int Id { get; set; }
    }

    public class GetFomItemCategoryByIdHandler : IRequestHandler<GetFomItemCategoryById, TblInvDefCategoryDto>
    {
        private readonly CINDBOneContext _context;
        // private readonly DBContext _context;
        private readonly IMapper _mapper;
        public GetFomItemCategoryByIdHandler(CINDBOneContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TblInvDefCategoryDto> Handle(GetFomItemCategoryById request, CancellationToken cancellationToken)
        {

            TblInvDefCategory obj = new();
            var itemMaster = await _context.InvCategories.AsNoTracking().ProjectTo<TblInvDefCategoryDto>(_mapper.ConfigurationProvider).FirstOrDefaultAsync(e => e.Id == request.Id);
            return itemMaster;
            // throw new NotImplementedException();
        }
    }
    #endregion

    #region Create_And_Update

    public class CreateUpdateFomItemCategory : IRequest<int>
    {
        public UserIdentityDto User { get; set; }
        public InvDefCategoryDto FomItemCategoryDto { get; set; }
    }

    public class CreateUpdateFomItemCategoryHandler : IRequestHandler<CreateUpdateFomItemCategory, int>
    {
        private readonly CINDBOneContext _context;
        private readonly IMapper _mapper;

        public CreateUpdateFomItemCategoryHandler(CINDBOneContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateUpdateFomItemCategory request, CancellationToken cancellationToken)
        {
            try
            {
                Log.Info("----Info Create Update  Fom Item Category method start----");

                var obj = request.FomItemCategoryDto;


                TblInvDefCategory FomItemCategory = new();
                if (obj.Id > 0)
                    FomItemCategory = await _context.InvCategories.AsNoTracking().FirstOrDefaultAsync(e => e.Id == obj.Id);


                FomItemCategory.Id = obj.Id;
                FomItemCategory.ItemCatCode = obj.ItemCatCode;
                FomItemCategory.ItemCatDesc = obj.ItemCatDesc;
                FomItemCategory.ItemCatName = obj.ItemCatName;
                FomItemCategory.ItemCatName_Ar = obj.ItemCatName_Ar;
                FomItemCategory.ItemCatPrefix = obj.ItemCatPrefix;
               
                



                if (obj.Id > 0)
                {
                    FomItemCategory.IsActive = obj.IsActive;
                    FomItemCategory.ModifiedOn = DateTime.Now;
                    _context.InvCategories.Update(FomItemCategory);
                   
                }
                else
                {
                    FomItemCategory.IsActive = obj.IsActive;
                    FomItemCategory.CreatedOn = DateTime.Now;
                    await _context.InvCategories.AddAsync(FomItemCategory);
                }
                await _context.SaveChangesAsync();
                Log.Info("----Info Create Update Fom Item Category method Exit----");
                return FomItemCategory.Id;
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
