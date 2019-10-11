import { Component, OnInit, EventEmitter } from '@angular/core';
import { Role } from '../_models/role';
import { Community } from '../_models/community';
import { Subcategory } from '../_models/subcategory';
import { Category } from '../_models/category';
import { Member } from '../_models/member';
import { Activity } from '../_models/activity';
import { BsModalRef } from 'ngx-bootstrap';

@Component({
  selector: 'app-edit-modal',
  templateUrl: './edit-modal.component.html',
  styleUrls: ['./edit-modal.component.css']
})
export class EditModalComponent {
  newRole: Role;
  newCommunity: Community;
  newSubCategory: Subcategory;
  newCategory: Category;
  newMember: Member;
  newActivity: Activity;
  existingRole: Role;
  existingCommunity: Community;
  existingSubCategory: Subcategory;
  existingCategory: Category;
  existingMember: Member;
  existingActivity: Activity;
  editMode: string;
  saveValid: boolean;
  categories: Category [];
  roles: Role [];
  communities: Community [];
  public event: EventEmitter<any> = new EventEmitter();
  constructor(private _bsModalRef: BsModalRef) { }

  ngOnInit() {

  }

  enableSave() {
    this.saveValid = false;
    if (this.editMode === 'newRole' && this.newRole.roleType && this.newRole.description) {
      this.saveValid = true;
    }
    if (this.editMode === 'editRole' && this.existingRole.roleType && this.existingRole.description) {
      this.saveValid = true;
    }
    if (this.editMode === 'newCategory' && this.newCategory.categoryName && this.newCategory.description) {
      this.saveValid = true;
    }
    if (this.editMode === 'editCategory' && this.existingCategory.categoryName && 
        this.existingCategory.description) {
      this.saveValid = true;
    }
    if (this.editMode === 'newSubcategory' && this.newSubCategory.subCategoryName && 
        this.newSubCategory.categoryName && this.newSubCategory.description) {
      this.saveValid = true;
    }
    if (this.editMode === 'editSubCategory' && this.existingSubCategory.subCategoryName && 
        this.existingSubCategory.categoryName && this.existingSubCategory.description) {
      this.saveValid = true;
    }
    if (this.editMode === 'newMember' && this.newMember.roleType && this.newMember.firstName && 
        this.newMember.lastName && this.newMember.address1 && this.newMember.city && 
        this.newMember.state && this.newMember.zipCode && this.newMember.phone && 
        this.newMember.communityName) {
      this.saveValid = true;
    }
    if (this.editMode === 'editMember' && this.existingMember.roleType && this.existingMember.firstName && 
        this.existingMember.lastName && this.existingMember.address1 && this.existingMember.city && 
        this.existingMember.state && this.existingMember.zipCode && this.existingMember.phone && 
        this.existingMember.communityName) {
      this.saveValid = true;
    }
    if (this.editMode === 'newCommunity' && this.newCommunity.communityName && this.newCommunity.affiliation &&
        this.newCommunity.address1 && this.newCommunity.city && this.newCommunity.state && 
        this.newCommunity.zipCode && this.newCommunity.phone) {
      this.saveValid = true;
    }
    if (this.editMode === 'editCommunity' && this.existingCommunity.communityName && this.existingCommunity.affiliation &&
        this.existingCommunity.address1 && this.existingCommunity.city && this.existingCommunity.state && 
        this.existingCommunity.zipCode && this.existingCommunity.phone) {
      this.saveValid = true;
    }
    if (this.editMode === 'newActivity' && this.newActivity.description && this.newActivity.activityDate && 
        this.newActivity.communityName && this.newActivity.serviceCategory && this.newActivity.fcnMemberName &&
        (this.newActivity.paidTime || this.newActivity.unpaidTime)) {
      this.saveValid = true;
    }
    if (this.editMode === 'editActivity' && this.newActivity.description && this.newActivity.activityDate && 
        this.newActivity.communityName && this.newActivity.serviceCategory && this.newActivity.fcnMemberName &&
        (this.newActivity.paidTime || this.newActivity.unpaidTime)) {
      this.saveValid = true;
    }
  }

  public onClose(): void {
    this._bsModalRef.hide();
  }

  createNewRole(): void {
    this.event.emit(this.newRole);
    this._bsModalRef.hide();
  }

  updateExistingRole(): void {
    this.event.emit(this.existingRole);
    this._bsModalRef.hide();
  }

  createNewCategory(): void {
    this.event.emit(this.newCategory);
    this._bsModalRef.hide();
  }

  updateExistingCategory(): void {
    this.event.emit(this.existingCategory);
    this._bsModalRef.hide();
  }

  createNewSubcategory(): void {
    this.event.emit(this.newSubCategory);
    this._bsModalRef.hide();
  }

  updateExistingSubcategory(): void {
    this.event.emit(this.existingSubCategory);
    this._bsModalRef.hide();
  }

  createNewMember(): void {
    this.event.emit(this.newMember);
    this._bsModalRef.hide();
  }

  updateExistingMember(): void {
    this.event.emit(this.existingMember);
    this._bsModalRef.hide();
  }

  createNewCommunity(): void {
    this.event.emit(this.newCommunity);
    this._bsModalRef.hide();
  }

  updateExistingCommunity(): void {
    this.event.emit(this.existingCommunity);
    this._bsModalRef.hide();
  }

  createNewActivity(): void {
    this.event.emit(this.newActivity);
    this._bsModalRef.hide();
  }

  updateExistingActivity(): void {
    this.event.emit(this.existingCategory);
    this._bsModalRef.hide();
  }
}
