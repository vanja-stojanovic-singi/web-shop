export class User {
    id: string;
    name: string;
    email: string;
    password: string;
    address: string;
    city: string;
    zip: string;
    phone: string;
    birthday: Date;
    role: string;

    constructor() {
        this.id = '';
        this.name = '';
        this.email = '';
        this.password = '';
        this.address = '';
        this.city = '';
        this.zip = '';
        this.phone = '';
        this.birthday = new Date();
        this.role = 'Customer';
    }
}