import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { EmployeeService } from 'src/app/Services/employee.service';

@Component({
  selector: 'app-edit-employee',
  templateUrl: './edit-employee.component.html',
  styleUrls: ['./edit-employee.component.css'],
})
export class EditEmployeeComponent implements OnInit {
  form: FormGroup = new FormGroup({});
  empId: string | null = '';
  constructor(
    private fb: FormBuilder,
    private empServ: EmployeeService,
    private activeRoute: ActivatedRoute,
    private toaster:ToastrService,
    private router:Router
  ) {}

  ngOnInit(): void {
    this.form = this.fb.group({
      userName: ["", [Validators.required]],
      email: ["", [Validators.required, Validators.email]],
      address: [""],
      id:[""]
    });
    this.empId = this.activeRoute.snapshot.paramMap.get('id');
    this.empServ.find(this.empId).subscribe((res) => {
     this.form.setValue(res)
    });
  }

  submit() {
    this.empServ.update(this.form.value)
    .subscribe(res=>{
      this.toaster.success("Employee edited successfully!")
      this.router.navigate(['/employees'])
    },e=>{
      this.toaster.error("Error in edit Employee!")
    })
  }
}
