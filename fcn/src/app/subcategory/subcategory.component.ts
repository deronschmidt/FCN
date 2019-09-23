import { Component, OnInit } from '@angular/core';
import { FcnService } from '../_services/fcn.service';

@Component({
  selector: 'app-subcategory',
  templateUrl: './subcategory.component.html',
  styleUrls: ['./subcategory.component.css']
})
export class SubcategoryComponent implements OnInit {

  constructor(private fcnService: FcnService) { }
  subcategories: string[];
  ngOnInit() {
    this.fcnService.GetAllSubcategories().subscribe(
      data => {  
        this.subcategories = data as string [];  
       }  
    );
  }

}
