//using AutoMapper;
//using PROFM.Application;
//using PROFM.Application.Common;
//using PROFM.Application.ProfmAdminDtos;
//using PROFM.DB;
//using MediatR;
//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading;
//using System.Threading.Tasks;
//using AutoMapper.QueryableExtensions;
//using System.Linq.Dynamic.Core;
//using PROFM.Domain.ProfmMgt;
//using PROFM.Domain.SystemSetup;

//namespace PROFM.Application.ProfmQuery
//{


//    #region Get MenuItems For Individual User

//    public class GetUserMenuSubLink : IRequest<List<string>>
//    {
//        public int UserId { get; set; }
//    }

//    public class GetUserMenuSubLinkHandler : IRequestHandler<GetUserMenuSubLink, List<string>>
//    {
//        private readonly DBContext _context;
//        private readonly IMapper _mapper;
//        public GetUserMenuSubLinkHandler(DBContext context, IMapper mapper)
//        {
//            _context = context;
//            _mapper = mapper;
//        }
//        public async Task<List<string>> Handle(GetUserMenuSubLink request, CancellationToken cancellationToken)
//        {
//            var menuIds = _context.ErpFomSysMenuLoginId.AsNoTracking().Where(e => e.LoginId == request.UserId).Select(e => e.MenuCode);
//            return await _context.ErpFomSysMenuOption.Where(e => menuIds.Any(mid => mid == e.MenuCode)).Select(e => e.MenuCode).ToListAsync();
//            //List<string> menuCodes = new();
//            //var menuCodeList = await _context.MenuOptions.Where(e => menuIds.Any(mid => mid == e.MenuCode)).Select(e => e.MenuCode).ToListAsync();
//            //foreach (var gItem in menuCodeList.Select(e => e.Substring(0, 1)).GroupBy(code => code))
//            //{
//            //    if (Alphabets().Any(e => gItem.Key.Contains(e)))
//            //    {
//            //        menuCodes.Add($"{gItem.Key.Substring(0, 1)}000");
//            //    }
//            //}
//            //        menuCodes.Add("AA00");
//            //menuCodes.AddRange(menuCodeList);
//            //return menuCodes;
//            //return await _context.MenuOptions.Where(e => menuIds.Any(mid => mid == e.MenuCode)).Select(e => e.MenuNameEng + "-" + e.MenuNameArb).ToListAsync();

//        }

//        //  IEnumerable<string> Alphabets() => new[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
//    }

//    #endregion


//    #region GetUserWiseMenuCodes

//    public class GetUserWiseMenuCodes : IRequest<List<string>>
//    {
//        public UserIdentityDto User { get; set; }
//        public int UserId { get; set; }
//    }

//    public class GetUserWiseMenuCodesHandler : IRequestHandler<GetUserWiseMenuCodes, List<string>>
//    {
//        private readonly DBContext _context;
//        private readonly IMapper _mapper;
//        public GetUserWiseMenuCodesHandler(DBContext context, IMapper mapper)
//        {
//            _context = context;
//            _mapper = mapper;
//        }
//        public async Task<List<string>> Handle(GetUserWiseMenuCodes request, CancellationToken cancellationToken)
//        {
//            return await _context.ErpFomSysMenuLoginId.Where(e => e.LoginId == request.UserId).AsNoTracking().Select(e => e.MenuCode).ToListAsync();
//        }
//    }

//    #endregion


//    //#region GetUserWiseMenuCodes

//    //public class GetUserWiseMenu : IRequest<List<TblErpSysMenuOptionDto>>
//    //{
//    //    public UserIdentityDto User { get; set; }
//    //    public string MenuCode { get; set; }
//    //}

//    //public class GetUserWiseMenuHandler : IRequestHandler<GetUserWiseMenu, List<TblErpSysMenuOptionDto>>
//    //{
//    //    private readonly DBContext _context;
//    //    private readonly IMapper _mapper;
//    //    public GetUserWiseMenuHandler(DBContext context, IMapper mapper)
//    //    {
//    //        _context = context;
//    //        _mapper = mapper;
//    //    }
//    //    public async Task<List<TblErpSysMenuOptionDto>> Handle(GetUserWiseMenu request, CancellationToken cancellationToken)
//    //    {
//    //        TblErpSysMenuOptionDto objMenu = new();
//    //       // var menuIds = _context.ErpFomSysMenuLoginId.AsNoTracking().Where(e => e.LoginId == request.UserId).Select(e => e.MenuCode);
//    //        //var menus = _context.ErpFomSysMenuOption.AsNoTracking().Where(e => menuIds.Any(mid => mid == e.MenuCode)).ToListAsync();
//    //        //return;



//    //        var Menus = await _context.ErpFomSysMenuOption.AsNoTracking().ProjectTo<TblErpSysMenuOptionDto>(_mapper.ConfigurationProvider).Where(e => e.MenuCode == request.MenuCode).ToListAsync();
//    //        return Menus;

//    //    }
//    //}

//    //#endregion


//    #region GetSideMenu OptionList For User

//    public class GetSideMenuOptionList : IRequest<List<GetSideMenuOptionListDto>>
//    {
//        public UserIdentityDto User { get; set; }

//        public int UserId { get; set; }
//    }

//    public class GetSideMenuOptionListHandler : IRequestHandler<GetSideMenuOptionList, List<GetSideMenuOptionListDto>>
//    {
//        private readonly DBContext _context;
//        private readonly IMapper _mapper;
//        public GetSideMenuOptionListHandler(DBContext context, IMapper mapper)
//        {
//            _context = context;
//            _mapper = mapper;
//        }
//        public async Task<List<GetSideMenuOptionListDto>> Handle(GetSideMenuOptionList request, CancellationToken cancellationToken)
//        {
//            var menuLoginIds = _context.ErpFomSysMenuLoginId.AsNoTracking().Where(e => e.LoginId == request.UserId && e.IsAllowed)
//                                                                   .Select(e => e.MenuCode);
//            var menuOptions = _context.ErpFomSysMenuOption.AsNoTracking();

//            var groupList = await menuOptions.Where(e => e.IsForm && menuLoginIds.Any(mid => mid == e.MenuCode))
//               .ToListAsync();
//            var groupByModules = groupList.GroupBy(e => e.ModuleName);
//            List<GetSideMenuOptionListDto> userSideMenuItems = new();
//            int moduleMenuCount = 0;

//            foreach (var module in groupByModules)
//            {
//                var menuOptionsList = await menuOptions.Where(e => e.ModuleName == module.Key).OrderBy(e => e.MenuCode).ToListAsync();

//                var menuGpList = menuOptionsList.GroupBy(e => e.Level2);
//                GetSideMenuOptionListDto userSideMenu = new();
//                List<MultiListMenuOptionDto> MultiListMenuOptions = new();
//                List<SubModuleMenuOptionDto> subModuleMenuOptions = new();
//                foreach (var l2Item in menuGpList)//.OrderBy(e=>e.Count()))
//                {
//                    if (l2Item.Key == -1)
//                    {
//                        moduleMenuCount++;
//                        userSideMenu.MainModule = l2Item.Where(e => e.Level2 == l2Item.Key)
//                                .Select(e => new MainModuleMenuOptionDto { Module = $"module_{moduleMenuCount}", ModuleEn = e.MenuNameEng, ModuleAr = e.MenuNameArb }).FirstOrDefault();
//                    }

//                    if (l2Item.Key != -1 && l2Item.Count() == 1 && l2Item.Any(e => menuLoginIds.Any(mlId => mlId == e.MenuCode)))
//                        subModuleMenuOptions.Add(l2Item.Where(e => e.Level2 == l2Item.Key)
//                            .Select(e => new SubModuleMenuOptionDto { MenuNameEng = e.MenuNameEng, MenuNameArb = e.MenuNameArb, IsForm = e.IsForm, Path = e.Path })
//                            .FirstOrDefault());
//                    else
//                    {
//                        if (l2Item.Any(e => menuLoginIds.Any(mlId => mlId == e.MenuCode)))
//                        {
//                            userSideMenu.HasMultiItems = true;
//                            MultiListMenuOptionDto multiListMenuOption = new();

//                            multiListMenuOption.SubModule = l2Item.Where(e => e.Level2 == l2Item.Key && e.Level3 == -1)
//                                .Select(e => new MainModuleMenuOptionDto { ModuleEn = e.MenuNameEng, ModuleAr = e.MenuNameArb }).FirstOrDefault();

//                            multiListMenuOption.Links = l2Item.Where(e => e.Level2 == l2Item.Key && e.Level3 != -1 && menuLoginIds.Any(mlId => mlId == e.MenuCode))
//                                .Select(e => new SubModuleMenuOptionDto { MenuNameEng = e.MenuNameEng, MenuNameArb = e.MenuNameArb, Path = e.Path }).ToList();


//                            if (multiListMenuOption.Links.Count > 0)
//                            {
//                                if (l2Item.Key == 0)
//                                    multiListMenuOption.Links = multiListMenuOption.Links.Where(e => !GetNonLinks().Any(lk => lk == e.MenuNameEng)).ToList();

//                                MultiListMenuOptions.Add(multiListMenuOption);
//                            }
//                        }
//                    }
//                }
//                userSideMenu.SubModules = subModuleMenuOptions;
//                userSideMenu.MItems = MultiListMenuOptions;
//                userSideMenuItems.Add(userSideMenu);


//                //GetSideMenuOptionListDto userSideMenuItem = new()
//                //{
//                //    ModuleEn = GetModuleEn(module.Key),
//                //    ModuleAr = GetModuleAr(module.Key),
//                //    SubModuleEn = GetSubModuleEn(module.Key),
//                //    SubModuleAr = GetSubModuleAr(module.Key),
//                //    Items = GetLinks(module)
//                //    //ArItems = GetLinks(item)
//                //};

//                //userSideMenuItems.Add(userSideMenuItem);
//            }

//            return userSideMenuItems;

//            //foreach (var menuOption in menuOptionsList)
//            //{
//            //    GetSideMenuOptionListDto userSideMenu = new();

//            //    //if (!menuOption.IsForm && menuOption.Level2 == -1 && menuOption.Level3 == -1)
//            //    //    userSideMenu.MainModule = new MainModuleMenuOptionDto { ModuleEn = menuOption.MenuNameEng, ModuleAr = menuOption.MenuNameArb };
//            //    //if (menuOption.Level2 != -1 && menuOption.Level3 == -1)
//            //    //    userSideMenu.SubModule = new SubModuleMenuOptionDto { MenuNameEng = menuOption.MenuNameEng, MenuNameArb = menuOption.MenuNameArb, IsForm = menuOption.IsForm, Path = menuOption.Path };


//            //    //GetSideMenuOptionListDto userSideMenu = new()
//            //    //{
//            //    //    MainModule = new MainModuleMenuOptionDto { ModuleEn = menuOption.MenuNameEng, },
//            //    //    ModuleEn = GetModuleEn(module.Key),
//            //    //    ModuleAr = GetModuleAr(module.Key),
//            //    //    //SubModule = 
//            //    //    SubModuleEn = GetSubModuleEn(module.Key),
//            //    //    SubModuleAr = GetSubModuleAr(module.Key),
//            //    //    //ArItems = GetLinks(item)
//            //    //};
//            //    //userSideMenu.Items = GetLinks(module);

//            //    //userSideMenuItems.Add(userSideMenu);
//            //}


//            //var menuList = from mIds in menuLoginIds
//            //               join opts in menuOptions
//            //               on mIds.MenuCode equals opts.MenuCode
//            //               where opts.IsForm == true
//            //               select new GetSideMenuOptionListDto
//            //               {

//            //                   Module =

//            //                   MenuCode = opts.MenuCode,
//            //                   MenuNameEng = opts.MenuNameEng,
//            //                   MenuNameArb = opts.MenuNameArb,
//            //                   IsForm = opts.IsForm,
//            //                   Level1 = opts.Level1,
//            //                   Level2 = opts.Level2,
//            //                   Level3 = opts.Level3,
//            //                   Path = opts.Path,
//            //               };
//            //var menuItems = await menuList.ToListAsync(cancellationToken);

//            //foreach (var item in menuItems)
//            //{
//            //    item
//            //}
//        }
//        private List<TblErpSysMenuOptionDto> GetLinks(IGrouping<string, TblErpSysMenuOption> item)
//        {
//            return item.Select(e => new TblErpSysMenuOptionDto { MenuNameEng = e.MenuNameEng, MenuNameArb = e.MenuNameArb, Path = e.Path }).ToList();
//        }
//        private string GetModuleEn(string moduleName) => GetModuleList().FirstOrDefault(e => moduleName == e.Value)?.Text ?? string.Empty;
//        private string GetModuleAr(string moduleName) => GetModuleList().FirstOrDefault(e => moduleName == e.Value)?.TextAr ?? string.Empty;

//        private string GetSubModuleEn(string submoduleName) => GetSubModuleList().FirstOrDefault(e => submoduleName == e.Value)?.Text ?? string.Empty;
//        private string GetSubModuleAr(string submoduleName) => GetSubModuleList().FirstOrDefault(e => submoduleName == e.Value)?.TextAr ?? string.Empty;

//        private string[] GetNonLinks()
//        {
//            return new[] { "Currency", "Cities", "Sequence Generator" };
//        }
//        //return moduleName switch
//        //{
//        //    "ADM" => "ADMINISTRATION MANAGEMENT",
//        //    "FI" => "FINANCE MANAGEMENT",
//        //    _ => string.Empty,
//        //};
//        //return submoduleName switch
//        //{
//        //    "ADM" => "System",
//        //    "FI" => "FINANCE",
//        //    _ => string.Empty,
//        //};


//        private List<LanCustomSelectListItem> GetModuleList()
//        {
//            return new List<LanCustomSelectListItem>
//            {
//                new(){ Text =  "ADMINISTRATION MANAGEMENT", TextAr = "إدارة الإدارة",Value="ADM"},
//               // new(){ Text =  "FINANCE MANAGEMENT", TextAr = "إدارة الشؤون المالية",Value="FI"},
//            };
//        }
//        private List<LanCustomSelectListItem> GetSubModuleList()
//        {
//            return new List<LanCustomSelectListItem>
//            {
//                new(){ Text =  "System", TextAr = "نظام", Value="ADM"},
//               // new(){ Text =  "FINANCE",TextAr = "الماليه", Value="FI"},
//            };
//        }

//    }
//    #endregion


//    #region CreateUpdate

//    public class CreateMenuOption : IRequest<int>
//    {
//        public MenuItemFlatNodeListDto MenuNodeList { get; set; }
//    }

//    public class CreateMenuOptionQueryHandler : IRequestHandler<CreateMenuOption, int>
//    {
//        //private readonly ICurrentUserService _currentUserService;
//        //protected string UserId => _currentUserService.UserId;
//        private readonly DBContext _context;
//        private readonly IMapper _mapper;

//        public CreateMenuOptionQueryHandler(DBContext context, IMapper mapper)
//        {
//            _context = context;
//            _mapper = mapper;
//        }

//        public async Task<int> Handle(CreateMenuOption request, CancellationToken cancellationToken)
//        {

//            using (var transaction = await _context.Database.BeginTransactionAsync())
//            {
//                try
//                {
//                    Log.Info("----Info CreateMenuOption method start----");

//                    var MenuItemIds = _context.ErpFomSysMenuLoginId.AsNoTracking()
//                        .Where(e => e.LoginId == request.MenuNodeList.UserId);
//                    if (await MenuItemIds.AnyAsync())
//                    {
//                        _context.ErpFomSysMenuLoginId.RemoveRange(MenuItemIds);
//                        await _context.SaveChangesAsync();
//                    }

//                    var menuOptions = await _context.ErpFomSysMenuOption.Where(e => e.IsForm).AsNoTracking().ToListAsync();
//                    var nodeList = request.MenuNodeList.Nodes.Distinct();
//                    var menuItemIdList = menuOptions.Where(e => nodeList.Any(nd => nd.Contains(e.MenuCode)))
//                        .Select(e => new TblErpSysMenuLoginId { MenuCode = e.MenuCode, LoginId = request.MenuNodeList.UserId, IsAllowed = true, CreatedOn = DateTime.Now })
//                        .ToList();

//                    await _context.ErpFomSysMenuLoginId.AddRangeAsync(menuItemIdList);
//                    await _context.SaveChangesAsync();

//                    //var menuItemIdList = from opt in menuOptions
//                    //                     join node in nodeList
//                    //                     on opt.MenuNameEng


//                    //foreach (var menu in request.MenuNodeList.Nodes)
//                    //{
//                    //    string menuCode = menu.Item;
//                    //    string level = menu.Level;

//                    //    var obj = new TblErpSysMenuLoginId
//                    //    {
//                    //        MenuCode =
//                    //    };
//                    //}
//                    //await _context.SaveChangesAsync();

//                    Log.Info("----Info CreateMenuOption method Exit----");
//                    await transaction.CommitAsync();
//                    return request.MenuNodeList.UserId;
//                }
//                catch (Exception ex)
//                {
//                    await transaction.RollbackAsync();
//                    Log.Error("Error in CreateMenuOption Method");
//                    Log.Error("Error occured time : " + DateTime.UtcNow);
//                    Log.Error("Error message : " + ex.Message);
//                    Log.Error("Error StackTrace : " + ex.StackTrace);
//                    return 0;
//                }
//            }
//        }
//    }
//    #endregion


//}
