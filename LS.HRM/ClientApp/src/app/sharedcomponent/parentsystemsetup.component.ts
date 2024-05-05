import { Component } from "@angular/core";
import { AuthorizeService } from "../api-authorization/AuthorizeService";
import { default as data } from "../../assets/i18n/apiuri.json";
@Component({
  selector: 'app-ParentSystemSetupComponent',
  template: '',
  styles: [
  ]
})
export class ParentSystemSetupComponent {
  constructor(authService: AuthorizeService) {
    authService.SetApiEndPoint(data.financeurl);
   /* authService.SetApiEndPoint(data.fomapiurl);*/
   // authService.SetApiEndPoint(authService.getUser()?.admUrl);
  }
}
