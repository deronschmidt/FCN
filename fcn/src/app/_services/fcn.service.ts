import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Member } from '../_models/member';
import { Community } from '../_models/community';
import { CommunityContact } from '../_models/community';
import { Activity } from '../_models/activity';
import { Role } from '../_models/role';
import { Subcategory } from '../_models/subcategory';
import { Category } from '../_models/category';

@Injectable({
  providedIn: 'root'
})
export class FcnService {
  Url: string;
  token: string;
  header: any;
  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

  constructor(private http: HttpClient) {
    this.Url = 'https://localhost:44380/api/';
    const headerSettings: { [name: string]: string | string[]; } = {};
    this.header = new HttpHeaders(headerSettings);

  }

  // FCN Member accessors
  GetAllMembers() {
    const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json' }) };
    return this.http.get(this.Url + 'FCNMember', httpOptions);
  }

  GetMember(id: number) {
    const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json' }) };
    return this.http.get(this.Url + `FCNMember/${id}`, httpOptions);
  }

  CreateMember(fcnmember: Member) {
    const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json' }) };
    return this.http.post(this.Url + 'FCNMember/', fcnmember, httpOptions).subscribe(
      data => {
        // console.log("POST Request is successful ", data);
      },
      error => {
        console.log("Error", error);
      }
    );
  }

  UpdateMember(fcnmember: Member, id: number) {
    const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json' }) };
    return this.http.put(this.Url + `FCNMember/${id}`, fcnmember, httpOptions).subscribe(
      data => {
        // console.log("PUT Request is successful ", data);
      },
      error => {
        console.log("Error", error);
      }
    );
  }

  DeleteMember(id: number) {
    return this.http.delete(this.Url + `FCNMember/${id}`).subscribe(
      data => {
        // console.log("DELETE Request is successful ", data);
      },
      error => {
        console.log("Error", error);
      }
    );
  }

  // Community accessors
  GetAllCommunities() {
    const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json' }) };
    return this.http.get(this.Url + 'Community', httpOptions);
  }

  GetCommunity(id: number) {
    const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json' }) };
    return this.http.get(this.Url + `Community/${id}`, httpOptions);
  }

  CreateCommunity(community: Community) {
    const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json' }) };
    return this.http.post(this.Url + 'Community/', community, httpOptions).subscribe(
      data => {
        // console.log("POST Request is successful ", data);
      },
      error => {
        console.log("Error", error);
      }
    );
  }

  UpdateCommunity(community: Community, id: number) {
    const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json' }) };
    return this.http.put(this.Url + `Community/${id}`, community, httpOptions).subscribe(
      data => {
        // console.log("PUT Request is successful ", data);
      },
      error => {
        console.log("Error", error);
      }
    );
  }

  DeleteCommunity(id: number) {
    return this.http.delete(this.Url + `Community/${id}`).subscribe(
      data => {
        // console.log("DELETE Request is successful ", data);
      },
      error => {
        console.log("Error", error);
      }
    );
  }

  // Service Category accessors
  GetAllCategories() {
    const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json' }) };
    return this.http.get(this.Url + 'ServiceCategory', httpOptions);
  }

  GetCategory(id: number) {
    const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json' }) };
    return this.http.get(this.Url + `ServiceCategory/${id}`, httpOptions);
  }

  CreateCategory(category: Category) {
    const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json' }) };
    return this.http.post(this.Url + 'ServiceCategory/', category, httpOptions).subscribe(
      data => {
        // console.log("POST Request is successful ", data);
      },
      error => {
        console.log("Error", error);
      }
    );
  }

  UpdateCategory(category: Category, id: number) {
    const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json' }) };
    return this.http.put(this.Url + `ServiceCategory/${id}`, category, httpOptions).subscribe(
      data => {
        // console.log("PUT Request is successful ", data);
      },
      error => {
        console.log("Error", error);
      }
    );
  }

  DeleteCategory(id: number) {
    return this.http.delete(this.Url + `ServiceCategory/${id}`).subscribe(
      data => {
        // console.log("DELETE Request is successful ", data);
      },
      error => {
        console.log("Error", error);
      }
    );
  }

  // Service Subcategory accessors
  GetAllSubcategories() {
    const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json' }) };
    return this.http.get(this.Url + 'ServiceSubcategory', httpOptions);
  }

  GetSubcategory(id: number) {
    const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json' }) };
    return this.http.get(this.Url + `ServiceSubcategory/${id}`, httpOptions);
  }

  CreateSubcategory(subcategory: Subcategory) {
    const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json' }) };
    return this.http.post(this.Url + `ServiceSubcategory/`, subcategory, httpOptions).subscribe(
      data => {
        // console.log("POST Request is successful ", data);
      },
      error => {
        console.log("Error", error);
      }
    );
  }

  UpdateSubcategory(subcategory: Subcategory, id: number) {
    const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json' }) };
    return this.http.put(this.Url + `ServiceSubcategory/${id}`, subcategory, httpOptions).subscribe(
      data => {
        // console.log("PUT Request is successful ", data);
      },
      error => {
        console.log("Error", error);
      }
    );
  }

  DeleteSubcategory(id: number) {
    return this.http.delete(this.Url + `ServiceSubcategory/${id}`).subscribe(
      data => {
        // console.log("DELETE Request is successful ", data);
      },
      error => {
        console.log("Error", error);
      }
    );
  }

  // Member Role accessors
  GetAllRoles() {
    const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json' }) };
    return this.http.get(this.Url + 'MemberRole', httpOptions);
  }

  GetRole(id: number) {
    const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json' }) };
    return this.http.get(this.Url + `MemberRole/${id}`, httpOptions);
  }

  CreateRole(role: Role) {
    const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json' }) };
    return this.http.post(this.Url + 'MemberRole/', role, httpOptions).subscribe(
      data => {
        // console.log("POST Request is successful ", data);
      },
      error => {
        console.log("Error", error);
      }
    );
  }

  UpdateRole(role: Role, id: number) {
    const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json' }) };
    return this.http.put(this.Url + `MemberRole/${id}` , role, httpOptions).subscribe(
      data => {
        // console.log("PUT Request is successful ", data);
      },
      error => {
        console.log("Error", error);
      }
    );
  }

  DeleteRole(id: number) {
    return this.http.delete(this.Url + `MemberRole/${id}`).subscribe(
      data => {
        // console.log("DELETE Request is successful ", data);
      },
      error => {
        console.log("Error", error);
      }
    );
  }
}
