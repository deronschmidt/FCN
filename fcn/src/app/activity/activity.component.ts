import { Component, OnInit } from '@angular/core';
import { FcnService } from '../_services/fcn.service';

@Component({
  selector: 'app-activity',
  templateUrl: './activity.component.html',
  styleUrls: ['./activity.component.css']
})
export class ActivityComponent implements OnInit {

  constructor(private fcnService: FcnService) { }

  ngOnInit() {
  }

}
