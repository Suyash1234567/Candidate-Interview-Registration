import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ServicefirstService } from '../servicefirst.service';
import { HeaderComponent } from 'src/app/header/header.component';
import { ToastrService } from 'ngx-toastr';
import { DataServiceService } from '../data-service.service';

@Component({
  selector: 'app-first',
  templateUrl: './first.component.html',
  styleUrls: ['./first.component.css']
})

export class FirstComponent implements OnInit {
  registerForm: FormGroup;
  submitted = false;
  selectedFile = null;
  textBoxType = "password";
  imagesource = "./info.png"
  namePattern = "^[A-Za-z][A-Za-z\\s]*$";
  pwdPattern = "(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{8,}";
  mobnumPattern = "^[0-9]{10}$";
  emailPattern = "^[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}$";
  empidPAttern = "^[0-9]{4}$";
  data;
  isValid: boolean = false;
  today = new Date();
  minDate = new Date(1990, 5, 11);
  qualifications = [];
  resume;
  // public Qualifications=[];

  constructor(private formBuilder: FormBuilder, private dataService: DataServiceService, private router: Router, private ServicefirstService: ServicefirstService, private toastr: ToastrService) {

    this.registerForm = this.formBuilder.group({
      candidateName: ['', [Validators.required, Validators.pattern('^[A-Za-z][A-Za-z\\s]*$')]],
      candidateEmail: ['', [Validators.required, Validators.pattern('^[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}$')]],
      candidateAddress: ['', [Validators.required, Validators.pattern('^[A-Za-z0-9][A-Za-z0-9,-\\s]+$')]],
      candidateHighestQualification: ['', [Validators.required]],
      candidateContactNo: ['', [Validators.required, Validators.minLength(10), Validators.maxLength(10)]],
      candidateResume: ['', [Validators.required]],
      resume: ['', [Validators.required]],
      candidateDateOfBirth: ['', [Validators.required]],
      UploadFile: ['']
    })
    if (this.registerForm.value != null) {
      this.loadForm();
    }
  }

  ngOnInit() {
    this.dataService.getQualifications().subscribe(res=>{
      this.qualifications=res;
    })
  }

  get f() { return this.registerForm.controls; }

  onFileSelected(event) {
    this.selectedFile = <File>event.target.files[0];

    console.log(this.selectedFile)
    this.dataService.setResume(this.selectedFile);
    this.registerForm.controls.candidateResume.setValue(this.selectedFile.name);
    // this.registerForm.controls.resume.setValue(this.selectedFile);
    console.log(this.registerForm.controls.resume);
    // const fd = new FormData();
    // fd.append('resume', this.selectedFile, this.selectedFile.name);
    // this.registerForm.controls.UploadFile.setValue(fd);
  }



  loadForm() {
    this.data = this.ServicefirstService.data
    //console.log(this.data)
    if (this.data != null) {
      this.registerForm.controls.candidateName.setValue(this.data.candidateName);
      this.registerForm.controls.candidateEmail.setValue(this.data.candidateEmail);
      this.registerForm.controls.candidateAddress.setValue(this.data.candidateAddress);
      this.registerForm.controls.candidateHighestQualification.setValue(this.data.candidateHighestQualification);
      this.registerForm.controls.candidateContactNo.setValue(this.data.candidateContactNo);
      this.registerForm.controls.candidateDateOfBirth.setValue(this.data.candidateDateOfBirth);
    }
  }

  onSubmit() {
    this.submitted = true;

    // stop here if form is invalid
    if (this.registerForm.invalid) {
      return;
    }

    console.log(this.registerForm.value)

    this.ServicefirstService.data = this.registerForm.value
    this.dataService.setButtonStatus(true);
    this.router.navigate(['/Edit']);

  }
  // Host Listener for Input Check

  contactKeyPress(event) {
    if (!Number(event.key) && (event.key != 0)) {
      return false;
    }
    const currentVal = this.registerForm.controls.candidateContactNo.value + event.key; //projected value that u type
    if (currentVal.length == 11) {
      return false;
    }
  }

}