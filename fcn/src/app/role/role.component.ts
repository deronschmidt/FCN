import { Component, OnInit } from '@angular/core';
import { FcnService } from '../_services/fcn.service';
import { EditModalComponent } from '../edit-modal/edit-modal.component';
import { Role } from '../_models/role';
import { BsModalRef, BsModalService } from 'ngx-bootstrap';

@Component({
  selector: 'app-role',
  templateUrl: './role.component.html',
  styleUrls: ['./role.component.css']
})

export class RoleComponent implements OnInit {
  public modalRef: BsModalRef;
  constructor(private fcnService: FcnService, private modalService: BsModalService) { }
  roles: string[];
  newRole: Role;
  existingRole: Role;
  editMode: string;
  ngOnInit() {
    this.fcnService.GetAllRoles().subscribe(
      data => {
        this.roles = data as string[];
      }
    );
  }

  addNewRole() {
    this.newRole = new Role();
    this.newRole.active = false;
    this.newRole.createdDate = new Date();
    this.newRole.updatedDate = new Date();
    this.newRole.description = '';
    this.newRole.roleType = '';
    this.newRole.id = 0;
    this.editMode = 'newRole';

    let modalRef = this.modalService.show(EditModalComponent);
    modalRef.content.editMode = this.editMode;
    modalRef.content.newRole = this.newRole;
    modalRef.content.saveValid = false;
    modalRef.content.event.subscribe(data => {
      this.newRole = data;
      this.fcnService.CreateRole(this.newRole);
    });

  }

  editRole(idx: number, item: Role) {
    this.existingRole = new Role();
    this.existingRole.active = item.active;
    this.existingRole.createdDate = item.createdDate;
    this.existingRole.updatedDate = item.updatedDate;
    this.existingRole.description = item.description;
    this.existingRole.roleType = item.roleType;
    this.existingRole.id = item.id;
    this.editMode = 'editRole';

    let modalRef = this.modalService.show(EditModalComponent);
    modalRef.content.editMode = this.editMode;
    modalRef.content.existingRole = this.existingRole;
    modalRef.content.saveValid = true;
    modalRef.content.event.subscribe(data => {
      this.existingRole = data;
      this.fcnService.UpdateRole(this.existingRole, this.existingRole.id);
    });
  }

  delRole(item: Role) {    
    if(confirm(`Are you sure to delete ${item.roleType} ?`)) {
      this.fcnService.DeleteRole(item.id);
    }
  }
}
