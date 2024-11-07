////export interface MenuItemListDto {
////  mainModule: string;
////  subModule: string;
////  list: MenuItemDto[];
////}

////export interface MenuItemDto {
////  name: string;
////  link: string;
////}

export interface CustomSelectListItem {
  text: string;
  value: string;

}
export interface LanCustomSelectListItem {
  text: string;
  value: string;
  textAr: string;
  textTwo: string;
}

export interface TblErpSysMenuOptionDto {
  menuCode: string;
  level1: number;
  level2: number;
  level3: number;
  menuNameEng: string;
  menuNameArb: string;
  isForm: boolean;
  path: string;
}

export interface GetSideMenuOptionListDto {
  moduleEn: string;
  moduleAr: string;
  subModuleEn: string;
  subModuleAr: string;
  items: TblErpSysMenuOptionDto[];
}


//public class DisciplineDto
//    {
//        public string NameEng { get; set; }
//        public List<ActivityDto> Activities { get; set; }
//    }

//    public class ActivityDto
//    {
//        public string ActName { get; set; }
//        public bool Selected { get; set; }
//    }
