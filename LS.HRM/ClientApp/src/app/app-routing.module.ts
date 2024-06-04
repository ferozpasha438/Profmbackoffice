import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './account/login.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { HomeComponent } from './home/home.component';
import { B2cdashboardComponent } from './home/b2cdashboard.component';


//import { BranchesComponent } from './systemsetup/branches/branches.component';
//import { CompanysetupComponent } from './systemsetup/companysetup/companysetup.component';
//import { CitiesComponent } from './systemsetup/cities/cities.component';
//import { CurrencyComponent } from './systemsetup/currency/currency.component';
//import { LoginandsecurityComponent } from './systemsetup/loginandsecurity/loginandsecurity.component';
//import { TransactionsequenceComponent } from './systemsetup/transactionsequence/transactionsequence.component';
//import { TaxesComponent } from './systemsetup/taxes/taxes.component';


const routes: Routes = [
  { path: '', component: LoginComponent, pathMatch: 'full' },
  //{ path: 'login', component: LoginComponent },
  //{ path: 'home', component: HomeComponent },
  {
    path: 'dashboard', component: DashboardComponent,
    children: [

      { path: '', component: HomeComponent },
      { path: 'b2cdashboard', component: B2cdashboardComponent },

      //ADM
      //{ path: 'branchlist', component: BranchesComponent },
      //{ path: 'companysetup', component: CompanysetupComponent },
      //{ path: 'cities', component: CitiesComponent },
      //{ path: 'currencies', component: CurrencyComponent },
      //{ path: 'usersecurity', component: LoginandsecurityComponent },
      //{ path: 'transactionsequence', component: TransactionsequenceComponent },
      //{ path: 'taxlist', component: TaxesComponent },      


      //SySTEM
      { path: 'system', loadChildren: () => import('./SystemSetup/systemsetup.module').then(m => m.SystemsetupModule) },

      //INVT
      { path: 'inventory', loadChildren: () => import('./Inventorysetup/inventorysetup.module').then(m => m.InventorysetupModule) },

      //OPERATION
      { path: 'operation', loadChildren: () => import('./Operationalmgt/operationalmgt.module').then(m => m.OperationalmgtModule) },

      //SALESSETUP
      { path: 'sales', loadChildren: () => import('./Sales/salessetup.module').then(m => m.SalessetupModule) },

       //PROfm
      { path: 'profm', loadChildren: () => import('./profm/profm-routing.module').then(m => m.PROfmRoutingModule) },
     
       ]
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
