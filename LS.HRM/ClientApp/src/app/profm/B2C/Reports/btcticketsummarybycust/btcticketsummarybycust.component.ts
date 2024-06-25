import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { AuthorizeService } from 'src/app/api-authorization/AuthorizeService';
import { ApiService } from 'src/app/services/api.service';
import { NotificationService } from 'src/app/services/notification.service';
import { UtilityService } from 'src/app/services/utility.service';
import { ValidationService } from 'src/app/sharedcomponent/ValidationService';
import { ParentB2CComponent } from 'src/app/sharedcomponent/parentb2c.component';

@Component({
  selector: 'app-btcticketsummarybycust',
  templateUrl: './btcticketsummarybycust.component.html',
  styles: [
  ]
})
export class BtcticketsummarybycustComponent extends ParentB2CComponent implements OnInit {
  serviceList: any;
  constructor(private fb: FormBuilder, private apiService: ApiService,
    private authService: AuthorizeService, private utilService: UtilityService,
    private notifyService: NotificationService, private validationService: ValidationService) {
    super(authService)
  };

  ngOnInit(): void {
    this.loadFormData();
  }
  loadFormData() {

    this.serviceList = [
      { id: "Daily", name: 'Daily Service', },
      { id: "Monthly", name: 'Monthly Service' },
      { id: "Yearly", name: 'Yearly Service' },
    ];
  }
}
