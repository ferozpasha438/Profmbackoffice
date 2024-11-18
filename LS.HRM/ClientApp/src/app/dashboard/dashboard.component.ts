import { Component, OnInit } from '@angular/core';
import { GetSideMenuOptionListDto } from '../models/MenuItemListDto';
import { UtilityService } from '../services/utility.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styles: [
  ]
})
export class DashboardComponent implements OnInit {
  //menuItems: Array<GetSideMenuOptionListDto> = [];
  isArab: boolean = false;
  menuItems: Array<GetSideMenuOptionListDto> = []; // Declare menuItems
  constructor(private utilService: UtilityService) { }

  //ngOnInit(): void {
  //  this.isArab = this.utilService.isArabic();
  //  console.log(localStorage.getItem('language'));
  //  this.menuItems = JSON.parse(localStorage.getItem('menuItems') as string) as Array<GetSideMenuOptionListDto>;
  //  console.log('the length of the menu items are for ' + this.menuItems.length)
  //}
  ngOnInit(): void {
    // Check if the language is Arabic
    this.isArab = this.utilService.isArabic();
    console.log(localStorage.getItem('language'));

    // Initialize menuItems as an empty array to avoid 'undefined' error
    this.menuItems = [];

    // Retrieve stored menuItems from localStorage
    const storedMenuItems = localStorage.getItem('menuItems');

    // Check if menuItems exists in localStorage
    if (storedMenuItems) {
      try {
        // Parse menuItems and assign to this.menuItems
        this.menuItems = JSON.parse(storedMenuItems) as Array<GetSideMenuOptionListDto>;
        console.log('The length of the menu items is ' + this.menuItems.length);
      } catch (error) {
        console.error('Error parsing menu items:', error);
        // Handle parsing error by redirecting to login or resetting menuItems
        //window.location.href = "login"; // Redirect to login if parsing fails
        location.replace('');
      }
    } else {
      // Log warning if no menuItems found and redirect to login
      console.warn('No menu items found in localStorage. Redirecting to login page.');
     // window.location.href = "login"; // Redirect to login if menuItems is not found
      location.replace('');
    }
  }

}
