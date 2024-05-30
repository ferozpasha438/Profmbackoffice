import { Component } from "@angular/core";
import { TranslateService } from "@ngx-translate/core";
import { AuthorizeService } from "../api-authorization/AuthorizeService";
import { default as data } from "../../assets/i18n/apiuri.json";

@Component({

  selector: 'app-ParentB2CComponent',
  template: '',
  styles: [
  ]
})
export class ParentB2CComponent {
  constructor(authService: AuthorizeService) {    
    authService.SetApiEndPoint(data.fomb2capiurl);
    //authService.SetApiEndPoint(authService.getUser()?.fltUrl);
  }
  public getCurrentUrl(): string {
    return data.fomb2capiurl
  };

}



