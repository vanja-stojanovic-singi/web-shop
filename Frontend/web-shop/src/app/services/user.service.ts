import { Injectable } from '@angular/core';
import { User } from '../models/user.model';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { AuthService } from './auth.service';

@Injectable({
    providedIn: 'root'
})
export class UserService {
    private baseUrl: string = 'https://localhost:7208/api';

    constructor(private router: Router, private http: HttpClient, private authService: AuthService) {
    }

    getAllUsers() {
        return this.http.get<User>(`${this.baseUrl}/users`, {headers: this.authService.getAuthHeaders()});
    }

    getUserById(userId: string) {
        return this.http.get<User>(`${this.baseUrl}/users/${userId}`, {headers: this.authService.getAuthHeaders()});
    }

    updateUser(userId: string, user: User) {
        return this.http.put<User>(`${this.baseUrl}/users/${userId}`, user, {headers: this.authService.getAuthHeaders()});
    }

    updatePassword(userId: string, oldPassword: string, newPassword: string) {
        return this.http.put(`${this.baseUrl}/users/${userId}/change/password`, {oldPassword, newPassword}, {headers: this.authService.getAuthHeaders()});
    }
}
