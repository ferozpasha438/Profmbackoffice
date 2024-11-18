using AutoMapper;
using AutoMapper.QueryableExtensions;
using CIN.Application.Common;
using CIN.Application.FomMgtDtos;
using CIN.Application.FomMobDtos;
using CIN.Application.FomMobQuery;
using CIN.DB;
using CIN.Domain.FomMgt;
using CIN.Domain.FomMob;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CIN.Application.FomMobB2CQuery
{

    #region GetAllActiveFomServicePeriodsForB2C

    public class GetAllActiveFomServicePeriodsForB2C : IRequest<List<TblErpFomPeriodDto>>
    {
        public UserIdentityDto User { get; set; }
        public string ServiceCode { get; set; }
        public string ImagesUrl { get; set; }
    }

    public class GetAllActiveFomServicePeriodsForB2CHandler : IRequestHandler<GetAllActiveFomServicePeriodsForB2C, List<TblErpFomPeriodDto>>
    {
        private readonly CINDBOneContext _context;
        private readonly IMapper _mapper;
        public GetAllActiveFomServicePeriodsForB2CHandler(CINDBOneContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<TblErpFomPeriodDto>> Handle(GetAllActiveFomServicePeriodsForB2C request, CancellationToken cancellationToken)
        {
            var list = await _context.FomPeriod.AsNoTracking().Where(e => e.IsActive)
                .ProjectTo<TblErpFomPeriodDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
            foreach (var item in list)
            {
                item.ImagePath = $"{request.ImagesUrl}{item.ImagePath}";
            }
            return list;
        }
    }

    #endregion


    #region GetDepartmentListQuery

    public class GetDepartmentListQuery : IRequest<List<TblErpFomDepartmentDto>>
    {
        public UserIdentityDto User { get; set; }
    }

    public class GetDepartmentListQueryHandler : IRequestHandler<GetDepartmentListQuery, List<TblErpFomDepartmentDto>>
    {
        private readonly CINDBOneContext _context;
        private readonly IMapper _mapper;
        public GetDepartmentListQueryHandler(CINDBOneContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<TblErpFomDepartmentDto>> Handle(GetDepartmentListQuery request, CancellationToken cancellationToken)
        {
            var list = await _context.ErpFomDepartments.AsNoTracking()
                .Where(e => e.IsActive == true && e.IsSheduleRequired2 == true && e.ServiceTimePeriods != null && e.ServiceTimePeriods.Trim().Length > 0).Select(e => new TblErpFomDepartmentDto()
                {
                    Id = e.Id,
                    DeptCode = e.DeptCode,
                    DeptServType = e.DeptServType,
                    NameArabic = e.NameArabic,
                    NameEng = e.NameEng,
                    FullImagePath = e.FullImagePath,
                    IsSheduleRequired1 = e.IsSheduleRequired1,
                    IsSheduleRequired2 = e.IsSheduleRequired2,
                    ServiceTimePeriods = e.ServiceTimePeriods,
                    ThumbNailImage = e.ThumbNailImage
                }).ToListAsync();
            return list;
        }
    }
    #endregion



    #region GetActivitiesByDepartmentListQuery

    public class GetActivitiesByDepartmentListQuery : IRequest<List<TblErpFomActivitiesDto>>
    {
        public UserIdentityDto User { get; set; }
        public string DeptCode { get; set; }
    }

    public class GetActivitiesByDepartmentListQueryHandler : IRequestHandler<GetActivitiesByDepartmentListQuery, List<TblErpFomActivitiesDto>>
    {
        private readonly CINDBOneContext _context;
        private readonly IMapper _mapper;
        public GetActivitiesByDepartmentListQueryHandler(CINDBOneContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<TblErpFomActivitiesDto>> Handle(GetActivitiesByDepartmentListQuery request, CancellationToken cancellationToken)
        {
            var list = await _context.FomActivities.AsNoTracking().Where(e => e.IsB2C == true && e.DeptCode == request.DeptCode).Select(e => new TblErpFomActivitiesDto()
            {
                Id = e.Id,
                DeptCode = e.DeptCode,
                ActCode = e.ActCode,
                ActName = e.ActName,
                ActNameAr = e.ActNameAr,
                IsB2C = e.IsB2C,
                IsB2B = e.IsB2B,
            }).ToListAsync();
            return list;
        }
    }
    #endregion



    #region GetServiceItemsByActDeptListQuery

    public class GetServiceItemsByActDeptListQuery : IRequest<List<TblErpFomServiceItemsDto>>
    {
        public UserIdentityDto User { get; set; }
        public string DeptCode { get; set; }
        public string ActCode { get; set; }
    }

    public class GetServiceItemsByActDeptListQueryHandler : IRequestHandler<GetServiceItemsByActDeptListQuery, List<TblErpFomServiceItemsDto>>
    {
        private readonly CINDBOneContext _context;
        private readonly IMapper _mapper;
        public GetServiceItemsByActDeptListQueryHandler(CINDBOneContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<TblErpFomServiceItemsDto>> Handle(GetServiceItemsByActDeptListQuery request, CancellationToken cancellationToken)
        {
            var list = await _context.FomServiceItems.AsNoTracking().Where(e => e.ActivityCode == request.ActCode && e.DeptCode == request.DeptCode)
                .ProjectTo<TblErpFomServiceItemsDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
            return list;
        }
    }

    #endregion


    #region GetServiceDetailsByServiceCodeListQuery

    public class GetServiceDetailsByServiceCodeListQuery : IRequest<List<TblErpFomServiceItemsDetailsDto>>
    {
        public UserIdentityDto User { get; set; }
        public string ServiceCode { get; set; }
    }

    public class GetServiceDetailsByServiceCodeListQueryHandler : IRequestHandler<GetServiceDetailsByServiceCodeListQuery, List<TblErpFomServiceItemsDetailsDto>>
    {
        private readonly CINDBOneContext _context;
        private readonly IMapper _mapper;
        public GetServiceDetailsByServiceCodeListQueryHandler(CINDBOneContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<TblErpFomServiceItemsDetailsDto>> Handle(GetServiceDetailsByServiceCodeListQuery request, CancellationToken cancellationToken)
        {
            var list = await _context.FomServiceItemsDetails.AsNoTracking().Where(e => e.ServiceCode == request.ServiceCode)
                .ProjectTo<TblErpFomServiceItemsDetailsDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
            return list;
        }
    }

    #endregion


    #region GetServiceItemsListQuery

    public class GetServiceItemsListQuery : IRequest<PaginatedList<TblErpFomServiceItemsDto>>
    {
        public UserIdentityDto User { get; set; }
        public PaginationFilterDto Input { get; set; }
    }

    public class GetServiceItemsListQueryHandler : IRequestHandler<GetServiceItemsListQuery, PaginatedList<TblErpFomServiceItemsDto>>
    {
        private readonly CINDBOneContext _context;
        private readonly IMapper _mapper;
        public GetServiceItemsListQueryHandler(CINDBOneContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PaginatedList<TblErpFomServiceItemsDto>> Handle(GetServiceItemsListQuery request, CancellationToken cancellationToken)
        {
            var list = await _context.FomServiceItems.AsNoTracking()
                .Where(e => e.ServiceCode.Contains(request.Input.Query) || e.ActivityCode.Contains(request.Input.Query) || e.DeptCode.Contains(request.Input.Query))
                .OrderByDescending(e => e.Id)
                .ProjectTo<TblErpFomServiceItemsDto>(_mapper.ConfigurationProvider)
                .PaginationListAsync(request.Input.Page, request.Input.PageCount, cancellationToken);
            return list;
        }
    }

    #endregion


    #region GetAssignResourceSelectList

    public class GetAssignResourceSelectList : IRequest<List<CustomSelectListItem>>
    {
        public UserIdentityDto User { get; set; }
    }

    public class GetAssignResourceSelectListHandler : IRequestHandler<GetAssignResourceSelectList, List<CustomSelectListItem>>
    {
        private readonly CINDBOneContext _context;
        private readonly IMapper _mapper;
        public GetAssignResourceSelectListHandler(CINDBOneContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<CustomSelectListItem>> Handle(GetAssignResourceSelectList request, CancellationToken cancellationToken)
        {

            Log.Info("----Info GetAssignResourceSelectList method start----");
            var list = _context.FomResources.Where(e => e.IsActive);

            var newList = await list.AsNoTracking()
            .OrderBy(e => e.Id)
                 .Select(e => new CustomSelectListItem { Text = e.NameEng + " - " + e.NameAr, Value = e.ResCode })
                  .ToListAsync(cancellationToken);
            Log.Info("----Info GetAssignResourceSelectList method Ends----");
            return newList;
        }
    }


    #endregion


    #region GetServiceItemByIdQuery

    public class GetServiceItemByIdQuery : IRequest<TblErpFomServiceItemsDto>
    {
        public UserIdentityDto User { get; set; }
        public int Id { get; set; }
    }

    public class GetServiceItemByIdQueryHandler : IRequestHandler<GetServiceItemByIdQuery, TblErpFomServiceItemsDto>
    {
        private readonly CINDBOneContext _context;
        private readonly IMapper _mapper;
        public GetServiceItemByIdQueryHandler(CINDBOneContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<TblErpFomServiceItemsDto> Handle(GetServiceItemByIdQuery request, CancellationToken cancellationToken)
        {
            var serviceItem = await _context.FomServiceItems
                .Where(e => e.Id == request.Id)
                .ProjectTo<TblErpFomServiceItemsDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);

            var servicePrice = await _context.FomB2CDefaultPaymentPrices.Where(e => e.ServiceCode == serviceItem.ServiceCode).ToListAsync();
            serviceItem.MonthlyPrice = servicePrice.FirstOrDefault(e => e.PayType == "Month")?.Price ?? 0;
            serviceItem.IsMonthlyPrice = servicePrice.FirstOrDefault(e => e.PayType == "Month")?.Applicable ?? false;
            serviceItem.YearlyPrice = servicePrice.FirstOrDefault(e => e.PayType == "Year")?.Price ?? 0;
            serviceItem.IsYearlyPrice = servicePrice.FirstOrDefault(e => e.PayType == "Year")?.Applicable ?? false;


            return serviceItem;
        }
    }

    #endregion


    #region CreateUpdateServiceItemQuery

    public class CreateUpdateServiceItemQuery : IRequest<AppCtrollerDto>
    {
        public UserIdentityDto User { get; set; }
        public TblErpFomServiceItemsDto Input { get; set; }

    }

    public class CreateUpdateServiceItemQueryHandler : IRequestHandler<CreateUpdateServiceItemQuery, AppCtrollerDto>
    {
        private readonly CINDBOneContext _context;
        private readonly IMapper _mapper;
        public CreateUpdateServiceItemQueryHandler(CINDBOneContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<AppCtrollerDto> Handle(CreateUpdateServiceItemQuery request, CancellationToken cancellationToken)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    Log.Info("----Info CreateUpdateServiceItemQuery method start----");

                    var obj = request.Input;
                    TblErpFomServiceItems item = await _context.FomServiceItems.FirstOrDefaultAsync(e => e.Id == obj.Id) ?? new();

                    item.DeptCode = obj.DeptCode;
                    item.ActivityCode = obj.ActivityCode;
                    item.ServiceShortDesc = obj.ServiceShortDesc;
                    item.ServiceShortDescAr = obj.ServiceShortDescAr;
                    item.ServiceDetails = obj.ServiceDetails;
                    item.ServiceDetailsAr = obj.ServiceDetailsAr;
                    item.TimeUnitPrimary = obj.TimeUnitPrimary;
                    item.ResourceUnitPrimary = obj.ResourceUnitPrimary;
                    item.PotentialCost = obj.PotentialCost;
                    item.ApplicableDiscount = obj.ApplicableDiscount;
                    item.IsOnOffer = obj.IsOnOffer;
                    item.OfferPrice = obj.OfferPrice;
                    item.OfferStartDate = obj.OfferStartDate;
                    item.OfferEndDate = obj.OfferEndDate;
                    item.Remarks1 = obj.Remarks1;
                    item.Remarks2 = obj.Remarks2;
                    item.ThumbNailImagePath = obj.ThumbNailImagePath;
                    item.FullImagePath = obj.FullImagePath;
                    item.MinRequiredHrs = obj.MinRequiredHrs;
                    item.MinReqResource = obj.MinReqResource;
                    item.PrimaryUnitPrice = obj.PrimaryUnitPrice;
                    item.IsActive = obj.IsActive;

                    if (obj.SelectedServices is not null && obj.SelectedServices.Count > 0)
                    {
                        item.Serviceitems = string.Join(',', obj.SelectedServices);
                    }
                    else
                        item.Serviceitems = string.Empty;

                    if (obj.Id > 0)
                    {
                        _context.FomServiceItems.Update(item);
                    }
                    else
                    {
                        obj.ServiceCode = obj.ServiceCode.ToUpper();

                        item.ServiceCode = obj.ServiceCode;
                        await _context.FomServiceItems.AddAsync(item);
                    }
                    await _context.SaveChangesAsync();


                    #region Creating Service Items

                    obj.ServiceCode = obj.ServiceCode.ToUpper();

                    var servicePriceItems = _context.FomB2CDefaultPaymentPrices.Where(e => e.ServiceCode == obj.ServiceCode);
                    if (servicePriceItems.Any())
                    {
                        _context.FomB2CDefaultPaymentPrices.RemoveRange(servicePriceItems);
                        await _context.SaveChangesAsync();
                    }

                    bool IsOfService(string service) => item.Serviceitems.HasValue() && item.Serviceitems.ToLower().Contains(service);

                    List<TblFomB2CDefaultPaymentPrice> servicePriceList = new();
                    TblFomB2CDefaultPaymentPrice servicePrice = new()
                    {
                        ServiceCode = obj.ServiceCode,
                        Price = obj.PrimaryUnitPrice,
                        ApDiscount = obj.ApplicableDiscount,
                        Applicable = IsOfService("daily"),
                        OfferPrice = obj.OfferPrice,
                        PayType = "Day"
                    };
                    servicePriceList.Add(servicePrice);

                    servicePrice = new()
                    {
                        ServiceCode = obj.ServiceCode,
                        Price = obj.MonthlyPrice,
                        Applicable = IsOfService("month"),
                        PayType = "Month"
                    };
                    servicePriceList.Add(servicePrice);

                    servicePrice = new()
                    {
                        ServiceCode = obj.ServiceCode,
                        Price = obj.YearlyPrice,
                        Applicable = IsOfService("year"),
                        PayType = "Year"
                    };
                    servicePriceList.Add(servicePrice);

                    servicePriceList.Add(new() { PayType = "Adhoc", ServiceCode = obj.ServiceCode });

                    await _context.FomB2CDefaultPaymentPrices.AddRangeAsync(servicePriceList);
                    await _context.SaveChangesAsync();

                    ////if (item.Serviceitems.ToLower().Contains("month"))
                    ////{
                    ////    servicePrice.Price = obj.MonthlyPrice;
                    ////    servicePrice.Applicable = true;
                    ////    servicePrice.PayType = "Month";

                    ////    servicePriceList.Add(servicePrice);

                    ////    //await _context.FomB2CDefaultPaymentPrices.AddAsync(servicePrice);
                    ////    //await _context.SaveChangesAsync();

                    ////    //TblFomB2CDefaultPaymentPrice serviceMonthPrice = new()
                    ////    //{
                    ////    //    ServiceCode = obj.ServiceCode,
                    ////    //    Price = obj.MonthlyPrice,
                    ////    //    ApDiscount = obj.ApplicableDiscount,
                    ////    //    Applicable = true,
                    ////    //    OfferPrice = obj.OfferPrice,
                    ////    //    PayType = "Month"
                    ////    //};
                    ////}
                    ////if (item.Serviceitems.ToLower().Contains("year"))
                    ////{
                    ////    servicePrice.Price = obj.YearlyPrice;
                    ////    servicePrice.Applicable = true;
                    ////    servicePrice.PayType = "Year";

                    ////    servicePriceList.Add(servicePrice);

                    ////    //await _context.FomB2CDefaultPaymentPrices.AddAsync(servicePrice);
                    ////    //await _context.SaveChangesAsync();


                    ////    //TblFomB2CDefaultPaymentPrice serviceMonthPrice = new()
                    ////    //{
                    ////    //    ServiceCode = obj.ServiceCode,
                    ////    //    Price = obj.YearlyPrice,
                    ////    //    ApDiscount = obj.ApplicableDiscount,
                    ////    //    Applicable = true,
                    ////    //    OfferPrice = obj.OfferPrice,
                    ////    //    PayType = "Year"
                    ////    //};
                    ////}




                    #endregion

                    await transaction.CommitAsync();
                    Log.Info("----Info CreateUpdateServiceItemQuery method Exit----");
                    return ApiMessageInfo.Status(1, item.Id);
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    Log.Error("Error in CreateUpdateServiceItemQuery Method");
                    Log.Error("Error occured time : " + DateTime.UtcNow);
                    Log.Error("Error message : " + ex.Message);
                    Log.Error("Error StackTrace : " + ex.StackTrace);
                    return ApiMessageInfo.Status(0);
                }
            }
        }
    }

    #endregion



    #region CreateAssignresourceQuery

    public class CreateAssignresourceQuery : IRequest<AppCtrollerDto>
    {
        public UserIdentityDto User { get; set; }
        public AssignTicketResourceDto Input { get; set; }
    }

    public class CreateAssignresourceQueryHandler : IRequestHandler<CreateAssignresourceQuery, AppCtrollerDto>
    {
        private readonly CINDBOneContext _context;
        private readonly IMapper _mapper;
        public CreateAssignresourceQueryHandler(CINDBOneContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<AppCtrollerDto> Handle(CreateAssignresourceQuery request, CancellationToken cancellationToken)
        {
            try
            {
                Log.Info("----Info CreateAssignresourceQuery method start----");

                var obj = request.Input;
                var b2cTicket = await _context.FomB2CJobTickets.FirstOrDefaultAsync(e => e.TicketNumber == obj.TicketNumber);
                b2cTicket.ResCode = obj.ResCode;
                b2cTicket.JOStatus = (int)MetadataJoStatusEnum.Approved;
                b2cTicket.IsApproved = true;
                b2cTicket.ApprovedDate = obj.ApprovedDate ?? DateTime.Now;
                b2cTicket.ApprovedBy = request.User.Email;

                _context.FomB2CJobTickets.Update(b2cTicket);
                await _context.SaveChangesAsync();

                Log.Info("----Info CreateAssignresourceQuery method Exit----");
                return ApiMessageInfo.Status(1, b2cTicket.Id);
            }
            catch (Exception ex)
            {
                Log.Error("Error in CreateAssignresourceQuery Method");
                Log.Error("Error occured time : " + DateTime.UtcNow);
                Log.Error("Error message : " + ex.Message);
                Log.Error("Error StackTrace : " + ex.StackTrace);
                return ApiMessageInfo.Status(0);
            }
        }
    }
    #endregion

    #region DeleteServiceItem

    public class DeleteServiceItem : IRequest<int>
    {
        public UserIdentityDto User { get; set; }
        public int Id { get; set; }
    }

    public class DeleteServiceItemQueryHandler : IRequestHandler<DeleteServiceItem, int>
    {
        //private readonly ICurrentUserService _currentUserService;
        //protected string UserId => _currentUserService.UserId;
        private readonly CINDBOneContext _context;
        private readonly IMapper _mapper;

        public DeleteServiceItemQueryHandler(CINDBOneContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> Handle(DeleteServiceItem request, CancellationToken cancellationToken)
        {
            try
            {
                Log.Info("----Info DeleteServiceItem method start----");

                if (request.Id > 0)
                {
                    var ServiceItem = await _context.FomServiceItems.FirstOrDefaultAsync(e => e.Id == request.Id);
                    _context.Remove(ServiceItem);
                    await _context.SaveChangesAsync();
                    return request.Id;
                }
                return 0;
            }
            catch (Exception ex)
            {
                Log.Error("Error in DeleteServiceItem Method");
                Log.Error("Error occured time : " + DateTime.UtcNow);
                Log.Error("Error message : " + ex.Message);
                Log.Error("Error StackTrace : " + ex.StackTrace);
                return 0;
            }

        }
    }

    #endregion
}
