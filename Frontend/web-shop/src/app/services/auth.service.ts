import { Injectable } from "@angular/core";
import { LoggedUser } from "../models/logged-user.model";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { User } from "../models/user.model";
import { Router } from "@angular/router";

@Injectable({
    providedIn: 'root'
})
export class AuthService {
    private authUser: LoggedUser | undefined;
    private baseUrl: string = 'https://localhost:7208';

    constructor(private http: HttpClient, private router: Router) {}

    
    login(email: string, password: string) {
        return this.http.post<LoggedUser>(`${this.baseUrl}/api/auth/login`, { email, password});
    }

    register(user: User) {
        return this.http.post<LoggedUser>(`${this.baseUrl}/api/auth/register`, user);
    }

    logout() {
        localStorage.setItem('loggedUser', '');
        this.authUser = undefined;
        this.router.navigateByUrl('/login');
    }

    getAuthHeaders() {
        const headers = new HttpHeaders();
        return headers.append('Accept', 'application/json')
                      .append('Authorization', `Bearer ${this.loggedUser.token}`);
    }

    get loggedUser(): LoggedUser {
        if (!this.authUser) {
            const userStr = localStorage.getItem('loggedUser');
            if (userStr) {
                this.authUser = JSON.parse(userStr) as LoggedUser;
            }
        }

        return this.authUser;
    }

    set loggedUser(user: LoggedUser) {
        this.authUser = user;
        localStorage.setItem('loggedUser', JSON.stringify(user));
    }
}