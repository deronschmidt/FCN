import { Component, OnInit } from '@angular/core';
import { FcnService } from '../_services/fcn.service';

@Component({
  selector: 'app-role',
  templateUrl: './role.component.html',
  styleUrls: ['./role.component.css']
})
export class RoleComponent implements OnInit {

  constructor(private fcnService: FcnService) { }
  roles: string[];
  ngOnInit() {
    this.fcnService.GetAllRoles().subscribe(
      data => {  
        this.roles = data as string [];  
       }  
    );
  }

}
