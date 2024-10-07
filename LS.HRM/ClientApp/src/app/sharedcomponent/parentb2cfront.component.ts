import { Component } from "@angular/core";
import { TranslateService } from "@ngx-translate/core";
import { AuthorizeService } from "../api-authorization/AuthorizeService";
import { default as data } from "../../assets/i18n/apiuri.json";

@Component({

  selector: 'app-ParentB2CFrontComponent',
  template: '',
  styles: [
  ]
})
export class ParentB2CFrontComponent {
  constructor(authService: AuthorizeService) {
    authService.SetApiEndPoint(data.fomb2cfronturl);
    //authService.SetApiEndPoint(authService.getUser()?.fltUrl);
  }
  public getCurrentUrl(): string {
    return data.fomb2cfronturl
  };

}



