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
    public class GetFomItemMasterList : IRequest<PaginatedList<TblErpInvItemMasterDto>>
    {
        public UserIdentityDto User { get; set; }
        public PaginationFilterDto Input { get; set; }

    }

    public class GetFomItemMasterListHandler : IRequestHandler<GetFomItemMasterList, PaginatedList<TblErpInvItemMasterDto>>
    {
        private readonly CINDBOneContext _context;
        // private readonly DBContext _context;
        private readonly IMapper _mapper;
        public GetFomItemMasterListHandler(CINDBOneContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<PaginatedList<TblErpInvItemMasterDto>> Handle(GetFomItemMasterList request, CancellationToken cancellationToken)
        {


            var list = await _context.InvItemMaster.AsNoTracking().ProjectTo<TblErpInvItemMasterDto>(_mapper.ConfigurationProvider)
                                    .PaginationListAsync(request.Input.Page, request.Input.PageCount, cancellationToken);

            return list;
        }


    }


    #endregion

    #region GetById

    public class GetFomItemMasterById : IRequest<TblErpInvItemMasterDto>
    {
        public UserIdentityDto User { get; set; }
        public int Id { get; set; }
    }

    public class GetFomItemMasterByIdHandler : IRequestHandler<GetFomItemMasterById, TblErpInvItemMasterDto>
    {
        private readonly CINDBOneContext _context;
        // private readonly DBContext _context;
        private readonly IMapper _mapper;
        public GetFomItemMasterByIdHandler(CINDBOneContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TblErpInvItemMasterDto> Handle(GetFomItemMasterById request, CancellationToken cancellationToken)
        {

            TblErpInvItemMaster obj = new();
            var itemMaster = await _context.InvItemMaster.AsNoTracking().ProjectTo<TblErpInvItemMasterDto>(_mapper.ConfigurationProvider).FirstOrDefaultAsync(e => e.Id == request.Id);
            return itemMaster;
            // throw new NotImplementedException();
        }
    }
    #endregion

    #region Create_And_Update

    public class CreateUpdateFomItemMaster : IRequest<int>
    {
        public UserIdentityDto User { get; set; }
        public ErpInvItemMasterDto FomItemMasterDto { get; set; }
    }

    public class CreateUpdateFomItemMasterHandler : IRequestHandler<CreateUpdateFomItemMaster, int>
    {
        private readonly CINDBOneContext _context;
        private readonly IMapper _mapper;

        public CreateUpdateFomItemMasterHandler(CINDBOneContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateUpdateFomItemMaster request, CancellationToken cancellationToken)
        {
            try
            {
                Log.Info("----Info Create Update Fom Item Master method start----");

                var obj = request.FomItemMasterDto;


                TblErpInvItemMaster FomItemMaster = new();
                if (obj.Id > 0)
                    FomItemMaster = await _context.InvItemMaster.AsNoTracking().FirstOrDefaultAsync(e => e.Id == obj.Id);


                FomItemMaster.Id = obj.Id;
                FomItemMaster.ItemCode = obj.ItemCode;
                FomItemMaster.HSNCode = obj.HSNCode;
                FomItemMaster.ItemDescription = obj.ItemDescription;
                FomItemMaster.ItemDescriptionAr = obj.ItemDescriptionAr;
                FomItemMaster.ItemPrimeVendor = obj.ItemPrimeVendor;
                FomItemMaster.ItemQty = 1.0m; 
                FomItemMaster.DeptCodes = string.Join(",", obj.DeptCodes);
                FomItemMaster.ItemStandardCost = obj.ItemStandardCost;
                FomItemMaster.ItemStandardPrice1 = obj.ItemStandardPrice1;
                FomItemMaster.ItemStandardPrice2 = obj.ItemStandardPrice2;
                FomItemMaster.ItemStandardPrice3 = obj.ItemStandardPrice3;
                FomItemMaster.ItemCat = obj.ItemCat;
                FomItemMaster.ItemSubCat = obj.ItemSubCat;
                FomItemMaster.ItemSubClass = obj.ItemSubClass;
                FomItemMaster.ItemTaxCode = obj.ItemTaxCode;
                FomItemMaster.ItemTracking = obj.ItemTracking;
                FomItemMaster.ItemType = obj.ItemType;
                FomItemMaster.ItemWeight = obj.ItemWeight;
                FomItemMaster.ShortName = obj.ShortName;
                FomItemMaster.ShortNameAr = obj.ShortNameAr;
                FomItemMaster.ItemAvgCost = obj.ItemAvgCost;
                FomItemMaster.ItemBaseUnit = obj.ItemBaseUnit;
                FomItemMaster.ItemClass = obj.ItemClass;
                FomItemMaster.IsActive = obj.IsActive;
                FomItemMaster.AllowDiscounts = obj.AllowDiscounts;
                FomItemMaster.AllowPriceOverride = obj.AllowPriceOverride;
                FomItemMaster.AllowQuantityOverride = obj.AllowQuantityOverride;
                


                if (obj.Id > 0)
                {

                    _context.InvItemMaster.Update(FomItemMaster);
                }
                else
                {

                    await _context.InvItemMaster.AddAsync(FomItemMaster);
                }
                await _context.SaveChangesAsync();
                Log.Info("----Info Create Update Fom Item Master method Exit----");
                return FomItemMaster.Id;
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


    #region Get UOMList

    public class GetUOMList : IRequest<List<CustomSelectListItem>>
    {
        public UserIdentityDto User { get; set; }
        
    }

    public class GetUOMListHandler : IRequestHandler<GetUOMList, List<CustomSelectListItem>>
    {
        private readonly CINDBOneContext _context;
        private readonly IMapper _mapper;
        public GetUOMListHandler(CINDBOneContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<CustomSelectListItem>> Handle(GetUOMList request, CancellationToken cancellationToken)
        {
            Log.Info("----Info GetUOMList method start----");
            var item = await _context.InvUoms.AsNoTracking()
                .OrderByDescending(e => e.Id)
               .Select(e => new CustomSelectListItem { Text = e.UOMName, Value = e.UOMCode })
                  .ToListAsync(cancellationToken);
            Log.Info("----Info GetUOMList method Ends----");
            return item;
        }
    }

    #endregion

    #region GetTaxList

    public class GetTaxList : IRequest<List<CustomSelectListItem>>
    {
        public UserIdentityDto User { get; set; }
       
    }

    public class GetTaxListHandler : IRequestHandler<GetTaxList, List<CustomSelectListItem>>
    {
        private readonly CINDBOneContext _context;
        private readonly IMapper _mapper;
        public GetTaxListHandler(CINDBOneContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<CustomSelectListItem>> Handle(GetTaxList request, CancellationToken cancellationToken)
        {
            Log.Info("----Info GetTaxList method start----");
            var item = await _context.SystemTaxes.AsNoTracking()
                //.Where(e => e.BranchName.Contains(request.Input) || e.BranchCode.Contains(request.Input))
                .OrderByDescending(e => e.Id)
               .Select(e => new CustomSelectListItem { Text = e.TaxName, Value = e.TaxCode })
                  .ToListAsync(cancellationToken);
            Log.Info("----Info GetTaxList method Ends----");
            return item;
        }
    }

    #endregion

    #region GetSelectClassList

    public class GetSelectClass : IRequest<List<CustomSelectListItem>>
    {
        public UserIdentityDto User { get; set; }
       
    }

    public class GetSelectClassListHandler : IRequestHandler<GetSelectClass, List<CustomSelectListItem>>
    {
        private readonly CINDBOneContext _context;
        private readonly IMapper _mapper;
        public GetSelectClassListHandler(CINDBOneContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<CustomSelectListItem>> Handle(GetSelectClass request, CancellationToken cancellationToken)
        {
            Log.Info("----Info GetSelectClass method start----");
            var item = await _context.InvClasses.AsNoTracking()
                //.Where(e => e.BranchName.Contains(request.Input) || e.BranchCode.Contains(request.Input))
                .OrderByDescending(e => e.Id)
               .Select(e => new CustomSelectListItem { Text = e.ItemClassName, Value = e.ItemClassCode })
                  .ToListAsync(cancellationToken);
            Log.Info("----Info GetSelectClass method Ends----");
            return item;
        }
    }

    #endregion

    #region GetSelectSubClassList

    public class getSelectSubClassList : IRequest<List<CustomSelectListItem>>
    {
        public UserIdentityDto User { get; set; }
        
    }

    public class GetSelectSubClassListHandler : IRequestHandler<getSelectSubClassList, List<CustomSelectListItem>>
    {
        private readonly CINDBOneContext _context;
        private readonly IMapper _mapper;
        public GetSelectSubClassListHandler(CINDBOneContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<CustomSelectListItem>> Handle(getSelectSubClassList request, CancellationToken cancellationToken)
        {
            Log.Info("----Info getSelectSubClassList method start----");
            var item = await _context.InvSubClasses.AsNoTracking()
                .OrderByDescending(e => e.Id)
               .Select(e => new CustomSelectListItem { Text = e.ItemSubClassName, Value = e.ItemSubClassCode })
                  .ToListAsync(cancellationToken);
            Log.Info("----Info getSelectSubClassList method Ends----");
            return item;
        }
    }

    #endregion

    #region GetSelectSubCategoryList

    public class GetSelectSubCategoryList : IRequest<List<CustomSelectListItem>>
    {
        public UserIdentityDto User { get; set; }
        public string Code { get; set; }


    }

    public class GetSelectSubCategoryListHandler : IRequestHandler<GetSelectSubCategoryList, List<CustomSelectListItem>>
    {
        private readonly CINDBOneContext _context;
        private readonly IMapper _mapper;
        public GetSelectSubCategoryListHandler(CINDBOneContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<CustomSelectListItem>> Handle(GetSelectSubCategoryList request, CancellationToken cancellationToken)
        {
            //Log.Info("----Info GetSelectSubCategoryList method start----");
            //var item = await _context.InvSubCategories.AsNoTracking()
            //    .OrderByDescending(e => e.Id)
            //   .Select(e => new CustomSelectListItem { Text = e.ItemSubCatName, Value = e.ItemSubCatCode })
            //      .ToListAsync(cancellationToken);
            //Log.Info("----Info GetSelectSubCategoryList method Ends----");
            //return item;

            Log.Info("----Info GetSelectSubCategoryList method start----");
            var obj = _context.InvSubCategories.AsNoTracking();

            obj = obj.Where(e => e.ItemCatCode.Contains(request.Code));

            var newObj = await obj
               .OrderByDescending(e => e.Id)
               .Select(e => new CustomSelectListItem { Text = e.ItemSubCatName, Value = e.ItemSubCatCode })
                  .ToListAsync(cancellationToken);

            Log.Info("----Info GetSelectSubCategoryList method Ends----");
            return newObj;
        }
    }

    #endregion



    #region GetSelectCategoryList

    public class GetSelectCategoryList : IRequest<List<CustomSelectListItem>>
    {
        public UserIdentityDto User { get; set; }


    }

    public class GetSelectCategoryListHandler : IRequestHandler<GetSelectCategoryList, List<CustomSelectListItem>>
    {
        private readonly CINDBOneContext _context;
        private readonly IMapper _mapper;
        public GetSelectCategoryListHandler(CINDBOneContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<CustomSelectListItem>> Handle(GetSelectCategoryList request, CancellationToken cancellationToken)
        {
            Log.Info("----Info GetSelectSubCategoryList method start----");
            var item = await _context.InvCategories.AsNoTracking()
                .OrderByDescending(e => e.Id)
               .Select(e => new CustomSelectListItem { Text = e.ItemCatName, Value = e.ItemCatCode })
                  .ToListAsync(cancellationToken);
            Log.Info("----Info GetSelectSubCategoryList method Ends----");
            return item;

            //Log.Info("----Info GetSelectSubCategoryList method start----");
            //var obj = _context.InvSubCategories.AsNoTracking();

            //obj = obj.Where(e => e.ItemCatCode.Contains(request.Code));

            //var newObj = await obj
            //   .OrderByDescending(e => e.Id)
            //   .Select(e => new CustomSelectListItem { Text = e.ItemSubCatName, Value = e.ItemSubCatCode })
            //      .ToListAsync(cancellationToken);

            //Log.Info("----Info GetSelectSubCategoryList method Ends----");
            //return newObj;
        }
    }

    #endregion






    #region Delete
    public class DeleteFomItemMaster : IRequest<int>
    {
        public UserIdentityDto User { get; set; }
        public int Id { get; set; }
    }

    public class DeleteFomItemMasterHandler : IRequestHandler<DeleteFomItemMaster, int>
    {

        private readonly CINDBOneContext _context;
        private readonly IMapper _mapper;

        public DeleteFomItemMasterHandler(CINDBOneContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> Handle(DeleteFomItemMaster request, CancellationToken cancellationToken)
        {
            try
            {
                Log.Info("----Info Delete Fom Branch Start----");

                if (request.Id > 0)
                {
                    var itemMaster = await _context.InvItemMaster.FirstOrDefaultAsync(e => e.Id == request.Id);
                    _context.Remove(itemMaster);

                    await _context.SaveChangesAsync();

                    return request.Id;
                }
                return 0;
            }
            catch (Exception ex)
            {

                Log.Error("Error in Delete fom Company Branch");
                Log.Error("Error occured time : " + DateTime.UtcNow);
                Log.Error("Error message : " + ex.Message);
                Log.Error("Error StackTrace : " + ex.StackTrace);
                return 0;
            }

        }
    }

    #endregion
}
