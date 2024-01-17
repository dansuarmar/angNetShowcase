import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { environment } from "src/environments/environment";
import { Customer } from "../models/customer.model";

@Injectable({
    providedIn: 'root'
})
export class CustomerService {
    private apiUrl: string;
    constructor(
        private http: HttpClient,
    ) {
        this.apiUrl =  environment.apiUrl + 'Customer';
    }

    getAll(): Observable<any>{
        return this.http.get<any>(this.apiUrl + '?size=1000');
    }

    getById(id: string): Observable<any>{
        return this.http.get<any>(this.apiUrl + `/${id}`);
    }

    add(customer: Customer): Observable<any> {
        var plain = Object.assign({}, customer); 
        return this.http.post<any>(this.apiUrl, plain);
    }

    update(customer: Customer): Observable<any> {
        return this.http.put<any>(this.apiUrl + `/${customer.id}`, customer);
      }
}