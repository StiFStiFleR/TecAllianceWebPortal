import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { environment } from 'src/environments/environment';
import { User } from "../_model/user";


@Injectable({
    providedIn: 'root'
})
export class AuthenticationService {

   
    constructor(private http: HttpClient) { 

    }

    login(body : User) {       
        return this.http.post(environment.serverUrl + 'user/login', body, {withCredentials: true });
    }

    logout() {
        return this.http.post(environment.serverUrl + 'user/logout',{} , {withCredentials: true });
    }
}