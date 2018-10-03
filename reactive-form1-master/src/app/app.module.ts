import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { ReactiveFormsModule } from '@angular/forms';
import { AppRoutingModule } from './app.routing';
import { AppComponent } from './app.component';
import { NewpageComponent } from './newpage/newpage.component';
import { FirstComponent } from './first/first.component';
import { ServicefirstService } from './servicefirst.service';
import { HeaderComponent } from './header/header.component';
import { LoginComponent } from './login/login.component';
import { FormsModule } from '@angular/forms';
import { HttpClientModule} from '@angular/common/http';
import { DetailsComponent } from './details/details.component';
import { AuthService } from './auth.service';
import { CandidateDetailsComponent } from './candidate-details/candidate-details.component';
import { CandidatedetailsHRComponent } from './candidatedetails-hr/candidatedetails-hr.component';
import { ToastrModule,  } from 'ngx-toastr';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

@NgModule({
    imports: [
        BrowserModule,
        ReactiveFormsModule,
        AppRoutingModule,
        FormsModule,
        HttpClientModule,
        ToastrModule.forRoot(),
        BrowserAnimationsModule
    ],
    declarations: [
        AppComponent,
        NewpageComponent,
        FirstComponent,
        HeaderComponent,
        LoginComponent,
        DetailsComponent,
        CandidateDetailsComponent,
        CandidatedetailsHRComponent
    ],
    providers:[ServicefirstService,AuthService],
    bootstrap: [AppComponent]
})

export class AppModule { }