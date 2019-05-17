import { Injectable } from '@angular/core';

declare let alertify: any;

//Wrapper class for alertifyjs to allow for typesafety and injectability.
@Injectable({
    providedIn: 'root'
})
export class AlertifyService {

    constructor() { }

    public confirm(message: string, okCallback: () => any): void {
        alertify.confirm(message, e => {
            if (e) {
                okCallback();
            }
            else {}
        });
    }

    public success(message: string): void {
        alertify.success(message);
    }

    public error(message: string): void {
        alertify.error(message);
    }

    public warning(message: string): void {
        alertify.warning(message);
    }

    public message(message: string): void {
        alertify.message(message);
    }
}
