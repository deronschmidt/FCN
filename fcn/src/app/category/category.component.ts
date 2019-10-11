import { Component, OnInit } from '@angular/core';
import { FcnService } from '../_services/fcn.service';
import { EditModalComponent } from '../edit-modal/edit-modal.component';
import { Category } from '../_models/category';
import { BsModalRef, BsModalService } from 'ngx-bootstrap';

@Component({
  selector: 'app-category',
  templateUrl: './category.component.html',
  styleUrls: ['./category.component.css']
})
export class CategoryComponent implements OnInit {
  public modalRef: BsModalRef;
  constructor(private fcnService: FcnService, private modalService: BsModalService) { }
  categories: string[];
  newCategory: Category;
  existingCategory: Category;
  editMode: string;
  ngOnInit() {
    this.fcnService.GetAllCategories().subscribe(
      data => {
        this.categories = data as string[];
      }
    );
  }
  addNewCategory() {
    this.newCategory = new Category();
    this.newCategory.active = false;
    this.newCategory.createdDate = new Date();
    this.newCategory.updatedDate = new Date();
    this.newCategory.description = '';
    this.newCategory.categoryName = '';
    this.newCategory.id = 0;
    this.editMode = 'newCategory';

    let modalRef = this.modalService.show(EditModalComponent);
    modalRef.content.editMode = this.editMode;
    modalRef.content.newCategory = this.newCategory;
    modalRef.content.saveValid = false;
    modalRef.content.event.subscribe(data => {
      this.newCategory = data;
      this.fcnService.CreateCategory(this.newCategory);
    });

  }

  editCategory(idx: number, item: Category) {
    this.existingCategory = new Category();
    this.existingCategory.active = item.active;
    this.existingCategory.createdDate = item.createdDate;
    this.existingCategory.updatedDate = item.updatedDate;
    this.existingCategory.description = item.description;
    this.existingCategory.categoryName = item.categoryName;
    this.existingCategory.id = item.id;
    this.editMode = 'editCategory';

    let modalRef = this.modalService.show(EditModalComponent);
    modalRef.content.editMode = this.editMode;
    modalRef.content.existingCategory = this.existingCategory;
    modalRef.content.saveValid = true;
    modalRef.content.event.subscribe(data => {
      this.existingCategory = data;
      this.fcnService.UpdateCategory(this.existingCategory, this.existingCategory.id);
    });
  }

  delCategory(item: Category) {    
    if(confirm(`Are you sure to delete ${item.categoryName} ?`)) {
      this.fcnService.DeleteCategory(item.id);
    }
  }

}
