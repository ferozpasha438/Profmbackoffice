import { Component } from "@angular/core";
import { TranslateService } from "@ngx-translate/core";
import { AuthorizeService } from "../api-authorization/AuthorizeService";
import { default as data } from "../../assets/i18n/apiuri.json";

@Component({
  
  selector: 'app-ParentFomMgtComponent',
  template: '',
  styles: [
  ]
})
export class ParentFomMgtComponent {
  constructor(authService: AuthorizeService) {
    authService.SetApiEndPoint('my own api');
    authService.SetApiEndPoint(data.fomapiurl);
    //authService.SetApiEndPoint(authService.getUser()?.fltUrl);
  }
}



