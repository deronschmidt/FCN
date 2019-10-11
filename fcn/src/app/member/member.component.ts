import { Component, OnInit } from '@angular/core';
import { FcnService } from '../_services/fcn.service';
import { EditModalComponent } from '../edit-modal/edit-modal.component';
import { Member } from '../_models/member';
import { Community } from '../_models/community'
import { Role } from '../_models/role'
import { BsModalRef, BsModalService } from 'ngx-bootstrap';

@Component({
  selector: 'app-member',
  templateUrl: './member.component.html',
  styleUrls: ['./member.component.css']
})
export class MemberComponent implements OnInit {
  public modalRef: BsModalRef;
  constructor(private fcnService: FcnService, private modalService: BsModalService) { }
  communities: string[];
  roles: string [];
  newMember: Member;
  existingMember: Member;
  editMode: string;
  members: string[];
  ngOnInit() {
    this.fcnService.GetAllMembers().subscribe(
      data => {
        this.members = data as string[];
      }
    );

    this.fcnService.GetAllCommunities().subscribe(
      data => {
        this.communities = data as string[];
      }
    );

    this.fcnService.GetAllRoles().subscribe(
      data => {
        this.roles = data as string[];
      }
    );
  }
  addNewMember() {
    this.newMember = new Member();
    this.newMember.id = 0;
    this.newMember.roleID = 0;
    this.newMember.roleType = '';
    this.newMember.firstName = '';
    this.newMember.lastName = '';
    this.newMember.address1 = '';
    this.newMember.address2 = '';
    this.newMember.city = '';
    this.newMember.state = '';
    this.newMember.zipCode = '';
    this.newMember.email = '';
    this.newMember.phone = '';
    this.newMember.alternatePhone = '';
    this.newMember.communityID = 0;
    this.newMember.communityName = '';
    this.newMember.licensed = false;
    this.newMember.active = false;
    this.newMember.createdDate = new Date();
    this.newMember.updatedDate = new Date();

    this.editMode = 'newMember';

    let modalRef = this.modalService.show(EditModalComponent, {class: 'modal-xl'});
    modalRef.content.editMode = this.editMode;
    modalRef.content.roles = this.roles;
    modalRef.content.communities = this.communities
    modalRef.content.newMember = this.newMember;
    modalRef.content.saveValid = false;
    modalRef.content.event.subscribe(data => {
      this.newMember = data;
      this.fcnService.CreateMember(this.newMember);
    });

  }

  editMember(idx: number, item: Member) {
    console.log(item);
    this.existingMember = new Member();      
    this.existingMember.id = item.id;
    this.existingMember.roleID = item.roleID;
    this.existingMember.roleType = item.roleType;
    this.existingMember.firstName = item.firstName;
    this.existingMember.lastName = item.lastName;
    this.existingMember.address1 = item.address1;
    this.existingMember.address2 = item.address2;
    this.existingMember.city = item.city;
    this.existingMember.state = item.state;
    this.existingMember.zipCode = item.zipCode;
    this.existingMember.email = item.email;
    this.existingMember.phone = item.phone;
    this.existingMember.alternatePhone = item.alternatePhone;
    this.existingMember.communityID = item.communityID;
    this.existingMember.communityName = item.communityName;
    this.existingMember.licensed = item.licensed;
    this.existingMember.active = item.active;
    this.existingMember.createdDate = item.createdDate;
    this.existingMember.updatedDate = item.updatedDate;  
    this.editMode = 'editMember';

    let modalRef = this.modalService.show(EditModalComponent,{class: 'modal-xl'});
    modalRef.content.editMode = this.editMode;
    modalRef.content.roles = this.roles;
    modalRef.content.communities = this.communities
    modalRef.content.existingMember = this.existingMember;
    modalRef.content.saveValid = true;
    modalRef.content.event.subscribe(data => {
      this.existingMember = data;
      this.fcnService.UpdateMember(this.existingMember, this.existingMember.id);
    });
  }

  delMember(item: Member) {    
    if(confirm(`Are you sure to delete ${item.firstName} ${item.lastName} ?`)) {
      this.fcnService.DeleteMember(item.id);
    }
  }
}
