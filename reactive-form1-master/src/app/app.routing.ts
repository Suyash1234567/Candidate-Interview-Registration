import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { NewpageComponent } from './newpage/newpage.component';
import { FirstComponent } from './first/first.component';
import { LoginComponent } from './login/login.component';
import { DetailsComponent } from  './details/details.component';
import { AuthService } from './auth.service';
import { CandidateDetailsComponent } from './candidate-details/candidate-details.component';

const routes: Routes = [
    { path: '', component: FirstComponent },
    { path: 'Edit', component: NewpageComponent },
    { path: 'loginPage', component: LoginComponent },
    { path: 'details', component: DetailsComponent,canActivate : [AuthService] },
    { path: 'candidateDetails', component: CandidateDetailsComponent,canActivate:[AuthService] },
    // {path:'**',component:FirstComponent}
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule],
})

export class AppRoutingModule {}