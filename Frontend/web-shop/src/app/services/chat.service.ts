import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { AuthService } from './auth.service';

@Injectable({
    providedIn: 'root'
})
export class ChatService {
    private rasaApiUrl: string = 'http://192.168.1.12:5005/webhooks/rest/webhook';

    constructor(private http: HttpClient, private authService: AuthService) {
    }

    sendMessage(message: string) {
        const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
        const sender = 'test_user';

        return this.http.post<any>(this.rasaApiUrl, {sender, message}, {headers: headers});
    }
}
