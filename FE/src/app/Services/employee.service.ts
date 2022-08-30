import { HttpHeaders, HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, Observable, throwError } from 'rxjs';
import { Employee } from 'src/models/employee';

@Injectable({
  providedIn: 'root',
})
export class EmployeeService {
  private apiURL = 'https://localhost:7019/api/Employees/';

  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
    }),
  };

  constructor(private httpClient: HttpClient) {}

  getAll(): Observable<Employee[]> {
    return this.httpClient
      .get<Employee[]>(this.apiURL + 'getAllEmployees')
      .pipe(catchError(this.errorHandler));
  }

  create(employee: Employee): Observable<Employee> {
    return this.httpClient
      .post<Employee>(
        this.apiURL + 'addEmployee',
        JSON.stringify(employee),
        this.httpOptions
      )
      .pipe(catchError(this.errorHandler));
  }

  find(id: string | null): Observable<Employee> {
    return this.httpClient
      .get<Employee>(this.apiURL + 'getEmployee/' + id)
      .pipe(catchError(this.errorHandler));
  }

  update(emp: Employee): Observable<Employee> {
    return this.httpClient
      .post<Employee>(this.apiURL + 'editEmployee', JSON.stringify(emp),this.httpOptions)
      .pipe(catchError(this.errorHandler));
  }

  delete(id: string) {
    return this.httpClient
      .post<Employee>(this.apiURL + 'deleteEmployee/' + id, this.httpOptions)
      .pipe(catchError(this.errorHandler));
  }

  errorHandler(error: any) {
    let errorMessage = '';

    if (error.error instanceof ErrorEvent) {
      errorMessage = error.error.message;
    } else {
      errorMessage = `Error Code: ${error.status}\nMessage: ${error.message}`;
    }

    return throwError(errorMessage);
  }
}
