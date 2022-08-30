import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { EmployeeService } from 'src/app/Services/employee.service';
import { Employee } from 'src/models/employee';

@Component({
  selector: 'app-employee-list',
  templateUrl: './employee-list.component.html',
  styleUrls: ['./employee-list.component.css'],
})
export class EmployeeListComponent implements OnInit {
  employees: Employee[] = [];
  constructor(public employeeService: EmployeeService,private toastr: ToastrService) {}
  ngOnInit(): void {
    this.employeeService.getAll().subscribe((data: Employee[]) => {
      this.employees = data;
    });
  }
  deleteEmployee(id: string) {
    this.employeeService.delete(id).subscribe((res) => {
      this.employees = this.employees.filter((item) => item.id !== id);
      this.toastr.success("Employee deleted successfully!")
    },e=>{
      this.toastr.error("Error while deleted!")
    });
  }
}
