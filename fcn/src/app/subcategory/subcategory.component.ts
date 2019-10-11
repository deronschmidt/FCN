import { Component, OnInit } from '@angular/core';
import { FcnService } from '../_services/fcn.service';
import { EditModalComponent } from '../edit-modal/edit-modal.component';
import { Category } from '../_models/category';
import { Subcategory } from '../_models/subcategory'
import { BsModalRef, BsModalService } from 'ngx-bootstrap';

@Component({
  selector: 'app-subcategory',
  templateUrl: './subcategory.component.html',
  styleUrls: ['./subcategory.component.css']
})
export class SubcategoryComponent implements OnInit {
  public modalRef: BsModalRef;
  constructor(private fcnService: FcnService, private modalService: BsModalService) { }
  subcategories: string[];
  categories: string [];
  newSubcategory: Subcategory;
  existingSubcategory: Subcategory;
  editMode: string;
  ngOnInit() {
    this.fcnService.GetAllSubcategories().subscribe(
      data => {
        this.subcategories = data as string[];
        console.log(this.subcategories);
      }
    );
    this.fcnService.GetAllCategories().subscribe(
      data => {
        this.categories = data as string[];
        console.log(this.categories);
      }
    );
  }

  addNewSubcategory() {
    this.newSubcategory = new Subcategory();
    this.newSubcategory.active = false;
    this.newSubcategory.createdDate = new Date();
    this.newSubcategory.updatedDate = new Date();
    this.newSubcategory.description = '';
    this.newSubcategory.subCategoryName = '';
    this.newSubcategory.categoryID = 0;
    this.newSubcategory.categoryName = '';
    this.newSubcategory.id = 0;

    this.editMode = 'newSubcategory';

    let modalRef = this.modalService.show(EditModalComponent);
    modalRef.content.editMode = this.editMode;
    modalRef.content.categories = this.categories;
    modalRef.content.newSubcategory = this.newSubcategory;
    modalRef.content.saveValid = false;
    modalRef.content.event.subscribe(data => {
      this.newSubcategory = data;
      this.fcnService.CreateSubcategory(this.newSubcategory);
    });

  }

  editSubcategory(idx: number, item: Subcategory) {
    console.log(item);
    this.existingSubcategory = new Subcategory();
    this.existingSubcategory.active = item.active;
    this.existingSubcategory.createdDate = item.createdDate;
    this.existingSubcategory.updatedDate = item.updatedDate;
    this.existingSubcategory.description = item.description;
    this.existingSubcategory.subCategoryName = item.subCategoryName;
    this.existingSubcategory.categoryID = item.categoryID;
    this.existingSubcategory.categoryName = item.categoryName;
    this.existingSubcategory.id = item.id;
    this.editMode = 'editSubcategory';

    let modalRef = this.modalService.show(EditModalComponent);
    modalRef.content.editMode = this.editMode;
    modalRef.content.categories = this.categories;
    modalRef.content.existingSubcategory = this.existingSubcategory;
    modalRef.content.saveValid = true;
    modalRef.content.event.subscribe(data => {
      this.existingSubcategory = data;
      this.fcnService.UpdateSubcategory(this.existingSubcategory, this.existingSubcategory.id);
    });
  }

  delSubcategory(item: Subcategory) {    
    if(confirm(`Are you sure to delete ${item.subCategoryName} ?`)) {
      this.fcnService.DeleteSubcategory(item.id);
    }
  }

}
