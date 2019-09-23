import { Component, OnInit } from '@angular/core';
import { FcnService } from '../_services/fcn.service';

@Component({
  selector: 'app-member',
  templateUrl: './member.component.html',
  styleUrls: ['./member.component.css']
})
export class MemberComponent implements OnInit {

  constructor(private fcnService: FcnService) { }  
  members: string[];
  ngOnInit() {  
    this.fcnService.GetAllMembers().subscribe(
      data => {  
        this.members = data as string [];  
       }  
    );
  }  

}
