import { Component, OnInit } from '@angular/core';
import { FcnService } from '../_services/fcn.service';

@Component({
  selector: 'app-community',
  templateUrl: './community.component.html',
  styleUrls: ['./community.component.css']
})
export class CommunityComponent implements OnInit {
  constructor(private fcnService: FcnService) { }  
  communities: string[];
  ngOnInit() {      
    this.fcnService.GetAllCommunities().subscribe(
      data => {  
        this.communities = data as string [];  
       }  
    );
  }  
}
