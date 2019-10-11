import { Component, OnInit } from '@angular/core';
import { FcnService } from '../_services/fcn.service';
import { EditModalComponent } from '../edit-modal/edit-modal.component';
import { Member } from '../_models/member';
import { Community } from '../_models/community';
import { BsModalRef, BsModalService } from 'ngx-bootstrap';

@Component({
  selector: 'app-community',
  templateUrl: './community.component.html',
  styleUrls: ['./community.component.css']
})
export class CommunityComponent implements OnInit {
  public modalRef: BsModalRef;
  constructor(private fcnService: FcnService, private modalService: BsModalService) { }
  subcategories: string[];
  categories: string [];
  newCommunity: Community;
  existingCommunity: Community;
  editMode: string;
  communities: string[];
  ngOnInit() {
    this.fcnService.GetAllCommunities().subscribe(
      data => {
        this.communities = data as string[];
      }
    );
  }

  addNewCommunity() {
    this.newCommunity = new Community();
    this.newCommunity.id = 0;
    this.newCommunity.communityName = '';
    this.newCommunity.affiliation = '';
    this.newCommunity.address1 = '';
    this.newCommunity.address2 = '';
    this.newCommunity.city = '';
    this.newCommunity.state = '';
    this.newCommunity.zipCode = '';
    this.newCommunity.phone = '';
    this.newCommunity.alternatePhone = '';
    this.newCommunity.email = '';
    this.newCommunity.website = '';
    this.newCommunity.active = false;
    this.newCommunity.createdDate = new Date();
    this.newCommunity.updatedDate = new Date();    
    this.editMode = 'newCommunity';

    let modalRef = this.modalService.show(EditModalComponent);
    modalRef.content.editMode = this.editMode;
    modalRef.content.categories = this.categories;
    modalRef.content.newCommunity = this.newCommunity;
    modalRef.content.saveValid = false;
    modalRef.content.event.subscribe(data => {
      this.newCommunity = data;
      this.fcnService.CreateCommunity(this.newCommunity);
    });

  }

  editCommunity(idx: number, item: Community) {
    console.log(item);
    this.existingCommunity = new Community();
    this.existingCommunity.id = item.id;
    this.existingCommunity.communityName = item.communityName;
    this.existingCommunity.affiliation = item.affiliation;
    this.existingCommunity.address1 = item.address1;
    this.existingCommunity.address2 = item.address2;
    this.existingCommunity.city = item.city;
    this.existingCommunity.state = item.state;
    this.existingCommunity.zipCode = item.zipCode;
    this.existingCommunity.phone = item.phone;
    this.existingCommunity.alternatePhone = item.alternatePhone;
    this.existingCommunity.email = item.email;
    this.existingCommunity.website = item.website;
    this.existingCommunity.contacts = item.contacts;
    this.existingCommunity.active = false;
    this.existingCommunity.createdDate = new Date();
    this.existingCommunity.updatedDate = new Date();    
    this.editMode = 'editCommunity';

    let modalRef = this.modalService.show(EditModalComponent);
    modalRef.content.editMode = this.editMode;
    modalRef.content.categories = this.categories;
    modalRef.content.existingCommunity = this.existingCommunity;
    modalRef.content.saveValid = true;
    modalRef.content.event.subscribe(data => {
      this.existingCommunity = data;
      this.fcnService.UpdateCommunity(this.existingCommunity, this.existingCommunity.id);
    });
  }

  delCommunity(item: Community) {    
    if(confirm(`Are you sure to delete ${item.communityName} ?`)) {
      this.fcnService.DeleteCommunity(item.id);
    }
  }
}
