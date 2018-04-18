import { Component, Input, OnInit  } from '@angular/core';
import { ActivatedRoute, ParamMap } from '@angular/router';

import { LoginCredential } from './logincredential.type';

@Component({
    selector: 'login',
    templateUrl: './login.component.html'
})
export class LoginComponent {

    loginInfo: LoginCredential;

    constructor() {
        this.loginInfo = new LoginCredential();
    }

}