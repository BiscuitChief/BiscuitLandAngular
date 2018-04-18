import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import 'rxjs/Rx';
import 'rxjs/add/operator/delay';
import 'rxjs/add/observable/of';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/map';

import { NavItem } from './navitem.type';

@Injectable()
export class NavMenuService {
    constructor(private http: Http) {

    }

    getNavItems(): Promise<NavItem[]> {
        return this.http.get('api/gettopnavigation')
            .toPromise()
            .then(response => response.json() as NavItem[]);
    }
}
