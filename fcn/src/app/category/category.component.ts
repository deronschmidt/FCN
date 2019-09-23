import { Component, OnInit } from '@angular/core';
import { FcnService } from '../_services/fcn.service';

@Component({
  selector: 'app-category',
  templateUrl: './category.component.html',
  styleUrls: ['./category.component.css']
})
export class CategoryComponent implements OnInit {

  constructor(private fcnService: FcnService) { }
  categories: string[];
  ngOnInit() {
    this.fcnService.GetAllCategories().subscribe(
      data => {  
        this.categories = data as string [];  
       }  
    );
  }

}
