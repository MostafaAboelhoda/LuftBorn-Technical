import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { EmployeeService } from 'src/app/Services/employee.service';

@Component({
  selector: 'app-add-employee',
  templateUrl: './add-employee.component.html',
  styleUrls: ['./add-employee.component.css'],
})
export class AddEmployeeComponent implements OnInit {
  form: FormGroup = new FormGroup({});
  empId: string | null = '';
  constructor(
    private fb: FormBuilder,
    private empServ: EmployeeService,
    private toaster:ToastrService,
    private router:Router
  ) {}

  ngOnInit(): void {
    this.form = this.fb.group({
      userName: ["", [Validators.required]],
      email: ["", [Validators.required, Validators.email]],
      address: [""],
    });
  }

  submit() {
    this.empServ.create(this.form.value)
    .subscribe(res=>{
      this.toaster.success("Employee Created successfully!")
      this.router.navigate(['/employees'])
    },e=>{
      this.toaster.error("Error in create Employee!")
    })
  }
}
