import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule  } from '@angular/forms'; // <-- NgModel lives here
import { HTTP_INTERCEPTORS, HttpClientModule, HttpClient} from '@angular/common/http'; 
import { from } from 'rxjs';
import { RouterModule, Routes } from '@angular/router';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { CategoryComponent } from './category/category.component';
import { CommunityComponent } from './community/community.component';
import { LoginComponent } from './login/login.component';
import { MemberComponent } from './member/member.component';
import { RoleComponent } from './role/role.component';
import { SubcategoryComponent } from './subcategory/subcategory.component';
import { RegisterComponent } from './register/register.component';
import { ActivityComponent } from './activity/activity.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { AlertComponent } from './_components/alert/alert.component';
import { FcnService } from './_services/fcn.service';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { TooltipModule } from 'ngx-bootstrap/tooltip';
import { ModalModule } from 'ngx-bootstrap/modal';
import { JwtInterceptor } from './_helpers/jwt.interceptor';

@NgModule({
  declarations: [
    AppComponent,
    CategoryComponent,
    CommunityComponent,
    LoginComponent,
    MemberComponent,
    RoleComponent,
    SubcategoryComponent,
    RegisterComponent,
    ActivityComponent,
    DashboardComponent,
    AlertComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    BsDropdownModule.forRoot(),
    TooltipModule.forRoot(),
    ModalModule.forRoot()
  ],
  providers: [FcnService,
    {
    provide : HTTP_INTERCEPTORS,
    useClass: JwtInterceptor,
    multi   : true,
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
