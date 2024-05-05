import { HttpErrorResponse } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { MatDialog } from "@angular/material/dialog";
import { Router } from "@angular/router";
import { ApiMessageDto } from "../models/CINServerMetaDataDto";
import { NotificationService } from "./notification.service";
import { ErrorMessage } from "./utility.constants";
import * as moment from 'moment/moment';
import { TranslateService } from "@ngx-translate/core";

@Injectable({
  providedIn: 'root'
})
export class FomSharedService {
  ticketNumber: string='';
  constructor(private notifyService: NotificationService, private router: Router, private translate: TranslateService) {

  }

  set ticketNumberToEdit(ticketNumber: string) {
    this.ticketNumber = ticketNumber;
  }

  get ticketNumberToEdit(): string {
    return this.ticketNumber;
  }


}
