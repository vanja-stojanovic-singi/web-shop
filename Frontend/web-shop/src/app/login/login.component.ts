import { Component, OnDestroy } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { UserService } from '../services/user.service';
import { Router } from '@angular/router';
import { Subject, takeUntil } from 'rxjs';
import { LoggedUser } from '../models/logged-user.model';
import { HttpErrorResponse } from '@angular/common/http';
import { AuthService } from '../services/auth.service';

@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnDestroy {
    private unsubscribeAll: Subject<void> = new Subject();

    loginForm: FormGroup;
    showErrorMessage: boolean = false;

    constructor(private userService: UserService, private authService: AuthService, private router: Router) {
        this.loginForm = new FormGroup({
            email: new FormControl('', [Validators.required, Validators.email]),
            password: new FormControl('', [Validators.required])
        });
    }

    ngOnDestroy(): void {
        this.unsubscribeAll.next();
        this.unsubscribeAll.complete();
    }

    login() {
        this.showErrorMessage = false;

        if (!this.loginForm.valid) {
            this.loginForm.markAllAsTouched();
            return;
        }

        const formData = this.loginForm.getRawValue();

        this.authService.login(formData.email, formData.password).pipe(takeUntil(this.unsubscribeAll))
            .subscribe({
                next: (user: LoggedUser) => {
                    this.authService.loggedUser = user;
                    return this.router.navigateByUrl('/shop');
                },
                error: (error: HttpErrorResponse) => {
                    this.showErrorMessage = true;
                }
            });
    }
}
