import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { catchError, map, tap } from 'rxjs/operators';
import { Member } from '../_models/member';
import { Community } from '../_models/community';
import { CommunityContact } from '../_models/community';
import { Activity } from '../_models/activity';
import { Role } from '../_models/role';
import { Subcategory } from '../_models/subcategory';
import { Category } from '../_models/category';
import { BehaviorSubject } from 'rxjs';

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
  GetAllMembers ()
  {
    const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json' }) };
    return this.http.get(this.Url + 'FCNMember', httpOptions);
  }

  GetMember (id: number)
  {
    const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json' }) };
    return this.http.get(this.Url + 'FCNMember/${id}', httpOptions);
  }

  CreateMember(fcnmember: Member) {
    const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json' }) };
    return this.http.post<Member[]>(this.Url + 'FCNMember/', fcnmember, httpOptions)
  }

  UpdateMember(fcnmember: Member, id: number) {
    const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json' }) };
    return this.http.post<Member[]>(this.Url + 'FCNMember/${id}', fcnmember, httpOptions)
  }
  
  DeleteMember(id: number) {
    return this.http.delete(this.Url + 'FCNMember/${id}');
  }

  // Community accessors
  GetAllCommunities ()
  {
    const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json' }) };
    return this.http.get(this.Url + 'Community', httpOptions);
  }

  GetCommunity(id: number)
  {
    const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json' }) };
    return this.http.get(this.Url + 'Community/${id}', httpOptions);
  }

  CreateCommunity(community: Community) {
    const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json' }) };
    return this.http.post<Community[]>(this.Url + 'Community/', community, httpOptions)
  }

  UpdateCommunity(community: Community, id: number) {
    const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json' }) };
    return this.http.post<Community[]>(this.Url + 'Community/${id}', community, httpOptions)
  }
  
  DeleteCommunity(id: number) {
    return this.http.delete(this.Url + 'Community/${id}');
  }

  // Service Category accessors
  GetAllCategories ()
  {
    const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json' }) };
    return this.http.get(this.Url + 'ServiceCategory', httpOptions);
  }

  GetCategory(id: number)
  {
    const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json' }) };
    return this.http.get(this.Url + 'ServiceCategory/${id}', httpOptions);
  }

  CreateCategory(subcategory: Subcategory) {
    const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json' }) };
    return this.http.post<Subcategory[]>(this.Url + 'ServiceCategory/', subcategory, httpOptions)
  }

  UpdateCategory(subcategory: Subcategory, id: number) {
    const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json' }) };
    return this.http.post<Subcategory[]>(this.Url + 'ServiceCategory/${id}', subcategory, httpOptions)
  }
  
  DeleteCategory(id: number) {
    return this.http.delete(this.Url + 'ServiceCategory/${id}');
  }

  // Service Subcategory accessors
  GetAllSubcategories ()
  {
    const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json' }) };
    return this.http.get(this.Url + 'ServiceSubcategory', httpOptions);
  }

  GetSubcategory(id: number)
  {
    const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json' }) };
    return this.http.get(this.Url + 'ServiceSubcategory/${id}', httpOptions);
  }

  CreateSubcategory(subcategory: Subcategory) {
    const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json' }) };
    return this.http.post<Subcategory[]>(this.Url + 'ServiceSubcategory/', subcategory, httpOptions)
  }

  UpdateSubcategory(subcategory: Subcategory, id: number) {
    const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json' }) };
    return this.http.post<Subcategory[]>(this.Url + 'ServiceSubcategory/${id}', subcategory, httpOptions)
  }
  
  DeleteSubcategory(id: number) {
    return this.http.delete(this.Url + 'ServiceSubcategory/${id}');
  }

  // Member Role accessors
  GetAllRoles ()
  {
    const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json' }) };
    return this.http.get(this.Url + 'MemberRole', httpOptions);
  }

  GetRole(id: number)
  {
    const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json' }) };
    return this.http.get(this.Url + 'MemberRole/${id}', httpOptions);
  }

  CreateRole(role: Role) {
    const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json' }) };
    return this.http.post<Role[]>(this.Url + 'MemberRole/', role, httpOptions)
  }

  UpdateRole(role: Role, id: number) {
    const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json' }) };
    return this.http.post<Role[]>(this.Url + 'MemberRole/${id}', role, httpOptions)
  }
  
  DeleteRole(id: number) {
    return this.http.delete(this.Url + 'MemberRole/${id}');
  }
}
