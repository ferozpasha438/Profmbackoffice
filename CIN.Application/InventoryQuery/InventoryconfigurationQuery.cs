using AutoMapper;
using CIN.Application.Common;
using CIN.Application.InventoryDtos;
using CIN.DB;
using CIN.Domain.InventorySetup;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CIN.Application.InventoryQuery
{
    #region CreateUpdate

    public class CreateInvtConfig : IRequest<(string msg, int InvconfigId)>
    {
        public UserIdentityDto User { get; set; }
        public TblInvDefInventoryConfigDto Input { get; set; }
    }

    public class CreateInvtConfigQueryHandler : IRequestHandler<CreateInvtConfig, (string msg, int InvconfigId)>
    {
        //private readonly ICurrentUserService _currentUserService;
        //protected string UserId => _currentUserService.UserId;
        private readonly CINDBOneContext _context;
        private readonly IMapper _mapper;

        public CreateInvtConfigQueryHandler(CINDBOneContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<(string msg, int InvconfigId)> Handle(CreateInvtConfig request, CancellationToken cancellationToken)
        {
            try
            {
                Log.Info("----Info CreateInvtConfig method start----");

                var obj = request.Input;
                TblInvDefInventoryConfig cObj = new();
                if (obj.Id > 0)
                    cObj = await _context.InvInventoryConfigs.FirstOrDefaultAsync(e => e.Id == obj.Id);

                cObj.AutoGenItemCode = obj.AutoGenItemCode;
                cObj.PrefixCatCode = obj.PrefixCatCode;
                cObj.NewItemIndicator = obj.NewItemIndicator;
                cObj.ItemLength = obj.ItemLength;
                cObj.CategoryLength = obj.CategoryLength;

                if (obj.Id > 0)
                {
                    cObj.ModifiedOn = DateTime.Now;
                    _context.InvInventoryConfigs.Update(cObj);
                }
                else
                {
                    cObj.CentralWHCode = obj.CentralWHCode;
                    cObj.CreatedOn = DateTime.Now;

                    cObj.CentralWHCode = obj.CentralWHCode.ToUpper();
                    if (await _context.InvInventoryConfigs.AnyAsync(e => e.CentralWHCode == obj.CentralWHCode))
                        return (ApiMessageInfo.Duplicate(nameof(obj.CentralWHCode)), 0);
                  


                    await _context.InvInventoryConfigs.AddAsync(cObj);
                }
                await _context.SaveChangesAsync();
                Log.Info("----Info CreateInvtConfig method Exit----");
                return (string.Empty, cObj.Id);
            }
            catch (Exception ex)
            {
                Log.Error("Error in CreateInvtConfig Method");
                Log.Error("Error occured time : " + DateTime.UtcNow);
                Log.Error("Error message : " + ex.Message);
                Log.Error("Error StackTrace : " + ex.StackTrace);
                return (ApiMessageInfo.Failed, 0);
            }
        }
    }
    #endregion
}
