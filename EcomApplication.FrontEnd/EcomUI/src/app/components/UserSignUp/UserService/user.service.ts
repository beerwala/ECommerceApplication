import { HttpClient ,HttpErrorResponse} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, Observable, tap, throwError } from 'rxjs';
@Injectable({
  providedIn: 'root'
})
export class UserService {
  dataArr:any[]=[]
  flag=true;
  constructor(private http:HttpClient) { }

  registerRequest(data:any):Observable<any>{
    return this.http.post("https://localhost:7029/api/Account/RegisterUser",data)
      .pipe(
        catchError(this.errorHandler)
      )
  }

  loginRequest(data:any):Observable<any>{
    return this.http.post("https://localhost:7029/api/Account/LoginUser",data)
    .pipe(
      catchError(this.errorHandler)
    )
  }

  loginValidation(data: any): Observable<any> {
    return this.http.post<any>("https://localhost:7029/api/Account/AuthenticateLoginUser", data)
      .pipe(
        tap(response => {
          const jwtToken = response.result.data; // Extract JWT token from the response
          localStorage.setItem('jwtToken', jwtToken); // Set JWT token in local storage
        }),
        catchError(error => {
          // Handle error
          console.error('An error occurred:', error);
          throw error; // Rethrow the error to be caught by the caller
        })
      );
  }
  
errorHandler(error:HttpErrorResponse){
  console.log("error msg", error);
  return throwError(error);
}
}
