import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Customer } from '../models/customer.model';

@Injectable({
    providedIn: 'root',
})
export class CustomerService {
    private apiUrl: string;
    constructor(private http: HttpClient) {
        this.apiUrl = environment.apiUrl + 'Customer';
    }

    getAll(first: number, rows: number, sortField: string, sortOrder: number): Observable<any> {
        const parameters = this.generateGetAllParameters(first, rows, sortField, sortOrder);
        return this.http.get<any>(this.apiUrl + parameters);
    }

    private generateGetAllParameters(first: number, rows: number, sortField: string, sortOrder: number): string{ 
        let parameters = '?';
        parameters += `size=${rows}&`;
        parameters += `page=${(first / rows) + 1}&`;
        if(sortField){
            parameters += `sort=${sortField}&`;
            parameters += sortOrder === -1 ? `order=desc&` : '';
        }


        return parameters;
    }

    getById(id: string): Observable<any> {
        return this.http.get<any>(this.apiUrl + `/${id}`);
    }

    add(customer: Customer): Observable<any> {
        var plain = Object.assign({}, customer);
        return this.http.post<any>(this.apiUrl, plain);
    }

    update(customer: Customer): Observable<any> {
        return this.http.put<any>(this.apiUrl + `/${customer.id}`, customer);
    }

    delete(customer: Customer) {
        return this.http.delete<any>(this.apiUrl + `/${customer.id}`);
    }
}
