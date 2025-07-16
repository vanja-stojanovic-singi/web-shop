import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { User } from '../models/user.model';
import { Router } from '@angular/router';
import { AuthService } from '../services/auth.service';
import { take } from 'rxjs';
import { LoggedUser } from '../models/logged-user.model';

@Component({
    selector: 'app-register',
    templateUrl: './register.component.html',
    styleUrls: ['./register.component.scss']
})
export class RegisterComponent {
    registerForm: FormGroup;

    constructor(private authService: AuthService, private router: Router) {
        this.registerForm = new FormGroup({
            name: new FormControl('', [Validators.required]),
            email: new FormControl('', [Validators.required, Validators.email]),
            password: new FormControl('', [Validators.required]),
            address: new FormControl('', [Validators.required]),
            city: new FormControl('', [Validators.required]),
            zip: new FormControl('', [Validators.required]),
            phone: new FormControl('', [Validators.required])
        });
    }

    register() {
        if (!this.registerForm.valid) {
            this.registerForm.markAllAsTouched();
            return;
        }

        const user = <User>this.registerForm.getRawValue();
        this.authService.register(user).pipe(take(1)).subscribe((user: LoggedUser) => {
            this.authService.loggedUser = user;
            this.router.navigateByUrl('/shop');
        });
    }
}
